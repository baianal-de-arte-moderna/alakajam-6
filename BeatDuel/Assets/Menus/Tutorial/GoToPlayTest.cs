using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPlayTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene("PlayTest");
        }
    }
}
