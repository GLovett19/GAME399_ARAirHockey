using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTesting : MonoBehaviour
{
    //Self Populating Components
    ScoreManager sm_manager;

    public AudioClip explosion;
    AudioSource audioSource;

    //public fields
    public string playerGoal; // should only be Player1 or Player2

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sm_manager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PuckMovement>())
        {
            audioSource.PlayOneShot(explosion, 0.7f);
            sm_manager.SetScore(1, playerGoal);
        }
    }
}