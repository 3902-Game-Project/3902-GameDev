using GameProject.Controllers;
using GameProject.Factories;
using GameProject.FireModes;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class FakeBFGItem : ABaseGun {
  private readonly ILevelManager myLevelManager;

  public FakeBFGItem(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats)
    : base(texture, startPosition, player, levelManager, stats) {
    Category = ItemCategory.Primary;
    sourceRectangle = new(0, 0, 40, 20);
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2f, 0) * scale;
    fireMode = new SemiAutoFire(this.stats);
    myLevelManager = levelManager;
  }

  public override void Use(UseType useType) {
    if (EquipTimer > 0) return;

    if (fireMode != null && fireMode.CanFire(useType)) {

      myLevelManager.CurrentLevel.ProjectileManager.Add(ProjectileFactory.Instance.CreateBang(this));

      SoundManager.Instance.Play(SoundID.GunshotDefault);
    }
  }
}
