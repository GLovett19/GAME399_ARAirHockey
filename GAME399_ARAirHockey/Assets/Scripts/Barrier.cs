using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    //Components
    public GameObject BarrierModel;

    // private fields
    private float f_Counter;
    private Vector3 v3_StartPosition;
   // private Vector3 v3_CurrentPosition;

    //public fields
    public float f_ShakeDuration = 1;
    public float f_ShakeSpeed = 10;
    public float f_ShakeDistance = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (f_Counter > 0)
        {
            //calculate the position that the barrier model should shake to when hit
            float f_ShakePosition = Mathf.Sin(Time.time * f_ShakeSpeed) * (f_ShakeDistance * (f_Counter/f_ShakeDuration));

            // apply the transformation to the local position
            // this might appear backwards on some walls, because they all shake in the same direction first. 
            // to fix pass an integer or float value indicating which side the wall was hit on?
            BarrierModel.transform.localPosition = new Vector3(0, f_ShakePosition, 0);

            // decrement counter 
            f_Counter -= Time.deltaTime;
        }
        else
        {
            // just incase the model doesnt end up in the right spot when the shake is done set it back to zero
            BarrierModel.transform.localPosition = Vector3.zero;
        }

    }


    public void Hit(float f_ImpactSpeed)
    {
        
        f_Counter = f_ShakeDuration;
    }
}
