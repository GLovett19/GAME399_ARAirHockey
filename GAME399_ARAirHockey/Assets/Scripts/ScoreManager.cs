using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public ReadyBoard readyboard;
    public Text CountDowntext;
    public GameObject PuckPrefab;
    


    PuckMovement ActivePuck;


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
        if (f_Counter > 0)
        {
            CountDowntext.enabled = true;
            switch((int)f_Counter)
            {
                case 3:
                    CountDowntext.text = "3";
                break;
                case 2:
                    CountDowntext.text = "2";
                    break;
                case 1:
                    CountDowntext.text = "1";
                    break;
                case 0:
                    CountDowntext.text = "GO";
                    SpawnPuck();
                    f_Counter = 0;
                    CountDowntext.enabled = false;
                    break;
                default:
                    break;


            }
            f_Counter -= Time.deltaTime;
        }
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
    public int GetScore(string player)
    {
        switch (player)
        {
            case "Player1":
                return int_Player1Score;
            case "Player2":
                return int_Player2Score;
            default:
                return 0;
        }
    }
    public void RoundEnd()
    {
        // destroy any puck still on the table
        Destroy(ActivePuck.gameObject);
        ActivePuck = null;

        // show the scoreboards 
        if (Player1Scoreboard.b_isVisible == false && Player2Scoreboard.b_isVisible == false)
        {
            Player1Scoreboard.ToggleScoreBoardVisible();
            Player2Scoreboard.ToggleScoreBoardVisible();
        }

        // ready prompt for the players to start a new round appears
        if (readyboard.b_isVisible == false)
        {
            readyboard.ToggleReadyVisible();
        }
    }
    public void RoundStart()
    {
        f_Counter = 4f;
        // scoreboards are hidden and ready prompt disappears
        if (Player1Scoreboard.b_isVisible == true && Player2Scoreboard.b_isVisible == true)
        { 
        Player1Scoreboard.ToggleScoreBoardVisible();
        Player2Scoreboard.ToggleScoreBoardVisible();
        }
        if (readyboard.b_isVisible == true)
        {
            readyboard.ToggleReadyVisible();
        }


        // new puck is spawned and the game begins 
        /*
         * How should the new puck be spawned? 
         * 
         * there should be a countdown 3,2,1,GO and then the puck is spawned at the center hit towards the player who was just scored on. 
         * 
         * currently spawns puck using code below, but there is no delay meaning the puck moves while the UI is still covering the screen
         * 
         * 
         */
        //SpawnPuck();
       
    }
    public void SpawnPuck()
    {
        ActivePuck = Instantiate(
            PuckPrefab,
            new Vector3(0, 1, 0),
            Quaternion.Euler(90, 0, 0)
            ).GetComponent<PuckMovement>();
        ActivePuck.f_Speed = 3f;
    }
}
