using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Enemies;
using GameProject.Items;
using GameProject.PlayerSpace;
using GameProject.Projectiles;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level.Loader;

internal delegate IBlock BlockCreationFunc(float x, float y);
internal delegate IBlock PlayerBlockCreationFunc(float x, float y, Player player);
internal delegate IBlock HoleBlockCreationFunc(float x, float y, string pairedLevelName, ChangeLevelCallback changeLevelCallback);
internal delegate IBlock LockableDoorBlockCreationFunc(float x, float y, LockableDoorBlockState state, string pairedLevelName, ChangeLevelCallback changeLevelCallback);
internal delegate IBlock VaultDoorBlockCreationFunc(float x, float y, VaultDoorBlockState state, string pairedLevelName, ChangeLevelCallback changeLevelCallback);
internal delegate IEnemy EnemyCreationFunc(float x, float y);
internal delegate IEnemy FiringEnemyCreationFunc(float x, float y, CurrentLevelGetter GetCurrentLevel);
internal delegate IEnemy FiringItemEnemyCreationFunc(float x, float y, CurrentLevelGetter GetCurrentLevel, Player player);
internal delegate IItem GunItemCreationFunc(float xPos, float yPos, Player player, ProjectileManagerGetter GetProjectileManager);
internal delegate IItem KeyItemCreationFunc(float xPos, float yPos, CurrentLevelGetter GetCurrentLevel);
internal delegate IItem PlayerItemCreationFunc(float xPos, float yPos, Player player);
internal delegate IWorldPickup AmmoItemCreationFunc(Vector2 position, AmmoType type, int amount);

internal delegate void CellEntryParseFunc(
  Player player,
  ILevelManager levelManager,
  ISet<string> levelNames,

  string type, // needed for error information
  string[] arguments,
  float xPos,
  float yPos,

  List<IBlock> nonCollidableBlocks,
  List<IBlock> collidableBlocks,
  List<IBlock> doors,
  List<IEnemy> enemies,
  List<IWorldPickup> pickups,
  ref Vector2? playerPositionNullable
);
