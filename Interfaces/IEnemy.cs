namespace GameProject.Interfaces;

public interface IEnemy : ISprite {
  void ChangeDirection();
  void TakeDamage();
}
