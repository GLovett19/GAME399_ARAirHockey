using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    //Components
    ScoreManager sm_Manager;
    Text t_Score;
    Image i_Board;
    
    //Fields 
    float f_Count;

    public float f_Speed = 0.5f;
    public float f_Position = 510;
    public bool isVisible = false;
    public int isRight = 1;

    // Start is called before the first frame update
    void Start()
    {
        sm_Manager = FindObjectOfType<ScoreManager>();
        t_Score = GetComponentInChildren<Text>();
        i_Board = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (isVisible)
        {
            case true:
                if (f_Count > 0)
                {
                    // move the board out of view
                    float boardPositionX = Mathf.Sin(f_Speed * f_Count) * f_Position;
                    i_Board.transform.localPosition = new Vector3(
                        boardPositionX * isRight,
                        0,
                        0
                        );
                    // decrement the counter
                    f_Count -= Time.deltaTime;
                }
                else if (f_Count != 0)
                {
                    isVisible = false;
                    f_Count = 0;
                    //set to a static position for consistency
                    i_Board.transform.localPosition = new Vector3(
                       0,
                       0,
                       0
                       );
                }
                break;
            case false:
                if (f_Count > 0)
                {
                    float boardPositionX = Mathf.Sin(f_Speed * ((Mathf.PI * 0.5f / f_Speed) - f_Count)) * f_Position;
                    Debug.Log((Mathf.PI * 0.5f / f_Speed));
                    i_Board.transform.localPosition = new Vector3(
                        boardPositionX * isRight,
                        0,
                        0
                        );
                    f_Count -= Time.deltaTime;
                }
                else if(f_Count != 0)
                {
                    isVisible = true;
                    f_Count = 0;
                    i_Board.transform.localPosition = new Vector3(
                       f_Position * isRight,
                       0,
                       0
                       );
                }
                break;
        }
       
    }

    public void ToggleScoreBoardVisible()
    {
        f_Count = 3.14f;
    }
}
