using GameProject.Blocks;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossDeathState : IEnemyState {
  private readonly Boss boss;
  private double animationTimer = 0.0;
  private bool isDoorUnlocked = false;

  public BossDeathState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;

    this.boss.CurrentSourceRectangles = [
      new(336, 203, 56, 50),
      new(392, 203, 56, 50),
      new(448, 203, 56, 50),
      new(504, 203, 56, 50),
      new(560, 203, 56, 50),
    ];
    this.boss.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    // 1. Play the death animation until the final frame
    if (boss.CurrentFrame < boss.CurrentSourceRectangles.Count - 1) {
      animationTimer += deltaTime;
      if (animationTimer >= 0.2) { // Adjust this number to speed up/slow down the animation
        boss.CurrentFrame++;
        animationTimer = 0;
      }
    }
    // 2. Once the animation finishes, stay on the last frame (dead body) and unlock the door!
    else if (!isDoorUnlocked) {
      UnlockGoldRoomDoor();
      isDoorUnlocked = true;
    }
  }

  private void UnlockGoldRoomDoor() {
    // Scans the room and forces the Vault Door open
    foreach (var block in boss.LevelManager.CurrentLevel.GetOpenableDoors()) {
      if (block is VaultDoorBlock vaultDoor) {
        vaultDoor.Unlock();
      }
    }
  }
}
