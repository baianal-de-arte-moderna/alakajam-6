using System;
using System.Collections.Generic;
using System.Linq;
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
    #endregion

    private Dictionary<int, List<int>> beats = new Dictionary<int, List<int>>();

    private int currentPlayer;
    private int currentSubdivision;
    private List<int> currentPlayerBeats = new List<int>();
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
        if (!CheckCurrentPlayerBeats())
        {
            OnGameOver(currentPlayer, "You didn't hit all the beats!");
        }
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
                OnGameOver(currentPlayer, "You played twice in this turn!");
            }
        }
        else if (!currentPlayerBeats.Contains(beat))
        {
            currentPlayerBeats.Add(beat);
        }
        else
        {
            OnGameOver(currentPlayer, "You already played that note!");
        }

        beats[currentSubdivision] = currentSubdivisionBeats;
    }

    private bool CheckCurrentPlayerBeats()
    {
        if (!beats.TryGetValue(currentSubdivision, out List<int> currentSubdivisionBeats))
        {
            currentSubdivisionBeats = new List<int>();
        }

        return currentSubdivisionBeats.All(beat => currentPlayerBeats.Contains(beat));
    }

    private void OnGameOver(int loserPlayer, string gameOverReason)
    {
        GameOver.loserPlayer = loserPlayer;
        GameOver.gameOverReason = gameOverReason;
        SceneManager.LoadScene("GameOver");
    }
}
