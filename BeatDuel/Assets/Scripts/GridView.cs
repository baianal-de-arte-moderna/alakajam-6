/* vim: set ts=4 sts=4 sw=4 expandtab: */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
#region editor_variables
    public int subdivisions;
    public float gridMarginOffset;
    public float gridLineWidth;
    public float gridHeight;
    public int maxPlaysPerSubdivision;
    public List<GameObject> playerTiles;
#endregion editor_variables

    private Dictionary<int, List<GameObject>> currentPlays;
    private float SCREENWIDTH;
    private float SCREENWIDTH_2;

    public void RegisterPlay(int subdivision, int playerIndex)
    {
        currentPlays[subdivision].Add(Instantiate(playerTiles[playerIndex]));
        ArrangePlays();
    }

    private void ArrangePlaysInSubdivision(int subdivision, List<GameObject> plays)
    {
        //float subdivisionLineOffset = subdivision * gridLineWidth;
        float subdivisionSlotOffset = subdivision * (SCREENWIDTH / (subdivisions));
        //float playPosition = gridMarginOffset + subdivisionLineOffset + subdivisionSlotOffset - SCREENWIDTH_2;
        float playPosition = subdivisionSlotOffset - SCREENWIDTH_2;
        foreach (GameObject play in plays)
        {
            play.transform.position = (float)playPosition * Vector3.right;
        }
    }

    private void ArrangePlays()
    {
        foreach (var subdivision in currentPlays)
        {
            ArrangePlaysInSubdivision(subdivision.Key, subdivision.Value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SCREENWIDTH_2 = Camera.main.aspect * Camera.main.orthographicSize;
        SCREENWIDTH = 2 * SCREENWIDTH_2;
        currentPlays = new Dictionary<int, List<GameObject>>();
        for (int i = 0; i < subdivisions; i++)
        {
            currentPlays[i] = new List<GameObject>();
        }
        TestRegisterPlay(); 
    }

    private void TestRegisterPlay()
    {
        int currentPlayer = 0;
        for (int i = 0; i < subdivisions; i++)
        {
            RegisterPlay(i, currentPlayer);
            currentPlayer = (currentPlayer + 1) % 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
