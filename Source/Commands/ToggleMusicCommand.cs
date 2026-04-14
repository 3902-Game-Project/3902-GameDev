using GameProject.Managers;

namespace GameProject.Commands;

internal class ToggleMusicCommand : IGPCommand {
  public void Execute() {
    SoundManager.Instance.MusicEnabled = !SoundManager.Instance.MusicEnabled;
  }
}
