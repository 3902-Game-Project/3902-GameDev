using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface ICollider {
  bool CheckCollision(ICollider other);
}
