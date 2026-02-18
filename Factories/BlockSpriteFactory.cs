using GameProject.Blocks2;
using GameProject.Interfaces;
using GameProject.GameStates;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class BlockSpriteFactory {
  private Texture2D blockTextures;

  /*private static BlockSpriteFactory instance;
  public static BlockSpriteFactory Instance {
    get { return instance; }
  }*/

  public BlockSpriteFactory() {
    //instance = new BlockSpriteFactory();
  }

  public void LoadAllTextures(Game1 game) { // changed param from ContentManager content for sprint2 purposes
    blockTextures = game.Content.Load<Texture2D>("desert-atlas-v1");
    game.StateGame.Blocks.Add(new SandBlock(blockTextures, new Vector2(150, 300)));
    game.StateGame.Blocks.Add(new BarrelBlock(blockTextures, new Vector2(150, 300)));
    game.StateGame.Blocks.Add(new CrateBlock(blockTextures, new Vector2(150, 300))); 
  }

  public IBlock CreateSandBlockSprite() {
    return new SandBlock(blockTextures, new Vector2(150, 300));
  }
  public IBlock CreateBarrelBlockSprite() {
    return new BarrelBlock(blockTextures, new Vector2(150, 300));
  }
}
