using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsText : MonoBehaviour
{
    float frameCount = 0f;
    float dt = 0f;
    float fps = 0f;
    float updateRate = 4f;
    [SerializeField] public TextMeshProUGUI fpsText;
    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1f / updateRate;
            fpsText.text = "FPS: " + fps.ToString("F0");
        }
    }
}
