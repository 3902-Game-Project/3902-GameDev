using Microsoft.Xna.Framework;

namespace GameProject.Globals;

internal static class Constants {
  /* base constants */
  internal static readonly Color MAIN_BACKGROUND_COLOR = new(25, 28, 33); // Dark gray background
  internal static readonly Color LEVEL_BACKGROUND_COLOR = new(20, 20, 120); // Bluish background
  internal static readonly Color LETTERBOX_COLOR = Color.Black; // Color used for border with resized window

  internal const float BASE_BLOCK_WIDTH = 64.0f;
  internal const float BASE_BLOCK_HEIGHT = 64.0f;
  internal const int LEVEL_WIDTH_BLOCKS = 15;
  internal const int LEVEL_HEIGHT_BLOCKS = 9;

  internal const int HUD_HEIGHT = 100;

  internal const float VIGNETTE_RADIUS = 200.0f;
  internal const float VIGNETTE_NONE_FRAC = 0.6f;
  internal const float VIGNETTE_FULL_FRAC = 1.4f;

  internal const double WIN_SCREEN_WAIT = 10.0;
  internal const double LOSS_SCREEN_WAIT = 3.0;

  internal const double MAX_CHEAT_CODE_WAIT = 5.0;

  internal const double SCREEN_FADE_DURATION = 0.2;

  internal const float BASE_ENEMY_WIDTH = 64.0f;
  internal const float BASE_ENEMY_HEIGHT = 64.0f;
  internal const float ENEMY_DAMAGE_FLASH_DURATION = 0.15f;

  internal const float PLAYER_INVINCIBILITY_DURATION = 1.5f;
  internal const float PLAYER_DAMAGE_FLASH_DURATION = 0.3f;
  internal const float PLAYER_SPEED = 200.0f;

  internal const float PLAYER_SPRITE_SCALE = 0.15f;
  internal const float PLAYER_SPRITE_WIDTH = 171.0f;
  internal const float PLAYER_SPRITE_HEIGHT = 323.0f;
  internal const float HEALTH_BAR_SCALE = 0.15f;
  internal const int DEFAULT_MAX_HEALTH = 100;
  internal const int ENEMY_CONTACT_DAMAGE = 50;
  internal const float ITEM_GRAB_RANGE = 75.0f;
  internal const float AMMO_AUTO_COLLECT_RANGE = 30.0f;
  internal const float COLLISION_BUFFER = 1.0f;
  internal const int USE_ITEM_DURATION_FRAMES = 20;

  internal const int BACKPACK_COLUMNS = 5;

  internal const int BOSS_DAMAGE = 15;
  internal const int RIFLEMAN_DAMAGE = 80;
  internal const int SHOTGUNNER_DAMAGE = 50;

  internal const float BOSS_ALIGNMENT_THRESHOLD = 25.0f;
  internal const float BOSS_WANDER_SPEED = 120.0f;

  internal const PlayerIndex GAMEPAD_PLAYER_INDEX = PlayerIndex.One;

  internal const string SAVE_FILE_PATH = "savegame.json";

  /* derived constants */
  internal const float LEVEL_WIDTH = LEVEL_WIDTH_BLOCKS * BASE_BLOCK_WIDTH;
  internal const float LEVEL_HEIGHT = LEVEL_HEIGHT_BLOCKS * BASE_BLOCK_HEIGHT;
  internal const int GAME_HEIGHT = (int) LEVEL_HEIGHT;
  internal const int WINDOW_WIDTH = (int) LEVEL_WIDTH;
  internal const int WINDOW_HEIGHT = HUD_HEIGHT + GAME_HEIGHT;
}
