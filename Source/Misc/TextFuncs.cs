using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal static class TextFuncs {
  private static void DrawCenteredTextLine(SpriteBatch spriteBatch, Vector2 position, string line, Color? color = null) {
    spriteBatch.DrawString(
      spriteFont: MiscAssetStore.Instance.MainFont,
      text: line,
      position: position,
      color: color ?? Color.White,
      origin: MiscAssetStore.Instance.MainFont.MeasureString(line) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
  }

  public static void DrawCenteredText(SpriteBatch spriteBatch, Vector2 position, string text, Color? color = null) {
    string[] lines = text.Split("\n");

    var lineDelta = new Vector2(0.0f, MiscAssetStore.Instance.MainFont.LineSpacing * 1.2f);
    var startPosition = position - lineDelta * (lines.Length - 1) * 0.5f;

    for (int i = 0; i < lines.Length; i++) {
      var linePosition = startPosition + lineDelta * i;

      DrawCenteredTextLine(spriteBatch, linePosition, lines[i], color);
    }
  }
}
