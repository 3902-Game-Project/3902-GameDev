using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Source.Items.Utility;

public class KeyItem : IItem, IWorldPickup {
  // add collision info
    public FacingDirection Direction { get; set; } = FacingDirection.Right;
    private Rectangle sourceRectangle = new(0, 1344, 21, 39); // CHANGE
    private Texture2D texture;
    private Vector2 origin;
    private ILevelManager levelManager;
    public Vector2 Position { get; set; }
    public ItemCategory Category { get; } = ItemCategory.Consumable;

  public KeyItem(Texture2D keyTexture, Vector2 startPosition, ILevelManager levelManagers) {
    this.texture = keyTexture;
    Position = startPosition;
    this.levelManager = levelManagers;
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) { 
    // if key is picked up, stop drawing key
  }

  public void Use(UseType useType) {
    // Logic for using the whiskey item
  }
  public void OnPickup(Player player) {
    // if player collides with key, put key in inventory
  }
}
