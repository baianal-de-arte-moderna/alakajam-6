using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ChangeSubdivisionEvent : UnityEvent<int>
{ }

public class MusicLoader : MonoBehaviour
{
    private const int NUMBER_OF_SUBDIVISIONS = 16;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private UnityEvent OnChangeCycle;

    [SerializeField]
    private ChangeSubdivisionEvent OnChangeSubdivision;

    private float cycleDuration;
    private int currentCycle;
    private int currentSubdivision;

    void Start()
    {
        AudioClip audioClip = Resources.Load<AudioClip>($"Audio/{Director.SongName}");
        if (audioClip)
        {
            audioSource.clip = audioClip;
        }
        cycleDuration = audioSource.clip.length / 2;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        int computedCycle = Mathf.FloorToInt(audioSource.time / cycleDuration);
        if (currentCycle != computedCycle)
        {
            currentCycle = computedCycle;
            OnChangeCycle?.Invoke();
        }

        int computedSubdivision = (int)Mathf.Floor((audioSource.time % cycleDuration) / cycleDuration * NUMBER_OF_SUBDIVISIONS);
        if (currentSubdivision != computedSubdivision)
        {
            currentSubdivision = computedSubdivision;
            OnChangeSubdivision?.Invoke(currentSubdivision);
        }
    }
}
