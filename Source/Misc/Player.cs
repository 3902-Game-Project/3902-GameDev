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

public class Player {
  public Game1 game { get; private set; }
  public IPlayerState State { get; set; } // Current state
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public float Speed { get; set; } = 200f;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public PlayerInventory Inventory { get; private set; }

  public Texture2D Texture { get; private set; }

  public BoxCollider Collider { get; private set; }

  public IPlayerState StaticState { get; private set; }
  public IPlayerState MovingState { get; private set; }
  public IPlayerState UseItemState { get; private set; }
  public IPlayerState DeadState { get; private set; }

  public Player(Game1 game) {
    this.game = game;
    this.Position = new Vector2(400, 300);
    this.Velocity = Vector2.Zero;
    this.Collider = new BoxCollider(new Vector2(34, 64), this.Position);
    this.Inventory = new PlayerInventory(this);

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
  public void UseItem() => State.UseItem();
  public void Die() => State.Die();

  public void LoadContent() {
    this.Texture = game.Assets.Textures.PlayerTexture;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    Position += Velocity * dt;
    State.Update(gameTime);
    Velocity = Vector2.Zero;
    Collider.position = this.Position;
  }

  public void Draw(SpriteBatch spriteBatch) {
    State.Draw(spriteBatch);
  }
}
