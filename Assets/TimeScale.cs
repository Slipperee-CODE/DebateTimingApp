using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private float maxTimeScale;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateTimeScale()
    {
        if (slider.value == 0)
        {
            Time.timeScale = 1;
        } else
        {
            Time.timeScale = maxTimeScale * slider.value;
        }   
    }
}
