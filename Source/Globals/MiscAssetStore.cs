using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

internal class MiscAssetStore : IInitable {
  private static readonly float VIGNETTE_RADIUS = 200.0f;
  private static readonly float VIGNETTE_NONE_FRAC = 0.6f;
  private static readonly float VIGNETTE_FULL_FRAC = 1.4f;

  public static MiscAssetStore Instance { get; } = new MiscAssetStore();

  public SpriteFont MainFont { get; private set; }

  public Effect Vignette { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    MainFont = contentManager.Load<SpriteFont>("Misc/Main Font");

    Vignette = contentManager.Load<Effect>("Shaders/Vignette");
    Vignette.Parameters["VignetteDimensions"].SetValue(new Vector2(VIGNETTE_RADIUS, VIGNETTE_RADIUS) / new Vector2(Game1.GAME_WIDTH, Game1.GAME_HEIGHT));
    Vignette.Parameters["VignetteMaxTopLeft"].SetValue(new Vector2(0.0f, 0.0f));
    Vignette.Parameters["VignetteMaxBottomRight"].SetValue(new Vector2(1.0f, 1.0f));
    Vignette.Parameters["VignetteNoneDistSq"].SetValue(VIGNETTE_NONE_FRAC * VIGNETTE_NONE_FRAC);
    Vignette.Parameters["VignetteFullDistSq"].SetValue(VIGNETTE_FULL_FRAC * VIGNETTE_FULL_FRAC);
    Vignette.Parameters["VignetteColor"].SetValue(new Vector4(0.5f, 0.5f, 0.5f, 1.0f));
  }
}
