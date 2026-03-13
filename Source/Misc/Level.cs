using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Misc;

public class Level(
  Game1 game,
  List<IBlock> nonCollidableBlocks, // for non-collidable collidableBlocks -Aaron
  List<IBlock> collidableBlocks,
  List<IEnemy> enemies,
  List<IWorldPickup> pickups,
  Vector2 playerPosition
) : ILevel {
  public List<IBlock> CollidableBlocks => collidableBlocks;
  public List<IEnemy> Enemies => enemies;
  public Vector2 PlayerPosition { get; private set; } = playerPosition;

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    foreach (var nonCollidableBlocks in nonCollidableBlocks) {
      nonCollidableBlocks.Update(gameTime);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Update(gameTime);
    }

    foreach (var enemy in enemies) {
      enemy.Update(gameTime);
    }

    foreach (var pickup in pickups) {
      pickup.Update(gameTime);
    }
  }

  public void Draw(GameTime gameTime) {
    foreach (var nonCollidableBlock in nonCollidableBlocks) {
      nonCollidableBlock.Draw(game.SpriteBatch);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Draw(game.SpriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Draw(game.SpriteBatch);
    }

    foreach (var pickup in pickups) {
      pickup.Draw(game.SpriteBatch);
    }
  }

  public void AddPickup(IWorldPickup pickup) {
    pickups.Add(pickup);
  }
}
