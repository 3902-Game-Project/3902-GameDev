using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Blocks;
using GameProject.Commands;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.Globals;
using GameProject.Items;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level.Loader;

internal partial class LevelLoader {
  private static readonly string FLAGS_LINE_START = "Flags: ";

  [GeneratedRegex(@"\r?\n")]
  private static partial Regex NewlineSplitRegex { get; }

  public static readonly float PLAYER_LEFT_BOUNDARY_THRESHOLD = Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_TOP_BOUNDARY_THRESHOLD = Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_RIGHT_BOUNDARY_THRESHOLD = Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_BOTTOM_BOUNDARY_THRESHOLD = Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_LEFT_POS_AFTER_TELEPORT = Constants.BASE_BLOCK_WIDTH * 1.5f;
  public static readonly float PLAYER_TOP_POS_AFTER_TELEPORT = Constants.BASE_BLOCK_WIDTH * 1.5f;
  public static readonly float PLAYER_RIGHT_POS_AFTER_TELEPORT = Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH * 1.5f;
  public static readonly float PLAYER_BOTTOM_POS_AFTER_TELEPORT = Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_WIDTH * 1.5f;

  private static readonly Dictionary<string, CellEntryParseFunc> CELL_ENTRY_FUNCS = new() {
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
    { "33", LevelLoaderCreatorFuncs.CreateAmmoItemCreator(WorldPickupFactory.Instance.CreateAmmo) }, /* ammo item */
    { "34", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateBankShelfBlockSprite) }, /* bank shelf */
    { "35", LevelLoaderCreatorFuncs.CreateCollidableBlockCreator(BlockFactory.CreateTellersDeskBlockSprite) }, /* tellers desk */
    { "36", LevelLoaderCreatorFuncs.CreatePlayerItemCreator(ItemFactory.Instance.CreateHealthPotion) }, /* health potion item */
    { "37", LevelLoaderCreatorFuncs.CreatePlayerItemCreator(ItemFactory.Instance.CreateInvincibilityPotion) }, /* invincibility potion item */
    { "38", LevelLoaderCreatorFuncs.CreatePlayerItemCreator(ItemFactory.Instance.CreateInfiniteAmmoPotion) }, /* infinite ammo potion item */
    { "39", LevelLoaderCreatorFuncs.CreateFiringEnemyCreator(EnemyFactory.Instance.CreateBossSprite) }, /* boss */
  };

  private static void ParseSingleFlag(LevelFlags flags, string flag) {
    switch (flag) {
      case "":
      case "None":
        /* do nothing */
        break;

      case "Cave":
        flags.Cave = true;
        break;

      case "VictoryLevel":
        flags.VictoryLevel = true;
        break;

      default:
        throw new FormatException($"unrecognized level flag: {flag}");
    }
  }

  private static LevelFlags ParseFlags(string flagString) {
    if (!flagString.StartsWith(FLAGS_LINE_START)) {
      throw new FormatException("level data flags entry invalid");
    }

    var flagSplit = flagString[FLAGS_LINE_START.Length..].Split(',');

    LevelFlags flags = new();

    foreach (var flagUntrimmed in flagSplit) {
      var flag = flagUntrimmed.Trim();

      ParseSingleFlag(flags, flag);
    }

    return flags;
  }

  private static void AddCellEntry(
    Player player,
    ILevelManager levelManager,
    ISet<string> levelNames,

    string type,
    string[] arguments,
    float xPos,
    float yPos,

    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IBlock> doors,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    ref Vector2? playerPositionNullable
  ) {
    if (CELL_ENTRY_FUNCS.TryGetValue(type, out var EntryParseFunc)) {
      EntryParseFunc(
        player,
        levelManager,
        levelNames,

        type,
        arguments,
        xPos,
        yPos,

        nonCollidableBlocks,
        collidableBlocks,
        doors,
        enemies,
        pickups,
        ref playerPositionNullable
      );
    } else {
      throw new FormatException($"unrecognized level block/entity type '{type}'");
    }
  }

  public static void FromStringCell(
    Player player,
    ILevelManager levelManager,
    ISet<string> levelNames,

    string cell,
    int rowIndex,
    int colIndex,

    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IBlock> doors,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    ref Vector2? playerPositionNullable
  ) {
    float xPos = Constants.BASE_BLOCK_WIDTH * colIndex;
    float yPos = Constants.BASE_BLOCK_HEIGHT * rowIndex;

    var cellSplit = cell.Split(';');

    foreach (var entry in cellSplit) {
      var entrySplit = entry.Trim().Split(':');
      var type = entrySplit[0];
      var arguments = entrySplit[1..];

      AddCellEntry(
        player,
        levelManager,
        levelNames,

        type,
        arguments,
        xPos,
        yPos,

        nonCollidableBlocks,
        collidableBlocks,
        doors,
        enemies,
        pickups,
        ref playerPositionNullable
      );
    }
  }

  public static Level FromString(Player player, IGPCommand winScreenCommand, ILevelManager levelManager, ISet<string> levelNames, string levelDataString) {
    List<IBlock> nonCollidableBlocks = [];
    List<IBlock> collidableBlocks = [];
    List<IBlock> doors = [];
    List<IEnemy> enemies = [];
    List<IWorldPickup> pickups = [];
    Vector2? playerPositionNullable = null;

    var lines = NewlineSplitRegex.Split(levelDataString.Trim());

    if (lines.Length < 1) {
      throw new FormatException("level data does not contain a flags entry");
    }

    // parse flags

    var flags = ParseFlags(lines[0]);

    // parse level data

    var levelData = lines[2..].Select(line => line.Split(',')).ToArray();

    for (int rowIndex = 0; rowIndex < levelData.Length; rowIndex++) {
      var row = levelData[rowIndex];

      if (row.Length != levelData[0].Length) {
        throw new FormatException("line #" + (rowIndex + 1) + " length (" + row.Length + ") does not match first line length (" + levelData[0].Length + ")");
      }

      for (int colIndex = 0; colIndex < levelData[0].Length; colIndex++) {
        var cell = row[colIndex];

        FromStringCell(
          player,
          levelManager,
          levelNames,

          cell,
          rowIndex,
          colIndex,

          nonCollidableBlocks,
          collidableBlocks,
          doors,
          enemies,
          pickups,
          ref playerPositionNullable
        );
      }
    }

    if (playerPositionNullable is Vector2 playerPosition) {
      var level = new Level(
        levelFlags: flags,
        nonCollidableBlocks: nonCollidableBlocks,
        collidableBlocks: collidableBlocks,
        doors: doors,
        enemies: enemies,
        pickups: pickups,
        playerPosition: playerPosition,
        player: player,
        levelManager: levelManager,
        winScreenCommand: winScreenCommand
      );

      return level;
    } else {
      throw new FormatException("default player position was not set in level");
    }
  }
}
