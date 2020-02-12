﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckMovement : MonoBehaviour
{
    //Self Assigned Components
    public Rigidbody rb_Rigidbody;


    // fields 
    public float f_Speed;
    public Vector3 v3_TargetDirection;
    

    void Start()
    {
        //Self Assigned Components 
        rb_Rigidbody = GetComponent<Rigidbody>();

        // pick an initial movement direction for testing purposes, this will be replaced later. 
        v3_TargetDirection = new Vector3(
            (float)Random.Range(-10, 10), 
            (float)Random.Range(-10, 10), 
            0
            );
        v3_TargetDirection.Normalize();
    }

    void Update()
    {
        // keep a constant velocity in the desired direction. possibly add drag to this later?
        rb_Rigidbody.velocity = v3_TargetDirection * f_Speed;


        // Clamp the Puck position to the visible area this is for testing only, every area should be fully enclosed by barriers
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        //Draw a ray for the desired direction
        Debug.DrawRay(transform.position, v3_TargetDirection, Color.yellow);
    }

    void OnCollisionEnter(Collision other)
    {
        // possibly use a switch statement instead of and If statement in future iterations, for more flexibility and collision types. 

        // check if the ball is colliding against a barrier
        if (other.collider.GetComponent<Barrier>() != null)
        {
            // do collision barrier things only, 
            // tell the barrier to bounce, shake or react here using pubilc methods in the barrier class.
            other.collider.GetComponent<Barrier>().Hit(0);
            //Reflect the target direction by the normal of the collision, Does this still work with sphere & Cylinder colliders? 
             v3_TargetDirection = Vector3.Reflect(v3_TargetDirection, other.contacts[0].normal);

        }

    }
}