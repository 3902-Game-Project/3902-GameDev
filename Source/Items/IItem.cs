using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal interface IItem : ISprite {
  ItemCategory Category { get; }
  FacingDirection Direction { get; set; }
  Vector2 Position { get; set; }
  public void OnPickup(Player player);
  void Use(UseType useType);


  // NEW: Dedicated method for drawing in menus
  void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint);
  void OnEquip();
  void OnUnequip();
}
