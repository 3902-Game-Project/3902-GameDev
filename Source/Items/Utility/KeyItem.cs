using GameProject.Blocks;
using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items.Utility;

public class KeyItem(Texture2D keyTexture, Vector2 startPosition, ILevelManager levelManager) : IItem, IWorldPickup {
  private Rectangle sourceRectangle = new(17, 448, 7, 13);
  private readonly ILevelManager levelManagers = levelManager;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  public Vector2 Position { get; set; } = startPosition;
  public bool IsCollected { get; set; } = false;
  private Vector2 origin;
  public ItemCategory Category { get; } = ItemCategory.Consumable;

  public void Draw(SpriteBatch spriteBatch) {
    if (!IsCollected) {
      origin = new Vector2(sourceRectangle.Width, sourceRectangle.Height);

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

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
      texture: keyTexture,
      position: position,
      sourceRectangle: sourceRectangle,
      color: tint,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: scale,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }

  public void Update(GameTime gameTime) {
  }

  public void Use(UseType useType) {
    foreach (var block in levelManagers.CurrentLevel.Doors) {
      if (block is VaultDoorBlock vaultDoorBlock) {
        vaultDoorBlock.ChangeState(VaultDoorBlockState.Opening);
      } else if (block is SlattedDoorBlock slattedDoorBlock) {
        slattedDoorBlock.ChangeState(LockableDoorBlockState.Open);
      }
    }
  }

  public void OnPickup(Player player) {
    IsCollected = true;
  }
}
