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
    public int int_MatchPoint = 7;
    public int int_GamePoint = 2;

    //Private Fields
    public int int_Player1Score;
    public int int_Player2Score;
    public int int_Player1Wins;
    public int int_Player2Wins;
    float f_Counter;
    public string str_toUnload;

    // Start is called before the first frame update
    void Start()
    {
        readyboard.MatchEndButtons();
        str_toUnload = ActiveSceneManager.GetSceneName();
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
                //Debug.Log("Player 1 Score : " + int_Player1Score);
                RoundEnd();
                break;
            case "Player2":
                int_Player2Score += val;
                //Debug.Log("Player 2 Score : " + int_Player2Score);
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

    public void SetWins(int val, string player)
    {
        switch (player)
        {
            case "Player1":
                int_Player1Wins += val;
                //Debug.Log("Player 1 Wins : " + int_Player1Wins);
                break;
            case "Player2":
                int_Player2Wins += val;
                //Debug.Log("Player 2 Wins : " + int_Player2Wins);
                break;
            default:
                break;
        }
    }
    public int GetWins(string player)
    {
        switch (player)
        {
            case "Player1":
                return int_Player1Wins;
            case "Player2":
                return int_Player2Wins;
            default:
                return 0;
        }
    }
    
    public void RoundEnd()
    {
        // this makes sure the ready board is displaying the correct buttons, find a better place to do this later.
        readyboard.RoundEndButtons();
        
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
            readyboard.ToggleReadyBoardVisible();           
        }

        if (int_Player1Score >= int_MatchPoint || int_Player2Score >= int_MatchPoint)
        {
            // this makes sure the ready board is displaying the correct buttons, find a better place to do this later.
            readyboard.MatchEndButtons();
            MatchEnd();
            
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
            readyboard.ToggleReadyBoardVisible();
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

    public void MatchEnd()
    {
        // assign wins to proper player 
        if (int_Player1Score > int_Player2Score)
        {
            SetWins(1, "Player1");
        }
        else
        {
            SetWins(1, "Player2");
        }
        // check to see if the game is over
        if (int_Player1Wins >= int_GamePoint || int_Player2Wins >= int_GamePoint)
        {
            Debug.Log("C");
            GameEnd();
            
        }

    }
    public void MatchStart()
    {
        int_Player1Score = 0;
        int_Player2Score = 0;
        RoundStart();
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

    public void GameEnd()
    {
        // what needs to happen at the end of a game
        /*
         * Swap ready board buttons to play next game OR return to main menu
         *      play next game can just reload the sceen
         *      Main menu can just load the main menu
         * Some Kind of Congradulations for the winner? 
         *      A Crown pops out over their scoreboard? 
         *      Something Else, Talk about this in class friday
         */
        Debug.Log("B");
        readyboard.GameEndButtons();
    }

    public void LoadNextGame()
    {
        ActiveSceneManager.ReloadScene(str_toUnload, true);
    }
    public void LoadMainMenu()
    {
        
        ActiveSceneManager.LoadScene("TopMenu", false);
        ActiveSceneManager.UnloadScene(str_toUnload);
    }
}
