using GameProject.Interfaces;
using GameProject.Items;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class ItemSpriteFactory (ProjectileManager projectileManager) {
  private Texture2D placeholderGunTexture;

  public void LoadAllTextures(ContentManager content) {
    placeholderGunTexture = content.Load<Texture2D>("basic_guns_spritesheet");
  }

  public IItem CreateRevolver() {
    return new Revolver(placeholderGunTexture, new Vector2(300, 300), projectileManager);
  }
}
