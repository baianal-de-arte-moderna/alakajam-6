using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour, IPointerDownHandler
{
    public static int loserPlayer = 1;
    public static string gameOverReason;

    [SerializeField]
    private TMP_Text gameOverText;

    [SerializeField]
    private TMP_Text gameOverReasonText;

    private void Start()
    {
        Debug.Log($"Loser player: {loserPlayer}");
        Debug.Log($"Game over reason: {gameOverReason}");

        float rotation = 0;
        if (loserPlayer % 2 == 1)
        {
            rotation = 180;
        }
        gameOverText.rectTransform.Rotate(Vector3.forward, rotation);

        if (gameOverReason != null)
        {
            gameOverReasonText.text = gameOverReason.ToLower();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
