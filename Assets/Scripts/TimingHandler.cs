using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimingHandler : MonoBehaviour
{
    [SerializeField] private float timePerRound;
    [SerializeField] private Color inBetweenRoundsColor;
    [SerializeField] private Color protectedTimeColor;
    [SerializeField] private Color nonProtectedTimeColor;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image background;
    private Boolean isTiming = false;
    private int roundsElasped = 0;

    private void Start()
    {

    }

    private void RegisterPress()
    {
        if (!isTiming)
        {
            if (roundsElasped < 5)
            {
                ElapseRound(timePerRound);
                isTiming = true;
            } 
            else if (roundsElasped < 7)
            {
                ElapseRound(timePerRound - 1);
                isTiming = true;
            }
            else
            {
                roundsElasped = 0;
            }
        }
        else
        {
            isTiming = false;
            background.color = inBetweenRoundsColor;
            roundsElasped++;
        }
    }

    private void ElapseRound(float minutes)
    {
        float timeElapsed = 0;
        while (timeElapsed <= minutes * 60 && isTiming == true)
        {
            UpdateTimer(timeElapsed);
            UpdateBackground(timeElapsed, minutes);

            timeElapsed += Time.deltaTime;
        }
        isTiming = false;
    }

    private void UpdateTimer(float timeElapsed)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);
        string timeText = string.Format("{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds);
        timerText.text = timeText;
    }

    private void UpdateBackground(float timeElapsed, float minutes)
    {
        if (timeElapsed <= 60 || timeElapsed >= (minutes - 1) * 60)
        {
            background.color = protectedTimeColor;
        }
        else
        {
            background.color = nonProtectedTimeColor;
        }
    }
}