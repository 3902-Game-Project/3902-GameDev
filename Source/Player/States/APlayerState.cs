using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

#nullable enable

internal abstract class APlayerState(Player player) : IPlayerState {
  private protected Player Player { get; } = player;
  private protected double DamageFlashTimer { get; set; } = 0.0;
  private protected Color CurrentTintColor => DamageFlashTimer > 0 ? Color.Red : Color.White;

  internal abstract void MoveDown();
  internal abstract void MoveLeft();
  internal abstract void MoveRight();
  internal abstract void MoveUp();
  internal abstract void UseItem(UseType useType);
  internal abstract void UseKey(UseType useType);

  internal virtual void TakeDamage(int amount) {
    if (!Player.IsInvincible) {
      Player.ReduceHealth(amount);
      Player.SetInvincibility(Constants.PLAYER_INVINCIBILITY_DURATION);
      DamageFlashTimer = Constants.PLAYER_DAMAGE_FLASH_DURATION;
      if (Player.Health <= 0) {
        Die();
      }
      SoundManager.Instance.Play(SoundID.PlayerHurt);
    }
  }

  internal virtual void Die() {
    Player.StateMachine.ChangeState(Player.StateMachine.DeadState);
  }

  internal virtual void Interact() {
    Player.WantsToInteract = true;
  }

  internal virtual void Update(double deltaTime) {
    if (DamageFlashTimer > 0) {
      DamageFlashTimer -= deltaTime;
    }
  }

  internal abstract void Draw(SpriteBatch spriteBatch);
}
