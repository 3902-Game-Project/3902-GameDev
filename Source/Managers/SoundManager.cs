using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

internal class SoundManager {

  private Dictionary<string, SoundEffect> sounds = new();
  private Dictionary<string, LoopingSound> loops = new();

  private float masterVolume = 1.0f;
  public float MasterVolume { 
    get => masterVolume;
    set {
      masterVolume = Math.Clamp(value, 0.0f, 1.0f);
      UpdateVolumes();
    } 
  }

  private class LoopingSound {
    public SoundEffectInstance Instance;
    public float Volume;
  }

  private static readonly SoundManager instance = new();

  public static SoundManager Instance {
    get { return instance; }
  }

  private SoundManager() {
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

  public void PlayLoop(string name, float volume = 1.0f) {
    if (!sounds.TryGetValue(name, out var sound))
            return;

        if (loops.TryGetValue(name, out var loop))
        {
            if (loop.Instance.State == SoundState.Playing)
                return;

            loop.Volume = volume;
            loop.Instance.IsLooped = true;
            loop.Instance.Volume = volume * MasterVolume;
            loop.Instance.Play();
            return;
        }

        var instance = sound.CreateInstance();
        instance.IsLooped = true;
        instance.Volume = volume * MasterVolume;
        instance.Play();

        loops[name] = new LoopingSound
        {
            Instance = instance,
            Volume = volume
        };
  }

  public void Stop(string name) {
    if (loops.TryGetValue(name, out var loop))
    {
        loop.Instance.Stop();
        loop.Instance.Dispose();
        loops.Remove(name);
    }
  }

  public void StopAll() {
    foreach (var loop in loops.Values)
    {
        loop.Instance.Stop();
        loop.Instance.Dispose();
    }
    loops.Clear();
  }

  private void UpdateVolumes() {
    foreach (var loop in loops.Values) {
      loop.Instance.Volume = loop.Volume * MasterVolume;
    }
  }
}
