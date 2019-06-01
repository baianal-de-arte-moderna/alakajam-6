using UnityEngine;
using UnityEngine.Events;

public class DrumElement : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnDrumElementHitEvent;

    public void DrumElementHit()
    {
        OnDrumElementHitEvent?.Invoke();
    }
}
