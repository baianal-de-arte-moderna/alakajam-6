using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        AudioClip audioClip = Resources.Load<AudioClip>($"Audio/{Director.SongName}");
        if (audioClip)
        {
            audioSource.clip = audioClip;
        }
        audioSource.Play();
    }
}
