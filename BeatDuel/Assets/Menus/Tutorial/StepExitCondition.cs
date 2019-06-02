using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Transition()
    {
        SafeGuard.SetActive(ExitOn == ExitConditions.Click);
    }
}
