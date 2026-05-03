using System;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Controllers;
using GameProject.Enemies;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.Managers;
using GameProject.PlayerSpace.States;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace;

#nullable enable

internal enum FacingDirection {
  Left,
  Right,
  Up,
  Down,
}

internal class Player : IInitable, ITemporalUpdatable, IGPDrawable, ICollidable {
  private static readonly float PLAYER_WIDTH = 171.0f * 0.15f;
  private static readonly float PLAYER_HEIGHT = 323.0f * 0.15f;

  public static readonly float INVINCIBILITY_DURATION = 1.5f;
  public static readonly float DAMAGE_FLASH_DURATION = 0.3f;

  private readonly ILevelManager levelManager;
  private readonly BoxCollider collider;
  private bool inputLeftThisFrame, inputRightThisFrame, inputUpThisFrame, inputDownThisFrame;
  private bool inputLeftLastFrame, inputRightLastFrame, inputUpLastFrame, inputDownLastFrame;
  private int activeDirX = 0;
  private int activeDirY = 0;
  private Vector2 lastInputVelocity = Vector2.Zero;

  public IShape Shape => collider;
  public Layer Mask { get; } = Layer.Environment;
  public Layer Layer { get; } = Layer.Player;
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Speed { get; set; } = 200f;

  public int Health { get; set; } = 100;
  
  public double InvincibilityTimer { get; set; } = 0.0;
  public double InfiniteAmmoTimer { get; set; } = 0.0;
  public bool HasInfiniteAmmo => InfiniteAmmoTimer > 0;

  public bool IsInvincible => InvincibilityTimer > 0;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public PlayerInventory Inventory { get; private set; }

  public Rectangle BoundingBox {
    get {
      int width = (int) PLAYER_WIDTH;
      int height = (int) PLAYER_HEIGHT;
      int x = (int) Position.X - (width / 2);
      int y = (int) Position.Y - (height / 2);
      return new Rectangle(x, y, width, height);
    }
  }


  public IPlayerState StaticState { get; private set; }
  public IPlayerState MovingState { get; private set; }
  public IPlayerState UseItemState { get; private set; }
  public IPlayerState DeadState { get; private set; }
  private IPlayerState currentState;

  public Player(ILevelManager levelManager, Game1 game) {
    this.levelManager = levelManager;
    Position = Vector2.Zero;
    Velocity = Vector2.Zero;
    Inventory = new PlayerInventory(this, levelManager);

    float width = 171 * 0.15f;
    float height = 323 * 0.15f;
    collider = new BoxCollider(width, height, Position);

    MovingState = new PlayerMovingState(this);
    StaticState = new PlayerStaticState(this);
    UseItemState = new PlayerUseItemState(this);
    DeadState = new PlayerDeadState(this, () => game.ChangeState(game.StateLoss));
    currentState = StaticState;
  }

  public void ChangeState(IPlayerState newState) {
    currentState = newState;
  }

  public void MoveUp() => inputUpThisFrame = true;
  
  public void MoveDown() => inputDownThisFrame = true;
  
  public void MoveLeft() => inputLeftThisFrame = true;
  
  public void MoveRight() => inputRightThisFrame = true;
  
  public void UseItem(UseType useType) {
    currentState.UseItem(useType);
  }

  public void UseKey(UseType useType) {
    currentState.UseKey(useType);
  }

  public void Die() {
    currentState.Die();
  }

  public void TakeDamage(int amount) {
    currentState.TakeDamage(amount);
  }

  public void Initialize() {
    Inventory.Initialize();
  }

  public void LoadContent(ContentManager content) {
    Inventory.LoadContent(content);
  }

  public void Update(double deltaTime) {
    bool justPressedLeft = inputLeftThisFrame && !inputLeftLastFrame;
    bool justPressedRight = inputRightThisFrame && !inputRightLastFrame;
    bool justPressedUp = inputUpThisFrame && !inputUpLastFrame;
    bool justPressedDown = inputDownThisFrame && !inputDownLastFrame;

    if (justPressedLeft) activeDirX = -1;
    else if (justPressedRight) activeDirX = 1;
    else if (!inputLeftThisFrame && !inputRightThisFrame) activeDirX = 0;
    else if (!inputLeftThisFrame && inputRightThisFrame) activeDirX = 1;
    else if (inputLeftThisFrame && !inputRightThisFrame) activeDirX = -1;

    if (justPressedUp) activeDirY = -1;
    else if (justPressedDown) activeDirY = 1;
    else if (!inputUpThisFrame && !inputDownThisFrame) activeDirY = 0;
    else if (!inputUpThisFrame && inputDownThisFrame) activeDirY = 1;
    else if (inputUpThisFrame && !inputDownThisFrame) activeDirY = -1;

    if (activeDirX == -1) currentState.MoveLeft();
    if (activeDirX == 1) currentState.MoveRight();
    if (activeDirY == -1) currentState.MoveUp();
    if (activeDirY == 1) currentState.MoveDown();

    if (Velocity.X != 0 && MathF.Sign(Velocity.X) != MathF.Sign(lastInputVelocity.X)) {
      Direction = (Velocity.X > 0) ? FacingDirection.Right : FacingDirection.Left;
    }
    if (Velocity.Y != 0 && MathF.Sign(Velocity.Y) != MathF.Sign(lastInputVelocity.Y)) {
      Direction = (Velocity.Y > 0) ? FacingDirection.Down : FacingDirection.Up;
    }
    if (Velocity.X != 0 && Velocity.Y == 0) {
      Direction = (Velocity.X > 0) ? FacingDirection.Right : FacingDirection.Left;
    }
    if (Velocity.Y != 0 && Velocity.X == 0) {
      Direction = (Velocity.Y > 0) ? FacingDirection.Down : FacingDirection.Up;
    }
    lastInputVelocity = Velocity;

    if (InvincibilityTimer > 0) InvincibilityTimer -= deltaTime;
    if (InfiniteAmmoTimer > 0) InfiniteAmmoTimer -= deltaTime;
    if (Velocity != Vector2.Zero) Velocity = Vector2.Normalize(Velocity) * Speed;
    float xStep = Velocity.X * ((float) deltaTime);
    float yStep = Velocity.Y * ((float) deltaTime);

    Position = new Vector2(Position.X + xStep, Position.Y);
    if (collider != null) collider.Position = Position;
    levelManager.CurrentLevel.PlayerResolveCollisions(this, CollisionAxis.X, MathF.Abs(yStep) + 1f);

    Position = new Vector2(Position.X, Position.Y + yStep);
    if (collider != null) collider.Position = Position;
    levelManager.CurrentLevel.PlayerResolveCollisions(this, CollisionAxis.Y, MathF.Abs(xStep) + 1f);

    currentState.Update(deltaTime);
    Velocity = Vector2.Zero;

    Inventory.Update(deltaTime);

    inputLeftLastFrame = inputLeftThisFrame;
    inputRightLastFrame = inputRightThisFrame;
    inputUpLastFrame = inputUpThisFrame;
    inputDownLastFrame = inputDownThisFrame;
    inputLeftThisFrame = false;
    inputRightThisFrame = false;
    inputUpThisFrame = false;
    inputDownThisFrame = false;

    // Auto-Collect for Ammo
    if (levelManager?.CurrentLevel != null) {
      foreach (var pickup in levelManager.CurrentLevel.GetRemoveAmmoInRange(Position, 30.0f)) {
        pickup.OnPickup(this);
      }
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    currentState.Draw(spriteBatch);
    Inventory.Draw(spriteBatch, Position, Direction, TextureStore.Instance.WhitePixel);
  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      Position = CollisionHelper.GetNudgedPosition(info, Position, info.Overlap + 0.01f);
      if (collider != null) collider.Position = Position;
    }

    if (info.Collider is IEnemy) {
      TakeDamage(50);
    }
  }

  public void Interact() {
    if (levelManager?.CurrentLevel == null) return;

    float grabRange = 75f;

    IWorldPickup? closestPickup = levelManager.CurrentLevel.GetClosestPickupInRange(Position, grabRange);

    if (closestPickup != null) {
      closestPickup.OnPickup(this);
      levelManager.CurrentLevel.RemovePickup(closestPickup);
    }
  }
}
