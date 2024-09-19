using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class TimingHandler : MonoBehaviour
{
    [SerializeField] private float timePerRound;
    [SerializeField] private Color inBetweenRoundsColor;
    [SerializeField] private Color protectedTimeColor;
    [SerializeField] private Color nonProtectedTimeColor;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private Image background;
    private Boolean isTiming = false;
    private int roundsElasped = 0;
    private PlayerInput playerInput;
    private InputAction touchPressAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
    }

    private void OnEnable()
    {
        touchPressAction.performed += RegisterPress;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= RegisterPress;
    }

    private void RegisterPress(InputAction.CallbackContext context)
    {
        print("Press Registered");
        if (!isTiming)
        {
            if (roundsElasped < 4)
            {
                isTiming = true;
                roundsElasped++;
                roundText.text = roundsElasped.ToString();
                StartCoroutine(ElapseRound(timePerRound));
            } 
            else if (roundsElasped < 6)
            {
                isTiming = true;
                roundsElasped++;
                roundText.text = roundsElasped.ToString();
                StartCoroutine(ElapseRound(timePerRound - 1));
            }
        }
        else
        {
            isTiming = false;
        }
    }

    IEnumerator ElapseRound(float minutes)
    {
        float timeElapsed = 0;
        while (timeElapsed <= minutes * 60 + 1 && isTiming == true)
        {
            UpdateTimer(timeElapsed);
            UpdateBackground(timeElapsed, minutes);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        isTiming = false;
        background.color = inBetweenRoundsColor;
        if (roundsElasped >= 6)
        {
            roundsElasped = 0;
        }
    }

    private void UpdateTimer(float timeElapsed)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);
        string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
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