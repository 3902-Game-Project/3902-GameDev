using GameProject.Blocks2;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class BlockSpriteFactory {
  private Texture2D blockTextures;

  private static BlockSpriteFactory instance;
  public static BlockSpriteFactory Instance {
    get { return instance; }
  }

  public BlockSpriteFactory() {
    //instance = new BlockSpriteFactory();
  }

  public void LoadAllTextures(Game1 game) { // changed from ContentManager content for sprint2 purposes
    blockTextures = game.Content.Load<Texture2D>("desert-atlas-v1");
    game.Blocks.Add(CreateSandBlockSprite());

  }

  public IBlock CreateSandBlockSprite() {
    return new SandBlock(blockTextures, new Vector2(300, 200));
  }
}
