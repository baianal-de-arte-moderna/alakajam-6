using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerEvent : UnityEvent<int, int>
{
}

public class BeatHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerEvent OnPlayerEvent;

    private Dictionary<int, List<int>> beats = new Dictionary<int, List<int>>();

    private int numberOfPlayers = 2;
    private float duration = Director.SongDuration / 2;
    private int numberOfSubdivisions = 16;

    private int currentPlayer;
    private List<int> currentPlayerBeats = new List<int>();
    private bool currentPlayerMove;
    private float currentTime;
    private int currentSubdivision;

    private void Start()
    {
        SetCurrentPlayer(0);
        SetCurrentSubdivision(0);
    }

    private void FixedUpdate()
    {
        currentTime = currentTime + Time.deltaTime;
        if (currentTime > duration)
        {
            if (!currentPlayerMove)
            {
                SetCurrentPlayer((currentPlayer + 1) % numberOfPlayers);

            }
            currentTime %= duration;
        }

        int computedSubdivision = (int) Mathf.Floor(currentTime / duration * numberOfSubdivisions);
        if (currentSubdivision != computedSubdivision)
        {
            if (!CheckCurrentPlayerBeats())
            {
                OnGameOver(currentPlayer);
            }
            SetCurrentSubdivision(computedSubdivision);
        }
    }

    public void RegisterPlayer0Beat(int beat)
    {
        if (currentPlayer == 0)
        {
            RegisterCurrentPlayerBeat(beat);
        }
    }

    public void RegisterPlayer1Beat(int beat)
    {
        if (currentPlayer == 1)
        {
            RegisterCurrentPlayerBeat(beat);
        }
    }

    private void SetCurrentPlayer(int newCurrentPlayer)
    {
        currentPlayer = newCurrentPlayer;
        currentPlayerMove = true;
        Debug.Log($"Player {currentPlayer}'s turn");
    }

    private void SetCurrentSubdivision(int newCurrentSubdivision)
    {
        currentSubdivision = newCurrentSubdivision;
        currentPlayerBeats.Clear();
        Debug.Log($"Current subdivision: {currentSubdivision}");
    }

    private void RegisterCurrentPlayerBeat(int beat)
    {
        Debug.Log($"Register beat {beat} for player {currentPlayer}");

        if (!beats.TryGetValue(currentSubdivision, out List<int> currentSubdivisionBeats))
        {
            currentSubdivisionBeats = new List<int>();
        }

        if (!currentSubdivisionBeats.Contains(beat))
        {
            if (currentPlayerMove)
            {
                currentPlayerMove = false;
                currentSubdivisionBeats.Add(beat);
                currentPlayerBeats.Add(beat);
                OnPlayerEvent?.Invoke(currentSubdivision, currentPlayer);
            }
            else
            {
                OnGameOver(currentPlayer);
            }
        }
        else if (!currentPlayerBeats.Contains(beat))
        {
            currentPlayerBeats.Add(beat);
        }
        else
        {
            OnGameOver(currentPlayer);
        }

        beats[currentSubdivision] = currentSubdivisionBeats;
        Debug.Log($"Beats: {GetBeatsString(beats)}");
    }

    private bool CheckCurrentPlayerBeats()
    {
        if (!beats.TryGetValue(currentSubdivision, out List<int> currentSubdivisionBeats))
        {
            currentSubdivisionBeats = new List<int>();
        }

        return currentSubdivisionBeats.All(beat => currentPlayerBeats.Contains(beat));
    }

    private void OnGameOver(int loserPlayer)
    {
        GameOver.loserPlayer = loserPlayer;
        SceneManager.LoadScene("GameOver");
    }

    private string GetBeatsString(Dictionary<int, List<int>> beatDictionary)
    {
        string beatsString = "";
        for (int subdivision = 0; subdivision < numberOfSubdivisions; ++subdivision)
        {
            if (!beats.TryGetValue(subdivision, out List<int> subdivisionBeats))
            {
                subdivisionBeats = new List<int>();
            }

            beatsString += $"{subdivision}: [{GetBeatsString(subdivisionBeats)}] ";
        }
        return beatsString;
    }

    private string GetBeatsString(List<int> beatList)
    {
        string beatsString = "";
        for (int beat = 0; beat < 5; ++beat)
        {
            if (beatList.Contains(beat))
            {
                beatsString += $"{beat}";
            }
            else
            {
                beatsString += $" ";
            }
        }
        return beatsString;
    }
}
