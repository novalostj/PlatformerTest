using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour
{
    public delegate void GameStartAndEnd();
    public static GameStartAndEnd set;
    
    public Sprite closeSprite;
    public Sprite openSprite;
    public GameObject gate;
    public SpriteRenderer spriteRenderer;

    public bool open;

    public float timer;

    private AudioSource source;
    private Animator Anim => gate.GetComponent<Animator>();
    private BoxCollider2D BoxCol => gate.GetComponent<BoxCollider2D>();

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (open) Open();
        else Close();
    }

    private void Update()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") && timer > 0) return;
        
        timer = 0.5f;
        open = !open;
        if (open) Open();
        else Close();
        set?.Invoke();
        
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Open()
    {
        open = true;
        spriteRenderer.sprite = openSprite;
        Anim.SetBool("Open", true);
        BoxCol.enabled = false;
        timer = 0.5f;
        source.Play();
    }

    private void Close()
    {
        open = false;
        spriteRenderer.sprite = closeSprite;
        Anim.SetBool("Open", false);
        BoxCol.enabled = true;
        timer = 0.5f;
        source.Play();
    }
    
    
}
