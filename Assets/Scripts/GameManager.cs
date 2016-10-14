using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    static public bool gameStarted = false;

    private int level = 3;

    // Use this for initialization
    void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
    }

	
    public void InitGame()
    {
        GameObject.Find("MenuImage").SetActive(false);
        gameStarted = true;

        boardScript.SetupScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
