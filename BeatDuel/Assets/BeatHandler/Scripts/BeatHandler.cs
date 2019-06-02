using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerEvent : UnityEvent<int, int>
{ }

[Serializable]
public class PlayerChangedEvent : UnityEvent<int>
{ }

public class BeatHandler : MonoBehaviour
{
    private const int NUMBER_OF_PLAYERS = 2;

    #region editor_variables
    [SerializeField]
    private PlayerEvent OnPlayerEvent;

    [SerializeField]
    private PlayerChangedEvent OnPlayerChanged;

    [SerializeField]
    private AudioSource audioLoop;

    [SerializeField]
    private bool isTutorial = false;
    #endregion

    private Dictionary<int, List<int>> beats = new Dictionary<int, List<int>>();
    private List<int> previousSubdivisionMissingBeats = new List<int>();
    private List<int> currentSubdivisionMissingBeats = new List<int>();

    private int currentPlayer;
    private int currentSubdivision;
    private bool currentPlayerMove;

    private void Start()
    {
        SetCurrentPlayer(0);
        SetCurrentSubdivision(0);
    }

    public void OnChangeCycle()
    {
        if (!currentPlayerMove)
        {
            SetCurrentPlayer((currentPlayer + 1) % NUMBER_OF_PLAYERS);
        }
    }

    public void OnChangeSubdivision(int newCurrentSubdivision)
    {
        Invoke("CheckCurrentPlayerBeats", 0.1f);
        SetCurrentSubdivision(newCurrentSubdivision);
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
        OnPlayerChanged?.Invoke(newCurrentPlayer);
        Debug.Log($"Player {currentPlayer}'s turn");
    }

    private void SetCurrentSubdivision(int newCurrentSubdivision)
    {
        currentSubdivision = newCurrentSubdivision;
        previousSubdivisionMissingBeats.AddRange(currentSubdivisionMissingBeats);
        currentSubdivisionMissingBeats.Clear();
        currentSubdivisionMissingBeats.AddRange(GetSubdivisionBeats(currentSubdivision));
        Debug.Log($"Current subdivision: {currentSubdivision}");
    }

    private void RegisterCurrentPlayerBeat(int beat)
    {
        Debug.Log($"Register beat {beat} for player {currentPlayer}");

        if (previousSubdivisionMissingBeats.Contains(beat))
        {
            previousSubdivisionMissingBeats.Remove(beat);
        }
        else if (currentSubdivisionMissingBeats.Contains(beat))
        {
            currentSubdivisionMissingBeats.Remove(beat);
        }
        else if (currentPlayerMove)
        {
            currentPlayerMove = false;
            beats[currentSubdivision] = GetSubdivisionBeats(currentSubdivision);
            beats[currentSubdivision].Add(beat);
            OnPlayerEvent?.Invoke(currentSubdivision, currentPlayer);
        }
        else
        {
            OnGameOver(currentPlayer, "You played a wrong beat!");
        }
    }

    private void CheckCurrentPlayerBeats()
    {
        if (previousSubdivisionMissingBeats.Count > 0)
        {
            OnGameOver(currentPlayer, "You didn't hit all the beats!");
        }
    }

    private void OnGameOver(int loserPlayer, string gameOverReason)
    {
        if (!isTutorial)
        {
            GameOver.loserPlayer = loserPlayer;
            GameOver.gameOverReason = gameOverReason;
            SceneManager.LoadScene("GameOver");
        }
    }

    private List<int> GetSubdivisionBeats(int subdivision)
    {
        if (!beats.TryGetValue(subdivision, out List<int> subdivisionBeats))
        {
            subdivisionBeats = new List<int>();
        }
        return subdivisionBeats;
    }
}
