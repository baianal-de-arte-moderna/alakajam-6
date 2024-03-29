﻿/* vim: set ts=4 sts=4 sw=4 expandtab: */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
#region editor_variables
    [SerializeField] private int subdivisions;
    [SerializeField] private float gridMarginOffset;
    [SerializeField] private float gridLineWidth;
    [SerializeField] private float gridHeight;
    [SerializeField] private int maxPlaysPerSubdivision;
    [SerializeField] private List<GameObject> playerTiles;
    [SerializeField] private GameObject gridLine;
#endregion editor_variables

    private Dictionary<int, List<GameObject>> currentPlays;
    private float SCREENWIDTH;
    private float SCREENWIDTH_2;
    private float GRIDHEIGHT_2;

    public void RegisterPlay(int subdivision, int playerIndex)
    {
        currentPlays[subdivision].Add(Instantiate(playerTiles[playerIndex]));
        ArrangePlays();
    }

    private void ArrangePlaysInSubdivision(int subdivision, List<GameObject> plays)
    {
        //float subdivisionLineOffset = subdivision * gridLineWidth;
        //float subdivisionSlotOffset = subdivision * (SCREENWIDTH / (subdivisions));
        //float playPosition = gridMarginOffset + subdivisionLineOffset + subdivisionSlotOffset - SCREENWIDTH_2;
        //float playPosition = subdivisionSlotOffset - SCREENWIDTH_2;
        // Distribute all play buckets across the grid.
        float subdivisionPosition = Mathf.Lerp(-SCREENWIDTH_2, SCREENWIDTH_2, subdivision / (float)subdivisions);
        for (int currentPlay = 0; currentPlay < plays.Count; currentPlay++)
        {
            GameObject play = plays[currentPlay];
            float playPosition = Mathf.Lerp(-GRIDHEIGHT_2, GRIDHEIGHT_2, currentPlay / (float)plays.Count);
            play.transform.position = subdivisionPosition * Vector3.right + playPosition * Vector3.up;
            play.transform.localScale = new Vector3(1.0f, 1.0f / plays.Count, 1.0f);
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
        GRIDHEIGHT_2 = gridHeight / 2;
        SCREENWIDTH_2 = Camera.main.aspect * Camera.main.orthographicSize;
        SCREENWIDTH = 2 * SCREENWIDTH_2;
        currentPlays = new Dictionary<int, List<GameObject>>();
        for (int i = 0; i < subdivisions; i++)
        {
            currentPlays[i] = new List<GameObject>();
        }
        DrawGrid();
        //TestRegisterPlay(); 
    }

    private void DrawGridLine(float halfLength, Vector3 direction, Vector3 position)
    {
        GameObject line = Instantiate(gridLine);
        line.transform.parent = transform;
        line.transform.position = position;

        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.startWidth = gridLineWidth;
        lineRenderer.endWidth = gridLineWidth;
        lineRenderer.SetPositions(new Vector3[] { -halfLength * direction, halfLength * direction });
    }

    private void DrawGrid()
    {
        for (int i = 0; i <= maxPlaysPerSubdivision; i++)
        {
            Vector3 position = Mathf.Lerp(-GRIDHEIGHT_2, GRIDHEIGHT_2, i / (float)(maxPlaysPerSubdivision)) * Vector3.up;
            DrawGridLine(SCREENWIDTH_2, Vector3.right, position);
        }
        for (int i = 0; i <= subdivisions; i++)
        {
            Vector3 position = Mathf.Lerp(-SCREENWIDTH_2, SCREENWIDTH_2, i / (float)(subdivisions)) * Vector3.right;
            DrawGridLine(GRIDHEIGHT_2, Vector3.up, position);
        }
    }

    private void TestRegisterPlay()
    {
        int currentPlayer = 0;
        for (int i = 0; i < 3 * subdivisions; i++)
        {
            RegisterPlay(i / 3, currentPlayer);
            currentPlayer = (currentPlayer + 1) % 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
