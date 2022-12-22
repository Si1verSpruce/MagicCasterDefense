using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseToggle : MonoBehaviour
{
    private int _pauseRequests;

    public void RequestPause()
    {
        _pauseRequests++;
        Time.timeScale = 0;
    }
    
    public void RequestPlay()
    {
        _pauseRequests--;

        if (_pauseRequests == 0)
            Time.timeScale = 1;
    }
}
