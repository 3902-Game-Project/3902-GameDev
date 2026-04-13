using GameProject.Blocks;
using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items.Utility;

public class KeyItem(Texture2D keyTexture, Vector2 startPosition, ILevelManager levelManager) : IItem {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  private Rectangle sourceRectangle = new(17, 448, 7, 13);
  private Vector2 origin;
  private ILevelManager levelManagers = levelManager;
  public Vector2 Position { get; set; } = startPosition;
  public bool IsCollected { get; set; } = false;
  public ItemCategory Category { get; } = ItemCategory.Consumable;

  public void Draw(SpriteBatch spriteBatch) {
    if (!IsCollected) {
      origin = new Vector2(sourceRectangle.Width, sourceRectangle.Height);  // removed " / 2"

      spriteBatch.Draw(
        keyTexture,
        Position,
        sourceRectangle,
        Color.White,
        0f,
        origin,
        2f,
        SpriteEffects.None,
        0f
      );
    }
  }

  public void Update(GameTime gameTime) {
    // if key is picked up, stop drawing key
  }

  public void Use(UseType useType) {
    foreach (var block in levelManagers.CurrentLevel.CollidableBlocks) {
      if (block is VaultDoorBlock vaultDoorBlock) {
        vaultDoorBlock.ChangeState(BlockState.opening);
      } else if (block is SlattedDoorBlock slattedDoorBlock) {
        slattedDoorBlock.ChangeState(BlockState.open);
      }
    }
    // DELETE ITEM
  }
  public void OnPickup(Player player) {
    IsCollected = true;
    //Player.Inventory.PickUpItem(this);
    //levelManager.CurrentLevel.Pickups.Remove(this); in player class
  }
}
