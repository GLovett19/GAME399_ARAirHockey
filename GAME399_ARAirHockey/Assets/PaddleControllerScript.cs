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
        // get the name of the image target you want this object to move according to. each image target name has to be unique for this to work 
       // itb_stones = GameObject.Find(s_ImageTargetName).GetComponent<ImageTargetBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itb_stones != null)
        {            
            Vector3 v3 = new Vector3(itb_stones.transform.position.x * f_movementScaleValueX, (-1 + itb_stones.transform.position.y) * f_movementScaleValueY, 0f);
            transform.position = v3;
        }
    }


   

}
