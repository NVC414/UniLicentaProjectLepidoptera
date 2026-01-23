using System.Collections;
using UnityEngine;

public sealed class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip defaultTrack;

    [Range(0f, 1f)]
    [SerializeField] float volume = 1f;

    [SerializeField] float defaultFadeSeconds = 0.6f;

    Coroutine fadeRoutine;

    void Reset()
    {
        source = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (source == null) source = GetComponent<AudioSource>();

        ApplyVolume();

        if (defaultTrack != null && !source.isPlaying)
        {
            Play(defaultTrack, true);
        }
    }

    void ApplyVolume()
    {
        source.volume = Mathf.Clamp01(volume);
    }

    public void SetVolume(float v)
    {
        volume = Mathf.Clamp01(v);
        ApplyVolume();
    }

    public float GetVolume()
    {
        return volume;
    }

    public void Play(AudioClip clip, bool immediate = false)
    {
        if (clip == null) return;

        if (fadeRoutine != null) StopCoroutine(fadeRoutine);

        if (immediate || !source.isPlaying)
        {
            source.clip = clip;
            ApplyVolume();
            source.Play();
            return;
        }

        fadeRoutine = StartCoroutine(CrossfadeTo(clip, defaultFadeSeconds));
    }

    public void Stop(bool immediate = false)
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);

        if (immediate)
        {
            source.Stop();
            return;
        }

        fadeRoutine = StartCoroutine(FadeOut(defaultFadeSeconds));
    }

    IEnumerator CrossfadeTo(AudioClip next, float seconds)
    {
        float startVol = source.volume;

        for (float t = 0f; t < seconds; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(startVol, 0f, t / seconds);
            yield return null;
        }

        source.volume = 0f;
        source.clip = next;
        source.Play();

        for (float t = 0f; t < seconds; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(0f, volume, t / seconds);
            yield return null;
        }

        source.volume = volume;
        fadeRoutine = null;
    }

    IEnumerator FadeOut(float seconds)
    {
        float startVol = source.volume;

        for (float t = 0f; t < seconds; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(startVol, 0f, t / seconds);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
        fadeRoutine = null;
    }
}
