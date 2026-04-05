using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

public class SoundManager {

  private Dictionary<string, SoundEffect> sounds = new();
  private Dictionary<string, SoundEffectInstance> instances = new();

  private float masterVolume = 1.0f;
  public float MasterVolume { 
    get { return masterVolume; }
    set {
      masterVolume = Math.Clamp(value, 0.0f, 1.0f);
      UpdateVolumes();
    } 
  }

  public void Load(ContentManager content) {
    sounds["playerHurt"] = content.Load<SoundEffect>("Sound Effects/player_hurt");
    sounds["gunshotDefault"] = content.Load<SoundEffect>("Sound Effects/gun_shot_default");
    sounds["reloadDefault"] = content.Load<SoundEffect>("Sound Effects/reload_default");
  }

  public void Play(string name, float volume = 1.0f, float pitch = 0f, float pan = 0f) {
    if (sounds.TryGetValue(name, out SoundEffect sound)) {
      sound.Play(volume * MasterVolume, pitch, pan);
    }
  }

  public void PlayLoop(string name) {
    if (!sounds.TryGetValue(name, out SoundEffect sound)) 
      return;
    
    if (instances.TryGetValue(name, out SoundEffectInstance instance)) {
      if (instance.State == SoundState.Playing) 
        return;
      
      instance.IsLooped = true;
      instance.Volume = MasterVolume;
      instance.Play();
      return;
    }

    instance = sound.CreateInstance();
    instance.IsLooped = true;
    instance.Volume = MasterVolume;
    instance.Play();

    instances[name] = instance;
  }

  public void Stop(string name) {
    if (instances.TryGetValue(name, out var instance))
    {
        instance.Stop();
        instance.Dispose();
        instances.Remove(name);
    }
  }

  public void StopAll() {
    foreach (var instance in instances.Values)
    {
        instance.Stop();
        instance.Dispose();
    }
    instances.Clear();
  }

  private void UpdateVolumes() {
    foreach (SoundEffectInstance instance in instances.Values) {
      instance.Volume = MasterVolume;
    }
  }
}
