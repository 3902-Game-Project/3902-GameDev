using GameProject.Blocks;
using GameProject.Interfaces;
using GameProject.Source.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class BlockSpriteFactory {
  private static Texture2D blockTextures;

  /*private static BlockSpriteFactory instance;
  public static BlockSpriteFactory Instance {
    get { return instance; }
  }*/

  public BlockSpriteFactory() {
    //instance = new BlockSpriteFactory();
  }

  public void LoadAllTextures(Game1 game) { // changed param from ContentManager content for sprint2 purposes
    blockTextures = game.Content.Load<Texture2D>("desert-atlas-v6");

    game.StateGame.Blocks.Add(new LockedVaultBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new OpenVaultDoorBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new FirePitBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new SmallDoorBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new FireBlock(blockTextures, new Vector2(150, 100)));
  }

  /* Floor Blocks */
  public IBlock CreateSandBlockSprite(float x, float y) {
    return new SandBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateWoodPlankBlockSprite(float x, float y) {
    return new WoodPlankBlock(blockTextures, new Vector2(x, y));
  }

  /* Wall Blocks */
  public IBlock CreateRockBlockSprite(float x, float y) {
    return new RockBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateRockCornerBlockSprite(float x, float y) {
    return new RockCornerBlock(blockTextures, new Vector2(x, y));
  }

  /* Doors */
  public IBlock CreateSmallDoorBlockSprite(float x, float y) {
    return new SmallDoorBlock(blockTextures, new Vector2(x, y));
  }

  /* Object Blocks */
  public IBlock CreateBarrelBlockSprite(float x, float y) {
    return new BarrelBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateCrateBlockSprite(float x, float y) {
    return new CrateBlock(blockTextures, new Vector2(x, y));
  }

  // add other create methods...
}
