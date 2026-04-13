using System;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Misc;
using GameProject.PlayerStates;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace;

public enum FacingDirection {
  Left,
  Right
}

public class Player : IInitable, ICollidable {
  private static readonly float PLAYER_WIDTH = 171.0f * 0.15f;
  private static readonly float PLAYER_HEIGHT = 323.0f * 0.15f;

  private readonly CollisionManager collisionManager;

  public IShape Shape => Collider;
  public ILevelManager LevelManager { get; private set; }
  public BoxCollider Collider { get; private set; }
  public Layer Mask { get; } = Layer.Environment;
  public Layer Layer { get; } = Layer.Player;
  public IPlayerState State { get; set; }
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Speed { get; set; } = 200f;
  public int Health { get; private set; } = 100;

  private float invincibilityTimer = 0f;
  private readonly float invincibilityDuration = 1.5f;
  public bool IsInvincible => invincibilityTimer > 0;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public PlayerInventory Inventory { get; private set; }
  public Texture2D Texture { get; private set; }

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

  public Player(CollisionManager collisionManager, ILevelManager levelManager, Game1 game) {
    this.collisionManager = collisionManager;
    LevelManager = levelManager;
    Position = new Vector2(400, 300);
    Velocity = Vector2.Zero;
    Inventory = new PlayerInventory(levelManager);

    float width = 171 * 0.15f;
    float height = 323 * 0.15f;
    Collider = new BoxCollider(width, height, Position);

    MovingState = new PlayerAnimatedMovingState(this);
    StaticState = new PlayerStaticState(this);
    UseItemState = new PlayerUseItemState(this);
    DeadState = new PlayerDeadState(this, game);
    State = StaticState;
  }

  public void MoveUp() => State.MoveUp();
  public void MoveDown() => State.MoveDown();
  public void MoveLeft() => State.MoveLeft();
  public void MoveRight() => State.MoveRight();
  public void UseItem(UseType useType) {
    if (Inventory.ActiveItem != null) {
      Inventory.ActiveItem.Use(useType);
      State.UseItem(useType);
    }
  }
  public void Die() => State.Die();

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    Texture = contentManager.Load<Texture2D>("Misc/playerSpritesheet");
  }

  public void TakeDamage(int amount = 10) {
    if (!IsInvincible) {
      Health -= amount;
      invincibilityTimer = invincibilityDuration;
      if (Health <= 0) {
        Health = 0;
        Die();
      }
      SoundManager.Instance.Play(SoundID.PlayerHurt);
    }
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    if (invincibilityTimer > 0) invincibilityTimer -= dt;
    if (Velocity != Vector2.Zero) Velocity = Vector2.Normalize(Velocity) * Speed;
    float xStep = Velocity.X * dt;
    float yStep = Velocity.Y * dt;

    Position = new Vector2(Position.X + xStep, Position.Y);
    if (Collider != null) Collider.Position = Position;
    collisionManager.ResolveCollisionsFor(this, CollisionAxis.X, MathF.Abs(yStep) + 1f);

    Position = new Vector2(Position.X, Position.Y + yStep);
    if (Collider != null) Collider.Position = Position;
    collisionManager.ResolveCollisionsFor(this, CollisionAxis.Y, MathF.Abs(xStep) + 1f);

    State.Update(gameTime);
    Velocity = Vector2.Zero;

    if (Inventory.ActiveItem != null) {
      float unscaledWidth = 171f;
      float unscaledHeight = 323f;
      Vector2 spriteCenter = new(unscaledWidth / 2f, unscaledHeight / 2f);
      float playerScale = 0.15f;
      Vector2 rightHandUnscaled = new(100f, 195f);
      Vector2 leftHandUnscaled = new(18f, 188f);
      Vector2 rightHandOffset = (rightHandUnscaled - spriteCenter) * playerScale;
      Vector2 leftHandOffset = (leftHandUnscaled - spriteCenter) * playerScale;
      Vector2 currentOffset = (Direction == FacingDirection.Right) ? rightHandOffset : leftHandOffset;

      Inventory.ActiveItem.Position = Position + currentOffset;
      Inventory.ActiveItem.Direction = Direction;
      Inventory.ActiveItem.Update(gameTime);
    }
  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      Position += info.Direction * (info.Overlap + 0.01f);

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
      if (pickup is BaseWorldPickup basePickup) {
        float distance = Vector2.Distance(Position, basePickup.Position);
        if (distance < grabRange && distance < closestDistance) {
          closestDistance = distance;
          closestPickup = pickup;
        }
      }
    }

    if (closestPickup != null) {
      closestPickup.OnPickup(this);
      LevelManager.CurrentLevel.RemovePickup(closestPickup);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    State.Draw(spriteBatch);

    Inventory.ActiveItem?.Draw(spriteBatch);
  }
}
