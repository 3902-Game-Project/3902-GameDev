using Microsoft.Xna.Framework.Audio;
using GameProject.Factories;

namespace GameProject.Audio;

public class PlayerSounds {
  public SoundEffect hurtSFX { get; private set; }

  public PlayerSounds() {
    SoundFactory soundFactory = SoundFactory.Instance;
    hurtSFX = soundFactory.CreatePlayerHurtSFX();    
  }
}
