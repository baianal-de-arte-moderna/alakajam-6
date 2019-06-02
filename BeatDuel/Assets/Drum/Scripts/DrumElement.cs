using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrumElement : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Color padColorPressed;
    [SerializeField] private UnityEvent OnDrumElementHitEvent;

    private Color padColorIdle;
    public void Start()
    {
        padColorIdle = GetComponent<Image>().color;
    }

    public void OnAnimationStart() {
        Debug.Log("Animation started");
        Debug.Log(GetComponent<Image>());
        GetComponent<Image>().color = padColorPressed;
    }

    public void OnAnimationEnd() {
        Debug.Log("Animation ended");
        GetComponent<Image>().color = padColorIdle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrumElementHitEvent?.Invoke();
    }
}
