using GameProject.Interfaces;
using GameProject.Managers;

namespace GameProject.Commands;

public class ToggleMusicCommand : ICommand {
  public void Execute() {
    SoundManager.Instance.MusicEnabled = !SoundManager.Instance.MusicEnabled;
  }
}
