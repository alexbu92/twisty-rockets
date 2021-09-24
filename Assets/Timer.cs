using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    public Text bestTimeText;
    public float time = 0f;

    public float bestTime = 9999f;

    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
        time += Time.deltaTime;
        timerText.text = time.ToString("F2");
        }
    }

    public void onResetTime()
    {
        time = 0;
    }

    public void OnLevelFinished()
    {
        isPaused = true;
        if (time < bestTime)
        {
            bestTime = time;
            bestTimeText.text = bestTime.ToString("F2");
        }
    }

    public void OnPlayAgain()
    {
        onResetTime();
        isPaused = false;
    }
}
