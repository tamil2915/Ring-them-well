using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Board board;
    public GameObject instructions;

    public GameObject gameOverFailure;
    public GameObject gameOverSuccess;

    public bool isGameLocked = false;

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ExecuteGame()
    {
        board.PlayGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructions.SetActive(true);
    }

    public void GameOverSuccessWindow()
    {
        gameOverSuccess.SetActive(true);
    }

    public void GameOverFailureWindow() {
        gameOverFailure.SetActive(true);
    }

    public bool isGameBlocked()
    {
        return isGameLocked;
    }
}
