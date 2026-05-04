namespace GameProject.Globals;

internal static class Flags {
  /* compile-time tweakables */
  public const bool StartInDebugLevel = false;
  public const bool StartInBossLevel = false;
  public const bool DebugButtonBinds = true;
  public const bool SpawnBfgImmediately = false;

  /* runtime tweakables */
  public static bool Vignette { get; set; } = true;
  public static bool HaltAllUpdates { get; set; } = false;
  public static bool HaltEnemies { get; set; } = false;
  public static bool SlowMode { get; set; } = false;
}
