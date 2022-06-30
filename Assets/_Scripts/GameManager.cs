using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public Figures playerFigure;
    public Figures pcFigure;
    public GameState State;
    static public GameManager Instance;
    [SerializeField] float pcTimeoutTime = 1;

    public static event Action<GameState> OnGameStateChanged;

    public void newFigure(Figures figure)
    {
        Debug.Log(figure);
        playerFigure = figure;
        UpdateGameState(GameState.PlayerStep);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.PlayerStep:
                HandlePlayerStep();
                break;
            case GameState.PCStep:
                HandlePcStep();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    int checkWin(Figures player1, Figures player2)
    {
        bool player1Win = player1 == Figures.Paper && player2 == Figures.Stone ||
            player1 == Figures.Stone && player2 == Figures.Scissors ||
            player1 == Figures.Scissors && player2 == Figures.Paper;
        bool player2Win = player2 == Figures.Paper && player1 == Figures.Stone ||
        player2 == Figures.Stone && player1 == Figures.Scissors ||
        player2 == Figures.Scissors && player1 == Figures.Paper;
        if (player1Win) return 1;
        if (player2Win) return 2;
        return 0;
    }

    private void HandlePcStep()
    {
        int result = checkWin(playerFigure, pcFigure);

        string value = "";
        switch (result)
        {
            case 0:
                value = "No one win";
                break;
            case 1:
                value = "Player win!";
                break;
            case 2:
                value = "PC win!";
                break;
        }
        scoreText.text = value;
    }

    IEnumerator makePcStep()
    {
        yield return new WaitForSeconds(pcTimeoutTime);
        Figures[] figures = { Figures.Paper, Figures.Stone, Figures.Scissors };

        Figures fig = figures[UnityEngine.Random.Range(0, figures.Length)];
        pcFigure = fig;
        Debug.Log("PC: " + pcFigure);
        UpdateGameState(GameState.PCStep);
    }

    private void HandlePlayerStep()
    {
        UpdateGameState(GameState.PCTimeout);
        StartCoroutine(makePcStep());
    }

    void Awake()
    {
        Instance = this;
    }

}

public enum GameState
{
    PlayerStep,
    PCStep,
    PCTimeout
}