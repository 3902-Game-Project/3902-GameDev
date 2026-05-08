using System;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Controllers;
using GameProject.Enemies;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.PlayerSpace.States;
using GameProject.Projectiles;
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
  private readonly BoxCollider collider;
  private bool inputLeftThisFrame, inputRightThisFrame, inputUpThisFrame, inputDownThisFrame;
  private bool inputLeftLastFrame, inputRightLastFrame, inputUpLastFrame, inputDownLastFrame;
  private int activeDirX = 0;
  private int activeDirY = 0;
  private Vector2 lastInputVelocity = Vector2.Zero;
  public IShape Shape => collider;
  public Layer Layer { get; } = Layer.Player;
  private Vector2 _position;
  public Vector2 Position {
    get => _position;
    set {
      _position = value;
      if (collider != null) collider.Position = _position;
    }
  }
  public Vector2 Velocity { get; set; }
  public int Health { get; private set; } = Constants.DEFAULT_MAX_HEALTH;
  public double InvincibilityTimer { get; private set; } = 0.0;
  public double InfiniteAmmoTimer { get; private set; } = 0.0;
  public bool WantsToInteract { get; set; } = false;
  public bool HasInfiniteAmmo => InfiniteAmmoTimer > 0;
  public bool IsInvincible => InvincibilityTimer > 0;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public PlayerInventory Inventory { get; private set; }

  public PlayerStateMachine StateMachine { get; private set; }

  public Player(ProjectileManagerGetter GetProjectileManager, Action onLoss) {
    Position = Vector2.Zero;
    Velocity = Vector2.Zero;
    Inventory = new PlayerInventory(this, GetProjectileManager);

    float width = Constants.PLAYER_SPRITE_WIDTH * Constants.PLAYER_SPRITE_SCALE;
    float height = Constants.PLAYER_SPRITE_HEIGHT * Constants.PLAYER_SPRITE_SCALE;
    collider = new BoxCollider(width, height, Position);

    StateMachine = new PlayerStateMachine(this, onLoss);
  }
  public void Heal(int amount) => Health = Math.Min(Health + amount, Constants.DEFAULT_MAX_HEALTH);
  public void CheatHealth(int amount) => Health = amount;
  public void ReduceHealth(int amount) => Health = Math.Max(0, Health - amount);
  public void AddInvincibility(float duration) => InvincibilityTimer += duration;
  public void SetInvincibility(float duration) => InvincibilityTimer = duration;
  public void AddInfiniteAmmo(float duration) => InfiniteAmmoTimer += duration;

  public void MoveUp() => inputUpThisFrame = true;

  public void MoveDown() => inputDownThisFrame = true;

  public void MoveLeft() => inputLeftThisFrame = true;

  public void MoveRight() => inputRightThisFrame = true;

  public void UseItem(UseType useType) {
    StateMachine.CurrentState.UseItem(useType);
  }

  public void UseKey(UseType useType) {
    StateMachine.CurrentState.UseKey(useType);
  }

  public void Die() {
    StateMachine.CurrentState.Die();
  }

  public void TakeDamage(int amount) {
    StateMachine.CurrentState.TakeDamage(amount);
  }

  public void Interact() {
    StateMachine.CurrentState.Interact();
  }

  public void Initialize() {
    Inventory.Initialize();
  }

  public void LoadContent(ContentManager content) {
    Inventory.LoadContent(content);
  }

  public void Update(double deltaTime) {
    Velocity = Vector2.Zero;

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

    if (activeDirX == -1) StateMachine.CurrentState.MoveLeft();
    if (activeDirX == 1) StateMachine.CurrentState.MoveRight();
    if (activeDirY == -1) StateMachine.CurrentState.MoveUp();
    if (activeDirY == 1) StateMachine.CurrentState.MoveDown();

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
    if (Velocity != Vector2.Zero) Velocity = Vector2.Normalize(Velocity) * Constants.PLAYER_SPEED;
    StateMachine.CurrentState.Update(deltaTime);

    inputLeftLastFrame = inputLeftThisFrame;
    inputRightLastFrame = inputRightThisFrame;
    inputUpLastFrame = inputUpThisFrame;
    inputDownLastFrame = inputDownThisFrame;
    inputLeftThisFrame = false;
    inputRightThisFrame = false;
    inputUpThisFrame = false;
    inputDownThisFrame = false;
  }

  public void Draw(SpriteBatch spriteBatch) {
    StateMachine.CurrentState.Draw(spriteBatch);
  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      Position = CollisionHelper.GetNudgedPosition(info, Position, info.Overlap + 0.01f);
    }

    if (info.Collider is IEnemy) {
      TakeDamage(Constants.ENEMY_CONTACT_DAMAGE);
    }
  }
}
