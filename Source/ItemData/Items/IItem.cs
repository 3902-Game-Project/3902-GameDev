using GameProject.Controllers;
using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal interface IItem : ISprite {
  internal ItemCategory Category { get; }
  internal FacingDirection Direction { get; set; }
  internal Vector2 Position { get; set; }
  internal void OnPickup(Player player);
  internal void Use(UseType useType);
  // NEW: Dedicated method for drawing in menus:
  internal void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint);
  internal void OnEquip();
  internal void OnUnequip();
}
