using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.Misc;
using GameProject.PlayerStates;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject;

public enum FacingDirection {
  Left,
  Right
}

public class Player : ICollidable {
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; }
  public Layer Mask { get; } = Layer.Environment;
  public Layer Layer { get; } = Layer.Player;
  public Game1 game { get; private set; }
  public IPlayerState State { get; set; } // Current state
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Speed { get; set; } = 200f;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public PlayerInventory Inventory { get; private set; }

  public Texture2D Texture { get; private set; }
  public Rectangle BoundingBox {
    get {
      int width = (int)(171 * 0.2f);
      int height = (int)(323 * 0.2f);
      int x = (int)Position.X - (width / 2);
      int y = (int)Position.Y - (height / 2);
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
  public void UseItem(UseType useType) => State.UseItem(useType);
  public void Die() => State.Die();

  public void LoadContent() {
    this.Texture = game.Assets.Textures.PlayerTexture;
  }

  public void TakeDamage() {
    //Todo: Need to change this to take damage instead of dying
    Die();
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    Position += Velocity * dt;
    State.Update(gameTime);
    Velocity = Vector2.Zero;

    if (Collider != null) {
      Collider.position = this.Position;
    }

    if (Inventory.ActiveItem != null) {
      Vector2 rightHandOffset = new Vector2(29, 32);
      Vector2 leftHandOffset = new Vector2(3, 36);

      Vector2 currentOffset = (Direction == FacingDirection.Right) ? rightHandOffset : leftHandOffset;
      Inventory.ActiveItem.Position = this.Position + currentOffset;
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
      Position += info.Direction * 2f;
      Collider.position = this.Position;
    }
    // ... handle enemy collisions here ...
  }

  public void Draw(SpriteBatch spriteBatch) {
    State.Draw(spriteBatch);

    if (Inventory.ActiveItem != null) {
      Inventory.ActiveItem.Draw(spriteBatch);
    }
  }
}
