using UnityEngine;
using System;
using System.Collections;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.playOnAwake) Play(s.name);
        }
    }

    public void Play(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.source.Pause();
    }

    public void Resume(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.source.UnPause();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public AudioSource GetAudioSource(string name)
    {
        Sound s = GetSound(name);
        return s?.source;
    }

    public float GetSourcePitch(string name)
    {
        Sound s = GetSound(name);
        return s?.source.pitch ?? 0f;
    }

    public void SetSourcePitch(string name, float pitch)
    {
        Sound s = GetSound(name);
        if (s != null) s.source.pitch = pitch;
    }

    public void SetSourceVolume(string name, float volume)
    {
        Sound s = GetSound(name);
        if (s != null) s.source.volume = volume;
    }

    public void PlayFromRandomPosition(string name)
    {
        Sound s = GetSound(name);
        if (s == null) return;
        s.source.time = UnityEngine.Random.Range(0f, s.source.clip.length);
        s.source.Play();
    }

    public bool IsPlaying(string name)
    {
        Sound s = GetSound(name);
        return s?.source.isPlaying ?? false;
    }

    public void FadeIn(string name, float duration)
    {
        StartCoroutine(FadeAudio(name, duration, true));
    }

    public void FadeOut(string name, float duration)
    {
        StartCoroutine(FadeAudio(name, duration, false));
    }

    private IEnumerator FadeAudio(string name, float duration, bool fadeIn)
    {
        Sound s = GetSound(name);
        if (s == null) yield break;

        float startVolume = fadeIn ? 0f : s.source.volume;
        float targetVolume = fadeIn ? s.volume : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            s.source.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            yield return null;
        }

        s.source.volume = targetVolume;
        if (!fadeIn) s.source.Stop();
    }

    private Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Audio clip '{name}' not found!");
        }
        return s;
    }
}
