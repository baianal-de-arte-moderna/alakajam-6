using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour, IPointerDownHandler
{
    public static int loserPlayer;

    private void Start()
    {
        Debug.Log($"Loser player: {loserPlayer}");
        float rotation = 0;
        if (loserPlayer % 2 == 1)
        {
            rotation = 180;
        }
        gameObject.transform.Rotate(Vector3.forward, rotation);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
