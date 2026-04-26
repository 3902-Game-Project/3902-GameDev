using GameProject.Controllers;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerUseItemState(Player player) : APlayerState(player) {
  private int timer = 20;

  private Rectangle SpriteRight = new(773, 56, 171, 323);
  private Rectangle SpriteLeft = new(1531, 420, 171, 323);
  private Rectangle SpriteUp = new(453, 425, 161, 322);
  private Rectangle SpriteDown = new(455, 58, 161, 318);
  public override void MoveUp() { }
  public override void MoveDown() { }
  public override void MoveLeft() { }
  public override void MoveRight() { }
  public override void UseItem(UseType useType) {
    Player.Inventory.ActiveItem?.Use(useType);
    timer = 20;
  }

  public override void UseKey(UseType useType) { }

  public override void Update(double deltaTime) {
    timer--;
    if (timer <= 0) {
      timer = 20;
      Player.State = Player.StaticState;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;

    if (Player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
    } else if (Player.Direction == FacingDirection.Left) {
      sourceRect = SpriteLeft;
    } else if (Player.Direction == FacingDirection.Up) {
      sourceRect = SpriteUp;
    } else {
      sourceRect = SpriteDown;
    }

    Vector2 origin = new(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: Player.CurrentTintColor,
      rotation: 0f,
      origin: origin,
      scale: 0.15f, // Keeps the scale consistent with other states
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
