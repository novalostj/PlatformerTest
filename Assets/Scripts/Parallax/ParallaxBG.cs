using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public float multiplier = 0.3f;
    
    private Transform cameraTranform;
    private Vector3 lastCameraPos;

    private void Start()
    {
        cameraTranform = Camera.main.transform;
        lastCameraPos = cameraTranform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTranform.position - lastCameraPos;
        transform.position -= deltaMovement * multiplier;
        lastCameraPos = cameraTranform.position;
    }
}
