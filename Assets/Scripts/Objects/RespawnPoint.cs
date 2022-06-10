using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RespawnPoint : MonoBehaviour
{
    public delegate void SetSpawn();
    public static SetSpawn setSpawn;

    public bool playerSpawnPoint;
    
    private Animator anim;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    

    private void Update()
    {
        anim.SetBool("Active", playerSpawnPoint);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!playerSpawnPoint)
        {
            source.Play();
        }
        setSpawn?.Invoke();
        playerSpawnPoint = true;
        
        
        Movement.pDead += RespawnPlayer;
    }

    private void OnEnable()
    {
        setSpawn += Reset;
    }

    private void OnDisable()
    {
        setSpawn -= Reset;
        Reset();
    }

    private void Reset()
    {
        playerSpawnPoint = false;
        Movement.pDead -= RespawnPlayer;
    }

    private void RespawnPlayer(Transform player)
    {
        player.position = transform.position;
    }
}
