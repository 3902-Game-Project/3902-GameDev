using GameProject.Enemies.BossStates;
using GameProject.Enemies.States;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Boss : ABaseEnemy {
  public ILevelManager LevelManager { get; }

  public Boss(Texture2D texture, Vector2 position, ILevelManager levelManager) : base(texture, position, 64f, 128f) {
    LevelManager = levelManager;
    DrawScale = 2.0f;
    FlipOnRightDir = false;
    MaxHealth = 1000;
    Health = 1000;

    CurrentState = new BossIdleState(this);
  }

  // Override TakeDamage to trigger the Hurt animation
  public override void TakeDamage(int damage) {
    if (Health <= 0) return;

    base.TakeDamage(damage);
    if (Health > 0 && CurrentState is not BossHurtState && CurrentState is not BossSpecialAttackState) {
      CurrentState = new BossHurtState(this);
    }
  }

  protected override void TransitionToDeathState() {
    CurrentState = new BossDeathState(this);
  }
}
