using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Interfaces;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;

namespace GameProject.Source.Collision;

public class CollisionHandler {
  public void HandleCollision(Player player, IBlock block, CollisionSide side) {
    //Player vs Block
    if (side == CollisionSide.Left || side == CollisionSide.Right) {
      player.Velocity = new Vector2(0, player.Velocity.Y);
    } else if (side == CollisionSide.Top || side == CollisionSide.Bottom) {
      player.Velocity = new Vector2(player.Velocity.X, 0);
    }

    Rectangle overlap = Rectangle.Intersect(player.BoundingBox, block.BoundingBox);
    if (side == CollisionSide.Right) player.Position = new Vector2(player.Position.X - overlap.Width, player.Position.Y);
    if (side == CollisionSide.Left) player.Position = new Vector2(player.Position.X + overlap.Width, player.Position.Y);
    if (side == CollisionSide.Bottom) player.Position = new Vector2(player.Position.X, player.Position.Y - overlap.Height);
    if (side == CollisionSide.Top) player.Position = new Vector2(player.Position.X, player.Position.Y + overlap.Height);
  }

  public void HandleCollision(Player player, IEnemy enemy, CollisionSide side) {
    //Player vs Enemy
    float knockback = 50f;

    if (side == CollisionSide.Left) player.Position = new Vector2(player.Position.X + knockback, player.Position.Y);
    if (side == CollisionSide.Right) player.Position = new Vector2(player.Position.X - knockback, player.Position.Y);
    if (side == CollisionSide.Top) player.Position = new Vector2(player.Position.X, player.Position.Y + knockback);
    if (side == CollisionSide.Bottom) player.Position = new Vector2(player.Position.X, player.Position.Y - knockback);

    //Add damage things
  }

  public void HandleCollision(IEnemy enemy, IBlock block, CollisionSide side) {
    // Enemy vs Block
    if (side == CollisionSide.Left || side == CollisionSide.Right) {
      enemy.Velocity = new Vector2(-enemy.Velocity.X, enemy.Velocity.Y);
      enemy.FacingDirection *= -1;
    } else {
      enemy.Velocity = new Vector2(enemy.Velocity.X, -enemy.Velocity.Y);
    }
  }

  public void HandleCollision(IProjectile bullet, IEnemy enemy) {
    // Bullet vs Enemy
    enemy.TakeDamage();
    //todo: bullet vanishes
  }
}
