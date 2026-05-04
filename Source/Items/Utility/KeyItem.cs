using GameProject.Blocks;
using GameProject.Controllers;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal class KeyItem(Texture2D keyTexture, Vector2 startPosition, ILevelManager levelManager) : IItem, IWorldPickup {
  private static readonly Rectangle SOURCE_RECT = new(0, 448, 7, 13);
  
  private readonly ILevelManager levelManager = levelManager;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  public Vector2 Position { get; set; } = startPosition;
  public bool IsCollected { get; set; } = false;
  private Vector2 origin;
  public bool IsAutoCollect => false;
  public ItemCategory Category { get; } = ItemCategory.Consumable;
  public void OnEquip() { }
  public void OnUnequip() { }

  public void Draw(SpriteBatch spriteBatch) {
    if (!IsCollected) {
      origin = new Vector2(SOURCE_RECT.Width, SOURCE_RECT.Height);

      spriteBatch.Draw(
        keyTexture,
        Position,
        SOURCE_RECT,
        Color.White,
        0f,
        origin,
        2f,
        SpriteEffects.None,
        0f
      );
    }
  }

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
      texture: keyTexture,
      position: position,
      sourceRectangle: SOURCE_RECT,
      color: tint,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: scale,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }

  public void Update(double deltaTime) { }

  public void Use(UseType useType) {
    foreach (var block in levelManager.CurrentLevel.GetOpenableDoors()) {
      // Safely check if the block is a Vault Door
      if (block is VaultDoorBlock vaultDoor) {
        vaultDoor.Unlock(); // Explicitly trigger the door's built-in unlock method!
      } else if (block is SlattedDoorBlock slattedDoor) {
        slattedDoor.ChangeState(LockableDoorBlockState.Open);
      }
    }
  }

  public void OnPickup(Player player) {
    if (!IsCollected) {
      IsCollected = true;
      player.Inventory.PickupItem(this);
    }
  }
}
