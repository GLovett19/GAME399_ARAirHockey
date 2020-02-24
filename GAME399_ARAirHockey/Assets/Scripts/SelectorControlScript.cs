using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SelectorControlScript : ControlScriptGeneric
{

    public MeshRenderer myMesh;

    void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
    }

    public override void Update()
    {
        Movement();
        PauseWhenTracked();
    }


    public void PauseWhenTracked()
    {
        if (itb_ImageTarget.CurrentStatus == ImageTargetBehaviour.Status.TRACKED)
        {
            // if the target is detected do a specific thing now
            Time.timeScale = 0;
            myMesh.enabled = true;
            Debug.Log("PAUSE");
        }
        else
        {
            myMesh.enabled = false;
            Time.timeScale = 1;
        }
    }
}
