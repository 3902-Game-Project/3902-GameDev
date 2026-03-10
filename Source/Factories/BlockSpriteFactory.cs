using GameProject.Blocks;
using GameProject.Interfaces;
using GameProject.Source.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

  public void LoadAllTextures(ContentManager content) {
    blockTextures = content.Load<Texture2D>("desert-atlas-v6");
  }

  /* Floor Blocks */
  public IBlock CreateSandBlockSprite(float x, float y) {
    return new SandBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateRedSandBlockSprite(float x, float y) {
    return new RedSandBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateWoodPlankBlockSprite(float x, float y) {
    return new WoodPlankBlock(blockTextures, new Vector2(x, y));
  }

  /* Wall Blocks */
  public IBlock CreateLogBlockSprite(float x, float y) {
    return new LogBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateLogCornerBlockSprite(float x, float y) {
    return new LogCornerBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateRockBlockSprite(float x, float y) {
    return new RockBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateRockCornerBlockSprite(float x, float y) {
    return new RockCornerBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateRedXRockBlockSprite(float x, float y) {
    return new RedXRockBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateRockHoleBlockSprite(float x, float y) {
    return new RockHoleBlock(blockTextures, new Vector2(x, y));
  }

  /* Doors */
  public IBlock CreateSmallDoorBlockSprite(float x, float y) {
    return new SmallDoorBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateLockedVaultBlockSprite(float x, float y) {
    return new LockedVaultBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateOpenVaultDoorBlockSprite(float x, float y) {
    return new OpenVaultDoorBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateLockedSlattedDoorSprite(float x, float y) {
    return new LockedSlattedDoorBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateOpenSlattedDoorSprite(float x, float y) {
    return new OpenSlattedDoorBlock(blockTextures, new Vector2(x, y));
  }

  /* Object Blocks */
  public IBlock CreateBarrelBlockSprite(float x, float y) {
    return new BarrelBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateCrateBlockSprite(float x, float y) {
    return new CrateBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateBarShelfBlockSprite(float x, float y) {
    return new BarShelfBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateShelfBlockSprite(float x, float y) {
    return new ShelfBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateFirePitBlockSprite(float x, float y) {
    return new FirePitBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateFireBlockSprite(float x, float y) {
    return new FireBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateLadderBlockSprite(float x, float y) {
    return new LadderBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateMudBlockSprite(float x, float y) {
    return new MudBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateStoolBlockSprite(float x, float y) {
    return new StoolBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateTableBlockSprite(float x, float y) {
    return new TableBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateStatueBlockSprite(float x, float y) {
    return new StatueBlock(blockTextures, new Vector2(x, y));
  }
  public IBlock CreateWindowBlockSprite(float x, float y) {
    return new WindowBlock(blockTextures, new Vector2(x, y));
  }
}
