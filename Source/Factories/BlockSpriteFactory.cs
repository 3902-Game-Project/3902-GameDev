using GameProject.Blocks;
using GameProject.Globals;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Factories;

internal class BlockSpriteFactory {
  public static BlockSpriteFactory Instance { get; } = new();

  private BlockSpriteFactory() { }

  /* Floor Blocks */
  public static IBlock CreateSandBlockSprite(float x, float y) {
    return new SandBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateRedSandBlockSprite(float x, float y) {
    return new RedSandBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateWoodPlankBlockSprite(float x, float y) {
    return new WoodPlankBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }

  /* Wall Blocks */
  public static IBlock CreateLogBlockSprite(float x, float y) {
    return new LogBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateLogCornerBlockSprite(float x, float y) {
    return new LogCornerBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateRockBlockSprite(float x, float y) {
    return new RockBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateRockCornerBlockSprite(float x, float y) {
    return new RockCornerBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateRockRedXBlockSprite(float x, float y) {
    return new RedXRockBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }

  /* Doors */
  public static IBlock CreateRockHoleBlockSprite(float x, float y, string pairedLevelName, ILevelManager levelManager) {
    return new RockHoleBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), pairedLevelName, levelManager);
  }
  public static IBlock CreateSmallDoorBlockSprite(float x, float y, LockableDoorBlockState state, string pairedLevelName, ILevelManager levelManager) {
    return new SmallDoorBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), state, pairedLevelName, levelManager);
  }
  public static IBlock CreateVaultDoorBlockSprite(float x, float y, VaultDoorBlockState state, string pairedLevelName, ILevelManager levelManager) {
    return new VaultDoorBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), state, pairedLevelName, levelManager);
  }
  public static IBlock CreateSlattedDoorSprite(float x, float y, LockableDoorBlockState state, string pairedLevelName, ILevelManager levelManager) {
    return new SlattedDoorBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), state, pairedLevelName, levelManager);
  }

  /* Object Blocks */
  public static IBlock CreateTreasureBlockSprite(float x, float y) {
    return new TreasureBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateBarrelBlockSprite(float x, float y) {
    return new BarrelBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateCrateBlockSprite(float x, float y) {
    return new CrateBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateBarShelfBlockSprite(float x, float y) {
    return new BarShelfBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateShelfBlockSprite(float x, float y) {
    return new ShelfBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateBankShelfBlockSprite(float x, float y) {
    return new BankShelfBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateTellersDeskBlockSprite(float x, float y) {
    return new TellersDeskBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateFirePitBlockSprite(float x, float y) {
    return new FirePitBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateFireBlockSprite(float x, float y) {
    return new FireBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateLadderBlockSprite(float x, float y) {
    return new LadderBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateMudBlockSprite(float x, float y) {
    return new MudBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateStoolBlockSprite(float x, float y) {
    return new StoolBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateTableBlockSprite(float x, float y) {
    return new TableBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateStatueBlockSprite(float x, float y) {
    return new StatueBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  public static IBlock CreateWindowBlockSprite(float x, float y) {
    return new WindowBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
}
