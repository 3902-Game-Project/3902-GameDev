namespace GameProject.Globals;

internal static class Flags {
  /* compile-time tweakables */
  public static readonly bool StartInDebugLevel = false;
  public static readonly bool EnableVignette = true;

  /* runtime tweakables */
  public static bool HaltAllUpdates { get; set; } = false;
  public static bool HaltEnemies { get; set; } = false;
  public static bool SlowMode { get; set; } = false;
}
