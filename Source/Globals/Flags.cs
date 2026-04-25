namespace GameProject.Globals;

internal static class Flags {
  /* compile-time tweakables */
  public static bool StartInDebugLevel { get; } = false;
  public static bool EnableVignette { get; } = true;

  /* runtime tweakables */
  public static bool HaltAllUpdates { get; set; } = false;
  public static bool SlowMode { get; set; } = false;
}
