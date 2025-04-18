using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField]
    private float impactSoundThreshold = 2f;
    [SerializeField]
    private AudioClip[] collisionEnterSounds;
    [SerializeField]
    private AudioClip[] collisionStaySounds;
    [SerializeField]
    private AudioClip[] collisionExitSounds;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionEnterSounds.Length > 0)
        {
            if (collision.relativeVelocity.magnitude > impactSoundThreshold)
            {
                audioSource.PlayOneShot(collisionEnterSounds[Random.Range(0, collisionEnterSounds.Length)]);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collisionStaySounds.Length > 0)
        {
            if (collision.relativeVelocity.magnitude > impactSoundThreshold)
            {
                audioSource.PlayOneShot(collisionStaySounds[Random.Range(0, collisionStaySounds.Length)]);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisionExitSounds.Length > 0)
        {
            if (collision.relativeVelocity.magnitude > impactSoundThreshold)
            {
                audioSource.PlayOneShot(collisionExitSounds[Random.Range(0, collisionExitSounds.Length)]);
            }
        }
    }
}
