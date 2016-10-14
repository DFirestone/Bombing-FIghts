using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlobalStateManager : MonoBehaviour
{

    private int deadPlayers = 0;
    private int deadPlayerNumber = -1;

    public enum GameEndState  { GAME_END_1PLAYER, GAME_END_2PLAYER, GAME_END_DRAW, GAME_NOT_ENDED };

    public static GameEndState endState = GameEndState.GAME_NOT_ENDED;

    public void PlayerDied(int playerNumber)
    {
        deadPlayers++;

        if (deadPlayers == 1)
        {
            deadPlayerNumber = playerNumber;
            Invoke("CheckPlayersDeath", .3f);
        }
    }

    void CheckPlayersDeath()
    {
        //Text title = GameObject.Find("TitleText").GetComponent<Text>();
        GameManager.gameStarted = false;

        if (deadPlayers == 1)
        { //Single dead player, he's the winner

            if (deadPlayerNumber == 1)
            { //P1 dead, P2 is the winner
                //title.text = "Player 2 is the winner!";
                endState = GameEndState.GAME_END_2PLAYER;
            }
            else { //P2 dead, P1 is the winner
                //title.text = "Player 1 is the winner!";
                endState = GameEndState.GAME_END_1PLAYER;
            }
        }
        else {  //Multiple dead players, it's a draw
            //title.text = "The game ended in a draw!";
            endState = GameEndState.GAME_END_DRAW;
        }


        

        //GameObject.Find("MenuImage").SetActive(true);
    }
}
