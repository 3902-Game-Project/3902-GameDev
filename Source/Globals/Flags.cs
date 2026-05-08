namespace GameProject.Globals;

internal static class Flags {
  /* compile-time tweakables */
  /* Note: must be static readonly not const because const causes dead code warnings for if statements. */
  internal static readonly bool StartInDebugLevel = false;
  internal static readonly bool StartInBossLevel = false;
  internal static readonly bool DebugButtonBinds = true;
  internal static readonly bool SpawnBfgImmediately = false;

  /* runtime tweakables */
  internal static bool Vignette { get; set; } = true;
  internal static bool HaltAllUpdates { get; set; } = false;
  internal static bool HaltEnemies { get; set; } = false;
  internal static bool SlowMode { get; set; } = false;
}
