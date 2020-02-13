using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /*What this script needs to do. 
     * 
     * Keep track of the current score of each player
     * allow other scripts access to those values via SetScore(int val)
     * 
     * Reset the scene after a scored goal placing new puck on the table. via TableReset()
     * 
     * Keep track of player Wins and Losses. 
     * after 2 wins send to end game scene? 
     * 
     * 
     * */



    //Components 
    public ScoreBoard Player1Scoreboard;
    public ScoreBoard Player2Scoreboard;

    //public Fields
    public int i_MatchPoint = 7;

    //Private Fields
    int int_Player1Score;
    int int_Player2Score;
    int int_Player1Wins;
    int int_Player2Wins;
    float f_Counter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int val, string player)
    {
        switch(player)
        {
            case "Player1":
                int_Player1Score += val;
                Debug.Log("Player 1 Score : " + int_Player1Score);
                RoundEnd();
                break;
            case "Player2":
                int_Player2Score += val;
                Debug.Log("Player 2 Score : " + int_Player2Score);
                RoundEnd();
                break;
            default:
                break;
        }
    }
    public void RoundEnd()
    {
        // destroy any puck still on the table for some reason
        
        
        // show the scoreboards 
        Player1Scoreboard.ToggleScoreBoardVisible();
        Player2Scoreboard.ToggleScoreBoardVisible();
            
        // ready prompt for the players to start a new round appears
    }
    public void RoundStart()
    {
        // scoreboards are hidden and ready prompt disappears
        Player1Scoreboard.ToggleScoreBoardVisible();
        Player2Scoreboard.ToggleScoreBoardVisible();

        // new puck is spawned and the game begins 
    }
}
