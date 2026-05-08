using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Blocks;
using GameProject.Commands;
using GameProject.Enemies;
using GameProject.Globals;
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
    if (!flagString.StartsWith(FLAGS_LINE_START, StringComparison.Ordinal)) {
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
    if (LevelLoaderCellEntries.CELL_ENTRY_FUNCS.TryGetValue(type, out var EntryParseFunc)) {
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
