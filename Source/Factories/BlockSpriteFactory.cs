using GameProject.Blocks;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

public class BlockSpriteFactory {
  // Publicly accessible since key item is part of block texture atlas
  public Texture2D MainAtlas { get; private set; }

  public static BlockSpriteFactory Instance { get; } = new();

  private BlockSpriteFactory() { }

  public void LoadAllTextures(ContentManager content) {
    MainAtlas = content.Load<Texture2D>("Misc/desert-atlas-v7");
  }

  /* Floor Blocks */
  public IBlock CreateSandBlockSprite(float x, float y) {
    return new SandBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateRedSandBlockSprite(float x, float y) {
    return new RedSandBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateWoodPlankBlockSprite(float x, float y) {
    return new WoodPlankBlock(MainAtlas, new Vector2(x, y));
  }

  /* Wall Blocks */
  public IBlock CreateLogBlockSprite(float x, float y) {
    return new LogBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateLogCornerBlockSprite(float x, float y) {
    return new LogCornerBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateRockBlockSprite(float x, float y) {
    return new RockBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateRockCornerBlockSprite(float x, float y) {
    return new RockCornerBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateRockRedXBlockSprite(float x, float y) {
    return new RedXRockBlock(MainAtlas, new Vector2(x, y));
  }

  /* Doors */
  public IBlock CreateRockHoleBlockSprite(float x, float y, string pairedLevelName, ILevelManager levelManager) {
    return new RockHoleBlock(MainAtlas, new Vector2(x, y), pairedLevelName, levelManager);
  }
  public IBlock CreateSmallDoorBlockSprite(float x, float y, BlockState state, string pairedLevelName, ILevelManager levelManager) {
    return new SmallDoorBlock(MainAtlas, new Vector2(x, y), state, pairedLevelName, levelManager);
  }
  public IBlock CreateVaultDoorBlockSprite(float x, float y, BlockState state, string pairedLevelName, ILevelManager levelManager) {
    return new VaultDoorBlock(MainAtlas, new Vector2(x, y), state, pairedLevelName, levelManager);
  }
  public IBlock CreateSlattedDoorSprite(float x, float y, BlockState state, string pairedLevelName, ILevelManager levelManager) {
    return new SlattedDoorBlock(MainAtlas, new Vector2(x, y), state, pairedLevelName, levelManager);
  }

  /* Object Blocks */
  public IBlock CreateBarrelBlockSprite(float x, float y) {
    return new BarrelBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateCrateBlockSprite(float x, float y) {
    return new CrateBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateBarShelfBlockSprite(float x, float y) {
    return new BarShelfBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateShelfBlockSprite(float x, float y) {
    return new ShelfBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateBankShelfBlockSprite(float x, float y) {
    return new BankShelfBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateTellersDeskBlockSprite(float x, float y) {
    return new TellersDeskBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateFirePitBlockSprite(float x, float y) {
    return new FirePitBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateFireBlockSprite(float x, float y) {
    return new FireBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateLadderBlockSprite(float x, float y) {
    return new LadderBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateMudBlockSprite(float x, float y) {
    return new MudBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateStoolBlockSprite(float x, float y) {
    return new StoolBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateTableBlockSprite(float x, float y) {
    return new TableBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateStatueBlockSprite(float x, float y) {
    return new StatueBlock(MainAtlas, new Vector2(x, y));
  }
  public IBlock CreateWindowBlockSprite(float x, float y) {
    return new WindowBlock(MainAtlas, new Vector2(x, y));
  }
}
