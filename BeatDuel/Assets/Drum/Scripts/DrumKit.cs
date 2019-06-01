using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DrumKitHitEvent : UnityEvent<int>
{
}

public class DrumKit : MonoBehaviour
{
    [SerializeField]
    private DrumKitHitEvent OnDrumKitHitEvent;

    public void DrumElementHit(int index)
    {
        OnDrumKitHitEvent?.Invoke(index);
    }
}
