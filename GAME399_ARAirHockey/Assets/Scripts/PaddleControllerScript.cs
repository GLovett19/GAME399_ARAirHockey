using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PaddleControllerScript : ControlScriptGeneric
{

    // Update is called once per frame
    public override void Update()
    {
        Movement();
    }

    public void LimitMovement()
    {
        // limit the position to positive or negative here
    }

   

}
