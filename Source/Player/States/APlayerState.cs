using GameProject.Controllers;
using GameProject.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal abstract class APlayerState(Player player) : IPlayerState {
  public Player Player { get; } = player;

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
      Player.DamageFlashTimer = Player.DAMAGE_FLASH_DURATION;
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

  public abstract void Update(double deltaTime);
  public abstract void Draw(SpriteBatch spriteBatch);
}
