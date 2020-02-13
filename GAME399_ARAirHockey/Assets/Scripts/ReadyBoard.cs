using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyBoard : MonoBehaviour
{
    //Components
    ScoreManager sm_Manager;
    Button TempButton;
    Text t_text;

    //Fields 
    float f_Count;

    public float f_Speed = 0.5f;
    public float f_Position = 310;
    public bool b_isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        sm_Manager = FindObjectOfType<ScoreManager>();
        TempButton = GetComponentInChildren<Button>();
        t_text = GetComponentInChildren<Text>();
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
                    TempButton.transform.localPosition = new Vector3(
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
                    TempButton.transform.localPosition = new Vector3(
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
                    TempButton.transform.localPosition = new Vector3(
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
                    TempButton.transform.localPosition = new Vector3(
                       0,
                       f_Position,
                       0
                       );
                }

                break;

        }
    }

    public void ToggleReadyVisible()
    {
        f_Count = (Mathf.PI * 0.5f / f_Speed);
    }
}
