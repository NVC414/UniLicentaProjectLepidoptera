using UnityEngine;

public sealed class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioClip track;

    void Start()
    {
        if (MusicPlayer.Instance != null)
            MusicPlayer.Instance.Play(track);
    }
}