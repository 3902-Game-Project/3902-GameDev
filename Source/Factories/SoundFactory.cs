using GameProject.Interfaces;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class SoundFactory {
  private byte[] hurtSFX;
  private byte[] gunSFX;
  private byte[] reloadSFX;
  private static readonly SoundFactory instance = new();

  public static SoundFactory Instance {
    get { return instance; }
  }

  private SoundFactory() {
  }

  public void LoadAllSounds(ContentManager content) {
  }
}
