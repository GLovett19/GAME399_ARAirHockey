using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyBoard : MonoBehaviour
{
    //Self Assigning Components
    ScoreManager sm_Manager;

    //Assigned Components 
    public GameObject ButtonMoveMount;
    public Button NextRoundButton;
    public Button NextMatchButton;
    public Button NextGameButton;
    public Button MainMenuButton;

    //Public fields 
    public float f_Speed = 0.5f; // this speed MUST be SLOWER than the score board speed otherwise the scoreboards will get stuck in the wrong position
    public float f_Position = 310;
    public bool b_isVisible = false;

    //Private Fields 
    float f_Count;

    bool b_RoundEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        sm_Manager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (b_isVisible)
        {
            case true:
                if (f_Count > 0)
                {
                    float boardPositionY = Mathf.Sin(f_Speed * f_Count) * f_Position;
                    //Debug.Log((Mathf.PI * 0.5f / f_Speed));
                    ButtonMoveMount.transform.localPosition = new Vector3(
                        0,
                        boardPositionY,
                        0
                        );
                    f_Count -= Time.deltaTime;
                }

                else if (f_Count != 0)
                {
                    b_isVisible = false;
                    f_Count = 0;
                    ButtonMoveMount.transform.localPosition = new Vector3(
                       0,
                       0,
                       0
                       );
                }

                break;
            case false:
                if (f_Count > 0)
                {
                    float boardPositionY = Mathf.Sin(f_Speed * ((Mathf.PI * 0.5f / f_Speed) - f_Count)) * f_Position;
                    //Debug.Log((Mathf.PI * 0.5f / f_Speed));
                    ButtonMoveMount.transform.localPosition = new Vector3(
                        0,
                        boardPositionY,
                        0
                        );
                    f_Count -= Time.deltaTime;
                }

                else if (f_Count != 0)
                {
                    b_isVisible = true;
                    f_Count = 0;
                    ButtonMoveMount.transform.localPosition = new Vector3(
                       0,
                       f_Position,
                       0
                       );
                    if (b_RoundEnding)
                    {                        
                        sm_Manager.RoundStart();
                    }
                }

                break;

        }
    }

    public void ToggleReadyBoardVisible()
    {
        f_Count = (Mathf.PI * 0.5f / f_Speed);
    }

    public void RoundEndButtons()
    {
        //NextRoundButton.gameObject.SetActive(true);
        b_RoundEnding = true;

        NextMatchButton.gameObject.SetActive(false);
        NextGameButton.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);
    }

    public void MatchEndButtons()
    {
        NextMatchButton.gameObject.SetActive(true);


        // NextRoundButton.gameObject.SetActive(false);
        b_RoundEnding = false;
        NextGameButton.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);
    }

    public void GameEndButtons()
    {
        Debug.Log("A");
        NextGameButton.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);

        NextMatchButton.gameObject.SetActive(false);
        //NextRoundButton.gameObject.SetActive(false);
        b_RoundEnding = false;
    }
}
