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
    //public MovingPanel readyboard;
    public MenuGeneric myMenu;
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
        myMenu = GetComponent<MenuGeneric>();
        //readyboard.MatchEndButtons();
        myMenu.ShowPanel("MatchEndPanel");
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
                case 4:
                    CountDowntext.text = "3";
                break;
                case 3:
                    CountDowntext.text = "2";
                    break;
                case 2:
                    CountDowntext.text = "1";
                    break;
                case 1:
                    CountDowntext.text = "GO";                   
                    break;
                case 0:
                    RoundStart();
                    f_Counter = 0;
                    SpawnPuck();
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


        // Attempting to mesh the score manager with the Genericised Menu script to allow better pause game management

        // Round Ends 

        // Show Scoreboards handlded here by score manager


        

        // Testing Ends 

        // this makes sure the ready board is displaying the correct buttons, find a better place to do this later.
        //readyboard.RoundEndButtons();

        // destroy any puck still on the table
        Destroy(ActivePuck.gameObject);
        ActivePuck = null;

        // show the scoreboards 
        if (Player1Scoreboard.b_isVisible == false && Player2Scoreboard.b_isVisible == false)
        {
            Player1Scoreboard.ToggleScoreBoardVisible();
            Player2Scoreboard.ToggleScoreBoardVisible();
        }

        if (int_Player1Score >= int_MatchPoint || int_Player2Score >= int_MatchPoint)
        {
            // this makes sure the ready board is displaying the correct buttons, find a better place to do this later.
            //readyboard.MatchEndButtons();


            MatchEnd();

        }
        else
        {
            // no check between rounds, show match end panel for testing purposes 
            //myMenu.ShowPanel("MatchEndPanel");
            
            // Set the Start timer 
            f_Counter = 5f;
        }
        


    }
    public void RoundStart()
    {

        f_Counter = 5f;
        // scoreboards are hidden and ready prompt disappears
        if (Player1Scoreboard.b_isVisible == true && Player2Scoreboard.b_isVisible == true)
        { 
        Player1Scoreboard.ToggleScoreBoardVisible();
        Player2Scoreboard.ToggleScoreBoardVisible();
        }
       
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

            GameEnd();
            
        }
        else
        {
            // show the match end panel
            myMenu.ShowPanel("MatchEndPanel");
        }

    }
    public void MatchStart()
    {
        myMenu.HidePanel("MatchEndPanel");

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
        myMenu.ShowPanel("GameEndPanel");
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
