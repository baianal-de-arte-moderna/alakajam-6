using UnityEngine;
using UnityEngine.SceneManagement;

public class BgGrid : MonoBehaviour
{
    public GameObject TilePrefab;
    public Vector2 prefabSize = Vector2.one * 32f;
    // Start is called before the first frame update
    void Awake()
    {
        var size = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        prefabSize /= 200f;
        for (var i = -size.x; i <= size.x; i += prefabSize.x)
        {
            for (var j = -size.y; j <= size.y; j += prefabSize.y)
            {
                Instantiate(TilePrefab, new Vector3(i, j), Quaternion.identity, transform);
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                        .GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
    }

    public void LoadSongChooser()
    {
        SceneManager.LoadScene("ChooseSongScene");
    }
}
