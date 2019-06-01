using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCreditsScript : MonoBehaviour
{
    public void LoadCredits()
    {
        SceneManager.LoadScene("MenuCreditos");
    }
}
