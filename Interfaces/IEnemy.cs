using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces
{
  public interface IEnemy : ISprite {
    void ChangeDirection();
    void TakeDamage();
  }
}
