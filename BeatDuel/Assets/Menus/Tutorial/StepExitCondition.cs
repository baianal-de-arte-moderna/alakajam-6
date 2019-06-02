using UnityEngine;
using TMPro;

public class StepExitCondition : MonoBehaviour
{
    public enum ExitConditions
    {
        Click,
        BeatAdded,
        TurnChange
    }
    public ExitConditions ExitOn;
    public GameObject SafeGuard;
    public TMP_Text contentText;

    public void Transition()
    {
        SafeGuard.SetActive(ExitOn == ExitConditions.Click);
    }

    public void Failed()
    {
        if (contentText != null)
            contentText.text = "Hit ALL the beats!\n\nHit the correct pads in the correct order and timing";
    }
}
