using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float time;
    void Update()
    {
        //Zaman değerlerini alır
        time += Time.deltaTime;
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        timerText.text =minutes.ToString("00") + "." + seconds.ToString("00");
    }
}
