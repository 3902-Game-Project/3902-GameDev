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
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace;

internal enum FacingDirection {
  Left,
  Right,
  Up,
  Down,
}

internal class Player : IGPUpdatable, IGPDrawable, ICollidable {
  private static readonly float PLAYER_WIDTH = 171.0f * 0.15f;
  private static readonly float PLAYER_HEIGHT = 323.0f * 0.15f;

  public static readonly float INVINCIBILITY_DURATION = 1.5f;
  public static readonly float DAMAGE_FLASH_DURATION = 0.3f;

  public IShape Shape => Collider;
  private ILevelManager LevelManager { get; set; }
  private BoxCollider Collider { get; set; }
  public Layer Mask { get; } = Layer.Environment;
  public Layer Layer { get; } = Layer.Player;
  public IPlayerState State { get; set; }
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Speed { get; set; } = 200f;

  public int Health { get; set; } = 100;
  private bool inputLeftThisFrame, inputRightThisFrame, inputUpThisFrame, inputDownThisFrame;
  private bool inputLeftLastFrame, inputRightLastFrame, inputUpLastFrame, inputDownLastFrame;

  private int activeDirX = 0;
  private int activeDirY = 0;

  public float InvincibilityTimer { get; set; } = 0f;
  public float DamageFlashTimer { get; set; } = 0f;
  public float InfiniteAmmoTimer { get; set; } = 0f;
  public bool HasInfiniteAmmo => InfiniteAmmoTimer > 0;

  public bool IsInvincible => InvincibilityTimer > 0;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  private Vector2 lastInputVelocity = Vector2.Zero;

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
  public Color CurrentTintColor => DamageFlashTimer > 0 ? Color.Red : Color.White;

  public Player(ILevelManager levelManager, Game1 game) {
    LevelManager = levelManager;
    Position = Vector2.Zero;
    Velocity = Vector2.Zero;
    Inventory = new PlayerInventory(levelManager);

    float width = 171 * 0.15f;
    float height = 323 * 0.15f;
    Collider = new BoxCollider(width, height, Position);

    MovingState = new PlayerAnimatedMovingState(this);
    StaticState = new PlayerStaticState(this);
    UseItemState = new PlayerUseItemState(this);
    DeadState = new PlayerDeadState(this, () => game.ChangeState(game.StateLoss));
    State = StaticState;
  }

  public void MoveUp() => inputUpThisFrame = true;
  public void MoveDown() => inputDownThisFrame = true;
  public void MoveLeft() => inputLeftThisFrame = true;
  public void MoveRight() => inputRightThisFrame = true;
  public void UseItem(UseType useType) {
    State.UseItem(useType);
  }
  public void UseKey(UseType useType) {
    State.UseKey(useType);
  }
  public void Die() => State.Die();

  public void TakeDamage(int amount) {
    State.TakeDamage(amount);
  }

  public void Update(GameTime gameTime) {
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

    if (activeDirX == -1) State.MoveLeft();
    if (activeDirX == 1) State.MoveRight();
    if (activeDirY == -1) State.MoveUp();
    if (activeDirY == 1) State.MoveDown();

    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
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

    if (InvincibilityTimer > 0) InvincibilityTimer -= dt;
    if (DamageFlashTimer > 0) DamageFlashTimer -= dt;
    if (Velocity != Vector2.Zero) Velocity = Vector2.Normalize(Velocity) * Speed;
    float xStep = Velocity.X * dt;
    float yStep = Velocity.Y * dt;

    Position = new Vector2(Position.X + xStep, Position.Y);
    if (Collider != null) Collider.Position = Position;
    LevelManager.CurrentLevel.PlayerResolveCollisions(this, CollisionAxis.X, MathF.Abs(yStep) + 1f);

    Position = new Vector2(Position.X, Position.Y + yStep);
    if (Collider != null) Collider.Position = Position;
    LevelManager.CurrentLevel.PlayerResolveCollisions(this, CollisionAxis.Y, MathF.Abs(xStep) + 1f);

    State.Update(gameTime);
    Velocity = Vector2.Zero;

    Inventory.Update(gameTime, Position, Direction);

    inputLeftLastFrame = inputLeftThisFrame;
    inputRightLastFrame = inputRightThisFrame;
    inputUpLastFrame = inputUpThisFrame;
    inputDownLastFrame = inputDownThisFrame;
    inputLeftThisFrame = false;
    inputRightThisFrame = false;
    inputUpThisFrame = false;
    inputDownThisFrame = false;

    // Auto-Collect for Ammo
    if (LevelManager?.CurrentLevel != null) {
      for (int i = LevelManager.CurrentLevel.Pickups.Count - 1; i >= 0; i--) {
        var pickup = LevelManager.CurrentLevel.Pickups[i];
        if (pickup.IsAutoCollect) {
          if (Vector2.Distance(Position, pickup.Position) < 30f) {
            pickup.OnPickup(this);
            LevelManager.CurrentLevel.RemovePickup(pickup);
          }
        }
      }
    }
  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      Position = CollisionHelper.GetNudgedPosition(info, Position, info.Overlap + 0.01f);
      if (Collider != null) Collider.Position = Position;
    }

    if (info.Collider is IEnemy) {
      TakeDamage(50);
    }
  }

  public void Interact() {
    if (LevelManager?.CurrentLevel == null) return;

    float grabRange = 75f;
    IWorldPickup closestPickup = null;
    float closestDistance = float.MaxValue;

    foreach (var pickup in LevelManager.CurrentLevel.Pickups) {
      float distance = Vector2.Distance(Position, pickup.Position);
      if (distance < grabRange && distance < closestDistance) {
        closestDistance = distance;
        closestPickup = pickup;
      }
    }

    if (closestPickup != null) {
      closestPickup.OnPickup(this);
      LevelManager.CurrentLevel.RemovePickup(closestPickup);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    State.Draw(spriteBatch);
    Inventory.Draw(spriteBatch, Position, TextureStore.Instance.WhitePixel);
  }
}
