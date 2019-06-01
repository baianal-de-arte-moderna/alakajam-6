using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrumElement : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private UnityEvent OnDrumElementHitEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrumElementHitEvent?.Invoke();
    }
}
