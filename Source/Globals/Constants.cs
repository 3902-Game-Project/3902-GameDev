namespace GameProject.Globals;

internal static class Constants {
  /* base constants */
  public const float BASE_BLOCK_WIDTH = 64.0f;
  public const float BASE_BLOCK_HEIGHT = 64.0f;
  public const int LEVEL_WIDTH_BLOCKS = 15;
  public const int LEVEL_HEIGHT_BLOCKS = 9;
  public const float BASE_ENEMY_WIDTH = 64.0f;
  public const float BASE_ENEMY_HEIGHT = 64.0f;
  public const int HUD_HEIGHT = 100;
  public const float VIGNETTE_RADIUS = 200.0f;
  public const float VIGNETTE_NONE_FRAC = 0.6f;
  public const float VIGNETTE_FULL_FRAC = 1.4f;
  public const double WIN_SCREEN_WAIT = 10.0;
  public const double LOSS_SCREEN_WAIT = 3.0;
  public const double MAX_CHEAT_CODE_WAIT = 5.0;
  public const double SCREEN_FADE_DURATION = 0.2;

  /* derived constants */
  public const float LEVEL_WIDTH = LEVEL_WIDTH_BLOCKS * BASE_BLOCK_WIDTH;
  public const float LEVEL_HEIGHT = LEVEL_HEIGHT_BLOCKS * BASE_BLOCK_HEIGHT;
  public const int GAME_HEIGHT = (int) LEVEL_HEIGHT;
  public const int WINDOW_WIDTH = (int) LEVEL_WIDTH;
  public const int WINDOW_HEIGHT = HUD_HEIGHT + GAME_HEIGHT;
}
