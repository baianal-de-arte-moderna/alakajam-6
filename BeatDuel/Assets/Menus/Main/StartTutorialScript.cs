using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorialScript : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
