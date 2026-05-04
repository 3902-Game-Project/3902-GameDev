namespace GameProject.Globals;

internal static class Flags {
  /* compile-time tweakables */
  /* Note: must be static readonly not const because const causes dead code warnings for if statements. */
  public static readonly bool StartInDebugLevel = false;
  public static readonly bool StartInBossLevel = false;
  public static readonly bool DebugButtonBinds = true;
  public static readonly bool SpawnBfgImmediately = false;

  /* runtime tweakables */
  public static bool Vignette { get; set; } = true;
  public static bool HaltAllUpdates { get; set; } = false;
  public static bool HaltEnemies { get; set; } = false;
  public static bool SlowMode { get; set; } = false;
}
