using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuide : MonoBehaviour
{
    public Canvas canvas;
    public GameObject[] steps;
    int currentStep;
    // Start is called before the first frame update
    void Start()
    {
        currentStep = 0;
        steps[currentStep].SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            NextStep();
        }
    }

    void NextStep()
    {
        steps[currentStep].SetActive(false);
        currentStep++;

        if (currentStep >= steps.Length)
        {
            canvas.gameObject.SetActive(false);
            enabled = false;
        }
        else
        {
            steps[currentStep].SetActive(true);
        }
    }
}
