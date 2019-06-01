using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        audioSource.clip = Resources.Load<AudioClip>($"Audio/{Director.SongName}");
        audioSource.Play();
    }
}
