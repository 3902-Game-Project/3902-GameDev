using GameProject.Managers;

namespace GameProject.Commands;

internal class ToggleMusicCommand : ICommand {
  public void Execute() {
    SoundManager.Instance.MusicEnabled = !SoundManager.Instance.MusicEnabled;
  }
}
