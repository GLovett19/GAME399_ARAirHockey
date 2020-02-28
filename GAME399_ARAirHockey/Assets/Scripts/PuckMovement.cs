using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckMovement : MonoBehaviour
{
    //Self Assigned Components
    public Rigidbody rb_Rigidbody;

    //Particles
    public ParticleSystem goalParticles;

    //Audio
    public List<AudioClip> explosion;
    AudioSource audioSource;

    // fields 
    public float f_Speed;
    public float f_minSpeed;
    public float f_maxSpeed;
    public Vector3 v3_TargetDirection;

    //private fields 
    public Vector3 v3_SavedVelocity;

    void Start()
    {
        //Audio
        audioSource = GetComponent<AudioSource>();

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

        switch (other.gameObject.tag)
        {


            case "Barrier":
                other.collider.GetComponent<Barrier>().Hit(0);

                //Audio
                audioSource.PlayOneShot(explosion [0], 0.7f);

                //Reflect the target direction by the normal of the collision, Does this still work with sphere & Cylinder colliders? 
                v3_TargetDirection = Vector3.Reflect(v3_TargetDirection, other.contacts[0].normal);
                if (f_Speed > f_minSpeed)
                {
                    f_Speed *= .9f;
                }
                break;
            case "Paddle":

                //Audio
                audioSource.PlayOneShot(explosion [1], 0.7f);

                // set the new direction and increase the speed 
                v3_TargetDirection = other.contacts[0].normal;
                if (f_Speed < f_maxSpeed)
                {
                    f_Speed *= 1.2f;
                }
                break;
            default:
                break;
        }
        // possibly use a switch statement instead of and If statement in future iterations, for more flexibility and collision types. 

    }    
    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Goal":
                audioSource.PlayOneShot(explosion [2], 0.7f);
                Instantiate(goalParticles, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }


    public void PausePuckMovement()
    {
        // save the current velocity 
        v3_SavedVelocity = f_Speed * v3_TargetDirection;

        // stop the puck 
        f_Speed = 0;
        v3_TargetDirection = Vector3.zero;
    }
    public void ResumePuckMovement()
    {
        f_Speed = v3_SavedVelocity.magnitude;
        v3_TargetDirection = v3_SavedVelocity.normalized;

        v3_SavedVelocity = Vector3.zero;
    }
    public Vector3 GetSavedVelocity()
    {
        return v3_SavedVelocity;
    }
}
