using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // Self Assigning Components
    ScoreManager sm_Manager;
    Image i_Board;

    // Assigned Components 
    public Text t_Score;
    public Text t_Wins;
    
    //Fields 
    float f_Count;

    //public fields
    public float f_Speed = 1f;
    public float f_Position = 510; // this is static and will not scale well with screen resolution, figure this out if you have time
    public bool b_isVisible = false;

    public string str_Player = "Player1";
    public int b_isRight = 1;

    // Start is called before the first frame update
    void Start()
    {
        sm_Manager = FindObjectOfType<ScoreManager>();
        i_Board = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (b_isVisible)
        {
            case true:
                if (f_Count > 0)
                {
                    // move the board out of view
                    float boardPositionX = Mathf.Sin(f_Speed * f_Count) * f_Position;
                    i_Board.transform.localPosition = new Vector3(
                        boardPositionX * b_isRight,
                        0,
                        0
                        );
                    // decrement the counter
                    f_Count -= Time.deltaTime;
                }
                else if (f_Count != 0)
                {
                    b_isVisible = false;
                    f_Count = 0;
                    //set to a static position for consistency
                    i_Board.transform.localPosition = new Vector3(
                       0,
                       0,
                       0
                       );
                    t_Score.text = sm_Manager.GetScore(str_Player).ToString();
                    t_Wins.text = sm_Manager.GetWins(str_Player).ToString();
                }
                break;
            case false:
                if (f_Count > 0)
                {
                    float boardPositionX = Mathf.Sin(f_Speed * ((Mathf.PI * 0.5f / f_Speed) - f_Count)) * f_Position;
                    //Debug.Log((Mathf.PI * 0.5f / f_Speed));
                    i_Board.transform.localPosition = new Vector3(
                        boardPositionX * b_isRight,
                        0,
                        0
                        );
                    f_Count -= Time.deltaTime;
                }
                else if(f_Count != 0)
                {
                    b_isVisible = true;
                    f_Count = 0;
                    i_Board.transform.localPosition = new Vector3(
                       f_Position * b_isRight,
                       0,
                       0
                       );
                    // update Displayed Score, possibly do some particle effects or something to jazz it up
                    t_Score.text = sm_Manager.GetScore(str_Player).ToString();
                    t_Wins.text = sm_Manager.GetWins(str_Player).ToString();
                }
                break;
        }
    }

    public void ToggleScoreBoardVisible()
    {
        f_Count = (Mathf.PI * 0.5f / f_Speed);
    }
    
}
