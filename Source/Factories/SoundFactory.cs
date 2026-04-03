using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Factories;

internal class SoundFactory {
  private SoundEffect playerHurtSFX;
  private SoundEffect gunshotDefaultSFX;
  private SoundEffect reloadDefaultSFX;
  private static readonly SoundFactory instance = new();

  public static SoundFactory Instance {
    get { return instance; }
  }

  private SoundFactory() {
  }

  public void LoadAllContent(ContentManager content)
  {
    playerHurtSFX = content.Load<SoundEffect>("Sound Effects/player_hurt");
    gunshotDefaultSFX = content.Load<SoundEffect>("Sound Effects/gun_shot_default");
    reloadDefaultSFX = content.Load<SoundEffect>("Sound Effects/reload_default");
  }

  public SoundEffect CreatePlayerHurtSFX() {
    return playerHurtSFX;
  }

  public SoundEffect CreateGunshotDefaultSFX() {
    return gunshotDefaultSFX;
  }

  public SoundEffect CreateReloadDefaultSFX() {
    return reloadDefaultSFX;
  }
}
