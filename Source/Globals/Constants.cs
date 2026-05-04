namespace GameProject.Globals;

internal static class Constants {
  /* base constants */
  public const int BASE_BLOCK_WIDTH = 64;
  public const int BASE_BLOCK_HEIGHT = 64;
  public const int LEVEL_WIDTH_BLOCKS = 15;
  public const int LEVEL_HEIGHT_BLOCKS = 9;

  /* derived constants */
  public const int LEVEL_WIDTH = LEVEL_WIDTH_BLOCKS * BASE_BLOCK_WIDTH;
  public const int LEVEL_HEIGHT = LEVEL_HEIGHT_BLOCKS * BASE_BLOCK_HEIGHT;
}
