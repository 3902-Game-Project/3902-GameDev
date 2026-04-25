using Microsoft.Xna.Framework;

namespace GameProject.Enemies;

internal interface IEnemyState {
  void Update(double deltaTime);
}
