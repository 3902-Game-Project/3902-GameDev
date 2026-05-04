using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

internal class HUDManager(Player player) : IGPDrawable {
  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(
      texture: TextureStore.Instance.HUDBackground,
      position: Vector2.Zero,
      sourceRectangle: null,
      color: Color.DarkSlateGray,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );

    var healthBarPosition = new Vector2(20, 20);
    float healthPercent = MathHelper.Clamp(player.Health / 100f, 0f, 1f);
    spriteBatch.Draw(
      texture: TextureStore.Instance.HealthBar,
      position: healthBarPosition,
      sourceRectangle: null,
      color: Color.DarkSlateGray,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 0.5f,  //scale of blood bar
      effects: SpriteEffects.None,
      layerDepth: 0f
    );


    int visibleWidth = (int) (TextureStore.Instance.HealthBar.Width * healthPercent);
    Rectangle sourceRectangle = new(0, 0, visibleWidth, TextureStore.Instance.HealthBar.Height);
    spriteBatch.Draw(
      texture: TextureStore.Instance.HealthBar,
      position: healthBarPosition,
      sourceRectangle: sourceRectangle,
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 0.5f, //scale of the blood bar
      effects: SpriteEffects.None,
      layerDepth: 0f
    );

    //draw player's items, keys...
    float visualHealthBarWidth = TextureStore.Instance.HealthBar.Width * 0.5f;
    Vector2 ammoPosition = new(healthBarPosition.X + visualHealthBarWidth + 20, healthBarPosition.Y);
    var activeWeapon = player.Inventory.ActiveItem;

    if (activeWeapon != null) {
      // Much cleaner! If it's any gun inheriting from DefaultGun, it will grab the stats.
      if (activeWeapon is ABaseGun activeGun) {
        GunStats activeStats = activeGun.Stats;

        if (activeStats != null) {
          // Draw Current Gun's Ammo
          string ammoText = $"Ammo: {activeStats.CurrentAmmo} / {activeStats.MaxAmmo}";
          spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, ammoText, ammoPosition, Color.White);

          // Draw the 3 ammo types
          if (TextureStore.Instance.AmmoRefill != null) {
            float iconScale = 2f;
            float textOffset = 35f;
            float spacing = 90f;
            Vector2 reserveStartPos = ammoPosition + new Vector2(0, 30);

            // Light Ammo
            spriteBatch.Draw(TextureStore.Instance.AmmoRefill, reserveStartPos, new Rectangle(0, 0, 16, 16), Color.White, 0f, Vector2.Zero, iconScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, $"{player.Inventory.Ammo[AmmoType.Light]}", reserveStartPos + new Vector2(textOffset, 5), Color.White);

            // Heavy Ammo
            Vector2 heavyPos = reserveStartPos + new Vector2(spacing, 0);
            spriteBatch.Draw(TextureStore.Instance.AmmoRefill, heavyPos, new Rectangle(16, 0, 16, 16), Color.White, 0f, Vector2.Zero, iconScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, $"{player.Inventory.Ammo[AmmoType.Heavy]}", heavyPos + new Vector2(textOffset, 5), Color.White);

            // Shells
            Vector2 shellsPos = reserveStartPos + new Vector2(spacing * 2, 0);
            spriteBatch.Draw(TextureStore.Instance.AmmoRefill, shellsPos, new Rectangle(32, 0, 16, 16), Color.White, 0f, Vector2.Zero, iconScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(MiscAssetStore.Instance.MainFont, $"{player.Inventory.Ammo[AmmoType.Shells]}", shellsPos + new Vector2(textOffset, 5), Color.White);
          }
        }
      }
    }

    Rectangle[] keySourceRects = [
      new Rectangle(0, 448, 8, 13),
      new Rectangle(9, 448, 8, 13),
      new Rectangle(17, 448, 8, 14)
    ];
    Vector2 keysStartPosition = new(ammoPosition.X + 200, healthBarPosition.Y);
    float keyScale = 3f;

    int keyCount = 0;
    foreach (var item in player.Inventory.GeneralItems) {
      if (item is KeyItem) keyCount++;
    }
    int keysToDraw = MathHelper.Clamp(keyCount, 0, 3);

    for (int i = 0; i < keysToDraw; i++) {
      Vector2 keyPos = keysStartPosition + new Vector2(i * 35, 0);

      spriteBatch.Draw(
        texture: TextureStore.Instance.MainBlockItemAtlas,
        position: keyPos,
        sourceRectangle: keySourceRects[i],
        color: Color.White,
        rotation: 0f,
        origin: Vector2.Zero,
        scale: keyScale,
        effects: SpriteEffects.None,
        layerDepth: 0f
      );
    }

    Vector2 mapPosition = new(keysStartPosition.X + 100, keysStartPosition.Y - 10);
    spriteBatch.Draw(
      texture: TextureStore.Instance.MainBlockItemAtlas,
      position: mapPosition,
      sourceRectangle: new Rectangle(128, 448, 97, 39), // FIX
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 2f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
