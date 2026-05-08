using GameProject.Misc;

namespace GameProject.Commands;

internal class ToggleMusicCommand : IGPCommand {
  internal void Execute() {
    SoundManager.Instance.MusicEnabled = !SoundManager.Instance.MusicEnabled;
  }
}
