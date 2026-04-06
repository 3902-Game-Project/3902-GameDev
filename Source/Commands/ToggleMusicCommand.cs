using GameProject.Managers;
using GameProject.Interfaces;

namespace GameProject.Commands;

public class ToggleMusicCommand : ICommand {
  public void Execute() {
    SoundManager.Instance.MusicEnabled = !SoundManager.Instance.MusicEnabled;
  }
}
