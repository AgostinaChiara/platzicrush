using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;

    private PlayerController controller;

    public int collectedObject = 0;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)
        {
            StartGame();
        } else if(Input.GetKeyDown(KeyCode.R))
        {
            GameOver();
        } else if(Input.GetKeyDown(KeyCode.M))
        {
            BackToMenu();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //TODO: colocar logica del menu
            MenuManager.sharedInstance.ShowMainMenu();
        } else if(newGameState == GameState.inGame)
        {
            //TODO: hay que preparar la escena para jugar
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();
        } else if(newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.ShowMainMenu();
            //TODO: preparar el juego para el GameOver
        }

        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
