using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGuide : MonoBehaviour
{
    public Canvas canvas;
    public StepExitCondition[] steps;
    int currentStep;
    // Start is called before the first frame update
    void Start()
    {
        currentStep = 0;
        steps.ToList().ForEach(x => x.gameObject.SetActive(false));
        steps[currentStep].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            if (steps[currentStep].ExitOn == StepExitCondition.ExitConditions.Click)
                NextStep();
        }
    }

    void NextStep()
    {
        steps[currentStep].gameObject.SetActive(false);
        currentStep++;

        if (currentStep >= steps.Length)
        {
            canvas.gameObject.SetActive(false);
            SceneManager.LoadScene("MainMenu");
            enabled = false;
        }
        else
        {
            steps[currentStep].gameObject.SetActive(true);
            steps[currentStep].Transition();
        }
    }

    public void BeatAdded()
    {
        if (steps[currentStep].ExitOn == StepExitCondition.ExitConditions.BeatAdded)
            NextStep();
    }

    public void TurnChanged()
    {
        if (steps[currentStep].ExitOn == StepExitCondition.ExitConditions.TurnChange)
            NextStep();
    }
}
