using GameProject.Commands;
using GameProject.Globals;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;

namespace GameProject.Blocks;

internal class BlockFactory {
  /* Floor Blocks */
  internal static IBlock CreateSandBlockSprite(float x, float y) {
    return new SandBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateRedSandBlockSprite(float x, float y) {
    return new RedSandBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateWoodPlankBlockSprite(float x, float y) {
    return new WoodPlankBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }

  /* Wall Blocks */
  internal static IBlock CreateLogBlockSprite(float x, float y) {
    return new LogBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateLogCornerBlockSprite(float x, float y) {
    return new LogCornerBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateRockBlockSprite(float x, float y) {
    return new RockBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateRockCornerBlockSprite(float x, float y) {
    return new RockCornerBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }

  /* Doors */
  internal static IBlock CreateRockHoleBlockSprite(float x, float y, string pairedLevelName, ChangeLevelCallback changeLevelCallback) {
    return new RockHoleBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), new ChangeLevelCommand(changeLevelCallback, pairedLevelName));
  }
  internal static IBlock CreateSmallDoorBlockSprite(float x, float y, LockableDoorBlockState state, string pairedLevelName, ChangeLevelCallback changeLevelCallback) {
    return new SmallDoorBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), state, new ChangeLevelCommand(changeLevelCallback, pairedLevelName));
  }
  internal static IBlock CreateVaultDoorBlockSprite(float x, float y, VaultDoorBlockState state, string pairedLevelName, ChangeLevelCallback changeLevelCallback) {
    return new VaultDoorBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), state, new ChangeLevelCommand(changeLevelCallback, pairedLevelName));
  }
  internal static IBlock CreateSlattedDoorSprite(float x, float y, LockableDoorBlockState state, string pairedLevelName, ChangeLevelCallback changeLevelCallback) {
    return new SlattedDoorBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), state, new ChangeLevelCommand(changeLevelCallback, pairedLevelName));
  }

  /* Object Blocks */
  internal static IBlock CreateTreasureBlockSprite(float x, float y) {
    return new TreasureBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateBarrelBlockSprite(float x, float y) {
    return new BarrelBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateCrateBlockSprite(float x, float y) {
    return new CrateBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateBarShelfBlockSprite(float x, float y) {
    return new BarShelfBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateShelfBlockSprite(float x, float y) {
    return new ShelfBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y + 27f));
  }
  internal static IBlock CreateBankShelfBlockSprite(float x, float y) {
    return new BankShelfBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateTellersDeskBlockSprite(float x, float y) {
    return new TellersDeskBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateFirePitBlockSprite(float x, float y) {
    return new FirePitBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateFireBlockSprite(float x, float y, Player player) {
    return new FireBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y), player);
  }
  internal static IBlock CreateMudBlockSprite(float x, float y) {
    return new MudBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateStoolBlockSprite(float x, float y) {
    return new StoolBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x + 21f, y + 28f));
  }
  internal static IBlock CreateTableBlockSprite(float x, float y) {
    return new TableBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y + 16f));
  }
  internal static IBlock CreateStatueBlockSprite(float x, float y) {
    return new StatueBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
  internal static IBlock CreateWindowBlockSprite(float x, float y) {
    return new WindowBlock(TextureStore.Instance.MainBlockItemAtlas, new Vector2(x, y));
  }
}
