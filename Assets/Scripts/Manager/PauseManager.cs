using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void SetTimeScale(int i)
    {
        Time.timeScale = i;
    }
}
