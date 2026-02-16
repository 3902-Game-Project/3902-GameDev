using GameProject.Interfaces;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class ItemSpriteFactory {
  private Texture2D placeholderGunTexture;
  private static ItemSpriteFactory instance = new ItemSpriteFactory();

  public static ItemSpriteFactory Instance {
    get { return instance; }
  }

  private ItemSpriteFactory() {
  }

  public void LoadAllTextures(ContentManager content) {
    placeholderGunTexture = content.Load<Texture2D>("placeholderGunSpritesheet");
  }

  public IItem CreateRevolver() {
    return new Revolver(placeholderGunTexture, new Vector2(300, 300));
  }
}
