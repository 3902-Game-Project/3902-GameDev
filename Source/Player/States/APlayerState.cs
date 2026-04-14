using GameProject.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal abstract class APlayerState : IPlayerState {
  public abstract void Die();
  public abstract void Draw(SpriteBatch spriteBatch);
  public abstract void MoveDown();
  public abstract void MoveLeft();
  public abstract void MoveRight();
  public abstract void MoveUp();
  public abstract void Update(GameTime gameTime);
  public abstract void UseItem(UseType useType);
  public abstract void UseKey(UseType useType);
}
