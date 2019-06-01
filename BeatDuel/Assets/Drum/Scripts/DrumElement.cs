using UnityEngine;
using UnityEngine.Events;

public class DrumElement : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private UnityEvent OnDrumElementHitEvent;

    public void DrumElementHit()
    {
        audioSource?.Play();
        OnDrumElementHitEvent?.Invoke();
    }
}
