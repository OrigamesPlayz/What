using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    public bool stopwatchActive = true;
    float currentTime;
    public TextMeshProUGUI currentTimeText;
    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        if (stopwatchActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopWatch()
    {
        stopwatchActive = true;
    }

    public void StopStopWatch()
    {
        stopwatchActive = false;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }
}
