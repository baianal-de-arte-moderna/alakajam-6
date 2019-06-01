using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSongButtonScript : MonoBehaviour
{
    public string songName;
    public float songDuration;

    public void SongChosen()
    {
        Director.SongName = songName;
        Director.SongDuration = songDuration;
        SceneManager.LoadScene("GameManagerScene");
    }
}
