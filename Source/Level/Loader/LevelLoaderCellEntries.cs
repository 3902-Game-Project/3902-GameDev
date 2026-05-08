using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.Items;

namespace GameProject.Level.Loader;

internal class LevelLoaderCellEntries {
  internal static readonly Dictionary<string, CellEntryParseFunc> CELL_ENTRY_FUNCS = new() {
    { "", LevelLoaderCreatorFuncs.CreateEmptyCreator() }, /* empty, do nothing */
    { "0", LevelLoaderCreatorFuncs.CreateEmptyCreator() }, /* empty, do nothing */
    { "1-0", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateLogBlockSprite) }, /* log: wall */
    { "1-1", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateLogCornerBlockSprite) }, /* log: corner */
    { "2", LevelLoaderCreatorFuncs.CreateCollidableLockableDoorBlockCreator(BlockFactory.CreateSmallDoorBlockSprite) }, /* small door */
    { "3", LevelLoaderCreatorFuncs.CreatePlayerPositionSetter() }, /* player position */
    { "4", LevelLoaderCreatorFuncs.CreateEnemyCreator(EnemyFactory.Instance.CreateSnakeSprite) }, /* snake */
    { "5", LevelLoaderCreatorFuncs.CreateNonCollidableBlockCreator(BlockFactory.CreateSandBlockSprite) }, /* sand */
    { "6", LevelLoaderCreatorFuncs.CreateNonCollidableBlockCreator(BlockFactory.CreateRedSandBlockSprite) }, /* red sand */
    { "7", LevelLoaderCreatorFuncs.CreateNonCollidableBlockCreator(BlockFactory.CreateWoodPlankBlockSprite) }, /* wood plank */
    { "8-0", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateRockBlockSprite) }, /* rock: wall */
    { "8-1", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateRockCornerBlockSprite) }, /* rock: corner */
    /* 8-2: omitted */
    { "8-3", LevelLoaderCreatorFuncs.CreateCollidableHoleBlockCreator(BlockFactory.CreateRockHoleBlockSprite) }, /* rock: hole to other room */
    { "9", LevelLoaderCreatorFuncs.CreateFiringItemEnemyCreator(EnemyFactory.Instance.CreateShotgunnerSprite) }, /* shotgunner */
    { "10", LevelLoaderCreatorFuncs.CreateEnemyCreator(EnemyFactory.Instance.CreateBatSprite) }, /* bat */
    { "11", LevelLoaderCreatorFuncs.CreateFiringItemEnemyCreator(EnemyFactory.Instance.CreateRiflemanSprite) }, /* rifleman */
    { "12", LevelLoaderCreatorFuncs.CreateEnemyCreator(EnemyFactory.Instance.CreateTumbleweedSprite) }, /* tumbleweed */
    { "13", LevelLoaderCreatorFuncs.CreateEnemyCreator(EnemyFactory.Instance.CreateCactusSprite) }, /* cactus */
    { "14", LevelLoaderCreatorFuncs.CreateGunItemCreator(ItemFactory.Instance.CreateRevolver) }, /* revolver */
    { "15", LevelLoaderCreatorFuncs.CreateGunItemCreator(ItemFactory.Instance.CreateRifle) }, /* rifle */
    { "16", LevelLoaderCreatorFuncs.CreateGunItemCreator(ItemFactory.Instance.CreateShotgun) }, /* shotgun */
    { "17", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateBarrelBlockSprite) }, /* barrel */
    { "18", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateBarShelfBlockSprite) }, /* bar shelf */
    { "19", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateShelfBlockSprite) }, /* shelf */
    { "20", LevelLoaderCreatorFuncs.CreateCollidableVaultDoorBlockCreator(BlockFactory.CreateVaultDoorBlockSprite) }, /* vault door */
    { "21", LevelLoaderCreatorFuncs.CreateKeyItemCreator(ItemFactory.CreateKey) }, /* key item */
    { "22", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateFirePitBlockSprite) }, /* fire pit */
    { "23", LevelLoaderCreatorFuncs.CreateCollidablePlayerBlockCreator(BlockFactory.CreateFireBlockSprite) }, /* fire */
    /* 24: omitted */
    { "25", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateMudBlockSprite) }, /* mud */
    { "26", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateCrateBlockSprite) }, /* crate */
    { "27", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateStoolBlockSprite) }, /* stool */
    { "28", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateTableBlockSprite) }, /* table */
    { "29", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateStatueBlockSprite) }, /* statue */
    { "30", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateWindowBlockSprite) }, /* window */
    { "31", LevelLoaderCreatorFuncs.CreateCollidableLockableDoorBlockCreator(BlockFactory.CreateSlattedDoorSprite) }, /* slatted door */
    { "32", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateTreasureBlockSprite) }, /* treasure block */
    { "33", LevelLoaderCreatorFuncs.CreateAmmoItemCreator(WorldPickupFactory.CreateAmmo) }, /* ammo item */
    { "34", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateBankShelfBlockSprite) }, /* bank shelf */
    { "35", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateTellersDeskBlockSprite) }, /* tellers desk */
    { "36", LevelLoaderCreatorFuncs.CreatePlayerItemCreator(ItemFactory.Instance.CreateHealthPotion) }, /* health potion item */
    { "37", LevelLoaderCreatorFuncs.CreatePlayerItemCreator(ItemFactory.Instance.CreateInvincibilityPotion) }, /* invincibility potion item */
    { "38", LevelLoaderCreatorFuncs.CreatePlayerItemCreator(ItemFactory.Instance.CreateInfiniteAmmoPotion) }, /* infinite ammo potion item */
    { "39", LevelLoaderCreatorFuncs.CreateFiringEnemyCreator(EnemyFactory.CreateBossSprite) }, /* boss */
  };
}
