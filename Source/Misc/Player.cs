using GameProject.Interfaces;
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

  public IItem CurrentItem { get; set; }

  public Player(Game1 game) {
    this.game = game;
    this.Position = new Vector2(400, 300);
    this.Velocity = Vector2.Zero;

    // For now, attack state is default
    //this.State = new PlayerUseItemState(this);
    this.State = new PlayerStaticState(this);
  }

  public void MoveUp() {
    Velocity = new Vector2(Velocity.X, -Speed);
  }

  public void MoveDown() {
    Velocity = new Vector2(Velocity.X, Speed);
  }

  public void MoveLeft() {
    Velocity = new Vector2(-Speed, Velocity.Y);
    Direction = FacingDirection.Left;
  }

  public void MoveRight() {
    Velocity = new Vector2(Speed, Velocity.Y);
    Direction = FacingDirection.Right;
  }

  // TODO: Moving methods

  public void UseItem() {
    State.UseItem();
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    Position += Velocity * dt;
    State.Update(gameTime);
    Velocity = Vector2.Zero;
  }

  public void Draw(SpriteBatch spriteBatch) {
    State.Draw(spriteBatch);
  }
}
