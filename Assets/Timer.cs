using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    static float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }


    public static void ResetTime()
    {
        time = 0;
    }

    public static string GetTime()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        float milliSeconds = (time % 1) * 1000;

        string textTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);

        return textTime;

    }
}