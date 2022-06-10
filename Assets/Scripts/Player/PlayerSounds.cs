using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip hit;
    public AudioClip jump;

    private AudioSource source;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Movement.pHit += Hit;
        Movement.pJump += Jump;
    }

    private void OnDisable()
    {
        Movement.pHit -= Hit;
        Movement.pJump -= Jump;
    }

    private void Jump()
    {
        source.PlayOneShot(jump);    
    }

    private void Hit()
    {
        source.PlayOneShot(hit);
    }
}
