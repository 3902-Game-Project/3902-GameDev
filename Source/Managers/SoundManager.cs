using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Managers;

internal enum SoundID {
  PlayerHurt,
  GunshotDefault,
  ReloadDefault,
  Background,
  Door,
}

internal class SoundManager {
  public bool MusicEnabled {
    get => musicEnabled;
    set {
      musicEnabled = value;
      UpdateVolumes();
    }
  }

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

  public static SoundManager Instance { get; } = new();
  
  private readonly Dictionary<SoundID, SoundEffect> sounds = [];
  private readonly Dictionary<SoundID, LoopingSound> loops = [];

  private bool musicEnabled = true;

  private SoundManager() { }

  public void LoadAllContent(ContentManager content) {
    sounds[SoundID.PlayerHurt] = content.Load<SoundEffect>("Sound Effects/Player Hurt");
    sounds[SoundID.GunshotDefault] = content.Load<SoundEffect>("Sound Effects/Gun Shot");
    sounds[SoundID.ReloadDefault] = content.Load<SoundEffect>("Sound Effects/Reload");
    sounds[SoundID.Door] = content.Load<SoundEffect>("Sound Effects/Door Open");

    sounds[SoundID.Background] = content.Load<SoundEffect>("Music/Western Rock-Metal Song");
  }

  public void Play(SoundID id, float volume = 1.0f, float pitch = 0f, float pan = 0f) {
    SoundEffect sound = sounds[id];
    sound.Play(volume * MasterVolume, pitch, pan);
  }

  public void PlayLoop(SoundID id, float volume = 1.0f) {
    if (!sounds.TryGetValue(id, out var sound))
      return;

    if (loops.TryGetValue(id, out var loop)) {
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

    loops[id] = new LoopingSound {
      Instance = instance,
      Volume = volume
    };
  }

  public void Stop(SoundID id) {
    if (loops.TryGetValue(id, out var loop)) {
      loop.Instance.Stop();
      loop.Instance.Dispose();
      loops.Remove(id);
    }
  }

  public void StopAll() {
    foreach (var loop in loops.Values) {
      loop.Instance.Stop();
      loop.Instance.Dispose();
    }
    loops.Clear();
  }

  private void UpdateVolumes() {
    float newVolume = MasterVolume;
    if (!musicEnabled)
      newVolume = 0;
    foreach (var loop in loops.Values) {
      loop.Instance.Volume = loop.Volume * newVolume;
    }
  }
}
