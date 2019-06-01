using UnityEngine;
using UnityEngine.Events;

public class DrumKit : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<int> OnDrumKitHitEvent;

    public void DrumElementHit(int index)
    {
        OnDrumKitHitEvent?.Invoke(index);
    }
}
