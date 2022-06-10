using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject canvas;
    public bool hasDifferentPCMSG = false;
    public string msgIfDesktop;
    public TextMeshProUGUI text;
    
    private void Start()
    {
        canvas.SetActive(false);

        if (hasDifferentPCMSG && SystemInfo.deviceType == DeviceType.Desktop)
        {
            text.text = msgIfDesktop;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
}
