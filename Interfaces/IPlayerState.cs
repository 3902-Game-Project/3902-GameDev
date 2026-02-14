using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces {
  public interface IPlayerState {
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void UseItem();
    // TODO: Add MoveUp(), MoveDown(), MoveLeft(), MoveRight() here
  }
}
