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
    DrawScale = 2.0f; // Bosses should be big!
    FlipOnRightDir = false;
    MaxHealth = 1000;
    Health = 1000;

    // Start in the idle state
    CurrentState = new BossIdleState(this);
  }

  protected override void DropLoot() {
    // Placeholder: Maybe drop a key or a lot of items later
  }

  protected override void TransitionToDeathState() {
    // Placeholder death animation frames
    CurrentState = new GenericDeathState(this, [new(0, 0, 64, 64), new(64, 0, 64, 64)]);
  }
}
