using GameProject.Controllers;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal abstract class APlayerState(Player player) : IPlayerState {
  protected Player Player { get; } = player;

  protected double DamageFlashTimer { get; set; } = 0.0;

  protected Color CurrentTintColor => DamageFlashTimer > 0 ? Color.Red : Color.White;

  public abstract void MoveDown();
  public abstract void MoveLeft();
  public abstract void MoveRight();
  public abstract void MoveUp();
  public abstract void UseItem(UseType useType);
  public abstract void UseKey(UseType useType);

  public virtual void TakeDamage(int amount) {
    if (!Player.IsInvincible) {
      Player.Health -= amount;
      Player.InvincibilityTimer = Player.INVINCIBILITY_DURATION;
      DamageFlashTimer = Player.DAMAGE_FLASH_DURATION;
      if (Player.Health <= 0) {
        Player.Health = 0;
        Die();
      }
      SoundManager.Instance.Play(SoundID.PlayerHurt);
    }
  }

  public virtual void Die() {
    Player.ChangeState(Player.DeadState);
  }

  public virtual void Update(double deltaTime) {
    if (DamageFlashTimer > 0) {
      DamageFlashTimer -= deltaTime;
    }
  }

  public abstract void Draw(SpriteBatch spriteBatch);
}
