using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Globals;

internal class MiscAssetStore : IInitable {
  public static MiscAssetStore Instance { get; } = new MiscAssetStore();

  public SpriteFont MainFont { get; private set; }

  public Effect Vignette { get; private set; }

  public void Initialize() { }

  public void LoadContent(ContentManager contentManager) {
    MainFont = contentManager.Load<SpriteFont>("Misc/Main Font");

    Vignette = contentManager.Load<Effect>("Shaders/Vignette");
    Vignette.Parameters["VignetteDimensions"].SetValue(new Vector2(Constants.VIGNETTE_RADIUS, Constants.VIGNETTE_RADIUS) / new Vector2(Constants.WINDOW_WIDTH, Constants.GAME_HEIGHT));
    Vignette.Parameters["VignetteMaxTopLeft"].SetValue(new Vector2(0.0f, 0.0f));
    Vignette.Parameters["VignetteMaxBottomRight"].SetValue(new Vector2(1.0f, 1.0f));
    Vignette.Parameters["VignetteNoneDistSq"].SetValue(Constants.VIGNETTE_NONE_FRAC * Constants.VIGNETTE_NONE_FRAC);
    Vignette.Parameters["VignetteFullDistSq"].SetValue(Constants.VIGNETTE_FULL_FRAC * Constants.VIGNETTE_FULL_FRAC);
    Vignette.Parameters["VignetteColor"].SetValue(new Vector4(0.5f, 0.5f, 0.5f, 1.0f));
  }
}
