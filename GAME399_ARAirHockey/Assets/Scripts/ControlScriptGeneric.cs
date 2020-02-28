using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class ControlScriptGeneric : MonoBehaviour
{
    //Components
    public ImageTargetBehaviour itb_ImageTarget;


    //Fields
    public float f_movementScaleValueX = -1;
    public float f_movementScaleValueY = 1;


    public virtual void Update()
    {
        Movement();
        
    }
    public virtual void Movement()
    {
        if (itb_ImageTarget != null)
        {

            //f_movementScaleValueX = -50 / (Mathf.Clamp(Mathf.Abs(itb_ImageTarget.transform.position.z + 20), 0, 1) + 1);
            //f_movementScaleValueY = 50 / (Mathf.Clamp(Mathf.Abs(itb_ImageTarget.transform.position.z + 20), 0, 1) + 1);


            Vector3 v3 = new Vector3(itb_ImageTarget.transform.position.x * f_movementScaleValueX, (-1 + itb_ImageTarget.transform.position.y) * f_movementScaleValueY, 0f);
            transform.position = v3;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                0f
                );
        }
    }




}
