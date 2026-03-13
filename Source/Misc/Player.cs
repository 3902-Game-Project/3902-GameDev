using GameProject.CollisionResponse;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.Misc;
using GameProject.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject;

public enum FacingDirection {
  Left,
  Right
}

public class Player : ICollidable {
  private static float PLAYER_WIDTH = 171.0f * 0.2f;
  private static float PLAYER_HEIGHT = 323.0f * 0.2f;

  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; }
  public Layer Mask { get; } = Layer.Environment;
  public Layer Layer { get; } = Layer.Player;
  public Game1 game { get; private set; }
  public IPlayerState State { get; set; } // Current state
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

  public Player(Game1 game) {
    this.game = game;
    this.Position = new Vector2(400, 300);
    this.Velocity = Vector2.Zero;
    this.Inventory = new PlayerInventory(this);

    float width = 171 * 0.2f;
    float height = 323 * 0.2f;
    this.Collider = new BoxCollider(width, height, this.Position);

    this.MovingState = new PlayerAnimatedMovingState(this);
    this.StaticState = new PlayerStaticState(this);
    this.UseItemState = new PlayerUseItemState(this);
    this.DeadState = new PlayerDeadState(this);
    this.State = StaticState;
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

  public void LoadContent() {
    this.Texture = game.Assets.Textures.PlayerTexture;
  }
  public void TakeDamage(int amount = 10) {
    if (!IsInvincible) {
      Health -= amount;

      invincibilityTimer = invincibilityDuration;

      if (Health <= 0) {
        Health = 0;
        Die();
      }
    }
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    if (invincibilityTimer > 0) {
      invincibilityTimer -= dt;
    }

    Position += Velocity * dt;
    State.Update(gameTime);
    Velocity = Vector2.Zero;

    if (Collider != null) {
      Collider.position = this.Position;
    }

    if (Inventory.ActiveItem != null) {
      float unscaledWidth = 171f;
      float unscaledHeight = 323f;
      Vector2 spriteCenter = new Vector2(unscaledWidth / 2f, unscaledHeight / 2f);
      float playerScale = 0.2f;
      Vector2 rightHandUnscaled = new Vector2(75f, 203f);
      Vector2 leftHandUnscaled = new Vector2(18f, 188f);
      Vector2 rightHandOffset = (rightHandUnscaled - spriteCenter) * playerScale;
      Vector2 leftHandOffset = (leftHandUnscaled - spriteCenter) * playerScale;
      Vector2 currentOffset = (Direction == FacingDirection.Right) ? rightHandOffset : leftHandOffset;
      Inventory.ActiveItem.Position = this.Position + currentOffset;
      Inventory.ActiveItem.Direction = this.Direction;

      Inventory.ActiveItem.Update(gameTime);
    }
  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock block) {
      if (info.Side == CollisionSide.Left || info.Side == CollisionSide.Right) {
        Velocity = new Vector2(0, Velocity.Y);
      } else if (info.Side == CollisionSide.Top || info.Side == CollisionSide.Bottom) {
        Velocity = new Vector2(Velocity.X, 0);
      }

      // Nudge player out of the wall
      Position += info.Direction * info.Overlap;
      Collider.position = this.Position;
    }
    if (info.Collider is IEnemy enemy) {
      float knockbackDistance = 100f;

      Position += info.Direction * knockbackDistance;
      Collider.position = this.Position;

      TakeDamage(50);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    State.Draw(spriteBatch);

    if (Inventory.ActiveItem != null) {
      Inventory.ActiveItem.Draw(spriteBatch);
    }
  }
}
