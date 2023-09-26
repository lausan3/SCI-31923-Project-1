using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class GameState : MonoBehaviour
{
    public GameObject gamePlaying;

    [Header("Text")] 
    public Canvas startScreenCanvas;
    public TextMeshProUGUI startGameText;
    public Canvas endScreenCanvas;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI levelText;
    public Canvas playingGameCanvas;
    
    #region references

    private PlayerController player;
    private GameManager gm;
    private static GameManager originalGM;

    #endregion

    private enum gameState
    {
        JustStarted,
        PlayingGame,
        GameOver
    }

    private gameState _gameState;
    
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gamePlaying = GameObject.Find("Playing Game");
        
        originalGM = gm;
        gamePlaying.SetActive(false);

        _gameState = gameState.JustStarted;
        
        StartScreen();
    }

    private void Update()
    {
        // just got into game, in start screen
        if (_gameState == gameState.JustStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Play(1f));
        }
        
        // playing game, player health dropped to 0
        if (_gameState == gameState.PlayingGame && player.health <= 0)
        {
            EndScreen();
            _gameState = gameState.GameOver;
        }
        
        // in end screen, restart game
        if (_gameState == gameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Play(1f));
        }
    }

    private void StartScreen()
    {
        Debug.Log("Started");
        StopGame();
        
        startScreenCanvas.gameObject.SetActive(true);
        startGameText.gameObject.SetActive(true);
        playingGameCanvas.gameObject.SetActive(false);
    }

    private IEnumerator Play(float waitTime)
    {
        Debug.Log("Played");
        StartGame();
        
        player.ResetGame();
        gm.ResetGame();
        
        playingGameCanvas.gameObject.SetActive(true);
        startScreenCanvas.gameObject.SetActive(false);
        endScreenCanvas.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        _gameState = gameState.PlayingGame;
    }

    private void EndScreen()
    {
        Debug.Log("Ended");
        StopGame();
        
        playingGameCanvas.gameObject.SetActive(false);
        endScreenCanvas.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        enemiesKilledText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        startGameText.gameObject.SetActive(true);

        timeText.text = "You survived " + gm.timer + " seconds!";
        enemiesKilledText.text = "Enemies Killed: " + gm.enemiesKilled;
        levelText.text = "You got level " + gm.level + "!";
    }
    
    private void StartGame()
    {
        gamePlaying.SetActive(true);
    }

    private void StopGame()
    {
        gamePlaying.SetActive(false);
        
        // kill all enemies and pickups
        GameObject[] activeObjectsNonPlayer = GameObject.FindGameObjectsWithTag("NPC");

        foreach (var aObject in activeObjectsNonPlayer)
        {
            Destroy(aObject);
        }
    }

}