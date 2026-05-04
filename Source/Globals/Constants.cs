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

  /* derived constants */
  public const float LEVEL_WIDTH = LEVEL_WIDTH_BLOCKS * BASE_BLOCK_WIDTH;
  public const float LEVEL_HEIGHT = LEVEL_HEIGHT_BLOCKS * BASE_BLOCK_HEIGHT;
  public const int GAME_HEIGHT = (int) LEVEL_HEIGHT;
  public const int WINDOW_WIDTH = (int) LEVEL_WIDTH;
  public const int WINDOW_HEIGHT = HUD_HEIGHT + GAME_HEIGHT;
}
