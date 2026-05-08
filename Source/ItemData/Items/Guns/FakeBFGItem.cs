using GameProject.Controllers;
using GameProject.FireModes;
using GameProject.Misc;
using GameProject.PlayerSpace;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class FakeBFGItem : ABaseGun {
  private readonly ProjectileManagerGetter GetProjectileManager;

  internal FakeBFGItem(Texture2D texture, Vector2 startPosition, Player player, ProjectileManagerGetter GetProjectileManager, GunStats stats) :
    base(texture, startPosition, player, GetProjectileManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new(0, 0, 40, 20);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2.0f, 0.0f) * SCALE;
    fireMode = new SemiAutoFire(this.stats);
    this.GetProjectileManager = GetProjectileManager;
  }

  internal override void Use(UseType useType) {
    if (EquipTimer > 0) return;

    if (fireMode.CanFire(useType)) {
      GetProjectileManager().Add(ProjectileFactory.CreateBang(this));

      SoundManager.Instance.Play(SoundID.GunshotDefault);
    }
  }
}
