using GameProject.Controllers;
using GameProject.Managers;
using Microsoft.Xna.Framework;
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
    if (!player.IsInvincible) {
      player.Health -= amount;
      player.InvincibilityTimer = Player.INVINCIBILITY_DURATION;
      player.DamageFlashTimer = Player.DAMAGE_FLASH_DURATION;
      if (player.Health <= 0) {
        player.Health = 0;
        Die();
      }
      SoundManager.Instance.Play(SoundID.PlayerHurt);
    }
  }

  public virtual void Die() {
    Player.State = Player.DeadState;
  }

  public abstract void Update(GameTime gameTime);
  public abstract void Draw(SpriteBatch spriteBatch);
}
