namespace GameProject.Globals;

internal static class Flags {
  public static bool StartInDebugLevel { get; } = false;
  public static bool EnableVignette { get; } = true;

  public static bool HaltAllUpdates { get; set; } = false;
}
