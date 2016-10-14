using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameManager gameManager;
    
	void Awake () {

        if (GameManager.instance == null)
            Instantiate(gameManager);
	}

    public void StartGame()
    {

        gameManager.InitGame();
    }

    public void ExitGame()
    {
        gameManager.ExitGame();
    }
	
	void Update () {
	
	}
}
