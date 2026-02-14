using GameProject.Blocks2;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class BlockSpriteFactory {
  private Texture2D blockTextures;

  private static BlockSpriteFactory instance;
  public static BlockSpriteFactory Instance {
    get { return instance; }
  }

  private BlockSpriteFactory() {
    instance = new BlockSpriteFactory();
  }

  public void LoadAllTextures(ContentManager content) {
    blockTextures = content.Load<Texture2D>("desert-atlas-v1");
  }

  public IBlock CreateSandBlockSprite() {
    return new SandBlock(blockTextures, new Vector2(300, 200));
  }
}
