using GameProject.Controllers;
using GameProject.Factories;
using GameProject.FireModes;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class FakeBFGItem : ABaseGun {
  private readonly ILevelManager levelManager;

  public FakeBFGItem(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats) :
    base(texture, startPosition, player, levelManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new(0, 0, 40, 20);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2.0f, 0.0f) * scale;
    fireMode = new SemiAutoFire(this.stats);
    this.levelManager = levelManager;
  }

  public override void Use(UseType useType) {
    if (EquipTimer > 0) return;

    if (fireMode.CanFire(useType)) {
      levelManager.CurrentLevel.ProjectileManager.Add(ProjectileFactory.Instance.CreateBang(this));

      SoundManager.Instance.Play(SoundID.GunshotDefault);
    }
  }
}
