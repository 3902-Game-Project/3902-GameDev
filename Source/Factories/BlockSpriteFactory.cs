using GameProject.Blocks;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
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
    blockTextures = game.Content.Load<Texture2D>("desert-atlas-v6");
    game.StateGame.Blocks.Add(new SandBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new BarrelBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new CrateBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new MudBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new LadderBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new RockBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new WoodPlankBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new WoodStairBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new WoodCornerBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new LogBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new LogCornerBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new StatueBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new LockedVaultBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new FirePitBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new TableBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new StoolBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new CactusBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new WindowBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new ShelfBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new BarShelfBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new SmallDoorBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new LargeDoorBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new RedSandBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new RedXRockBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new RockHoleBlock(blockTextures, new Vector2(150, 100)));
    game.StateGame.Blocks.Add(new FireBlock(blockTextures, new Vector2(150, 100)));
  }

  public IBlock CreateSandBlockSprite(float x, float y) {
    return new SandBlock(blockTextures, new Vector2(x, y));
  }

  public IBlock CreateBarrelBlockSprite(float x, float y) {
    return new BarrelBlock(blockTextures, new Vector2(x, y));
  }

  public IBlock CreateCrateBlockSprite(float x, float y) {
    return new CrateBlock(blockTextures, new Vector2(x, y));
  }

  // add other create methods...
}
