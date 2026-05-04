using GameProject.Controllers;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerUseItemState(Player player) : APlayerState(player) {
  private static readonly Rectangle SOURCE_RECT_RIGHT = new(773, 56, 171, 323);
  private static readonly Rectangle SOURCE_RECT_LEFT = new(1531, 420, 171, 323);
  private static readonly Rectangle SOURCE_RECT_UP = new(453, 425, 161, 322);
  private static readonly Rectangle SOURCE_RECT_DOWN = new(455, 58, 161, 318);

  private int timer = 20;

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
    base.Update(deltaTime);

    timer--;
    if (timer <= 0) {
      timer = 20;
      Player.ChangeState(Player.StaticState);
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;

    if (Player.Direction == FacingDirection.Right) {
      sourceRect = SOURCE_RECT_RIGHT;
    } else if (Player.Direction == FacingDirection.Left) {
      sourceRect = SOURCE_RECT_LEFT;
    } else if (Player.Direction == FacingDirection.Up) {
      sourceRect = SOURCE_RECT_UP;
    } else {
      sourceRect = SOURCE_RECT_DOWN;
    }

    Vector2 origin = new(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: CurrentTintColor,
      rotation: 0f,
      origin: origin,
      scale: 0.15f, // Keeps the scale consistent with other states
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
