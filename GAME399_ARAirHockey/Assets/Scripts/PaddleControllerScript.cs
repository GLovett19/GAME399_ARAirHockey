using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PaddleControllerScript : MonoBehaviour
{
    //Components
    public ImageTargetBehaviour itb_stones;


    //Fields
    //public string s_ImageTargetName;
    public float f_movementScaleValueX = -5;
    public float f_movementScaleValueY = 5;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (itb_stones != null)
        {            
            Vector3 v3 = new Vector3(itb_stones.transform.position.x * f_movementScaleValueX, (-1 + itb_stones.transform.position.y) * f_movementScaleValueY, 0f);
            transform.position = v3;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                0f
                );
        }
    }


   

}
