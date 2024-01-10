using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ClockView : MonoBehaviour
{
    [SerializeField] private Text _timeText;

    [Header("Hands")]
    [SerializeField] private HandView _minuteHand;
    [SerializeField] private HandView _hourHand;
    [SerializeField] private HandView _secondHand;

    public void SetTime(DateTime dateTime)
    {
        _minuteHand.SetValue((float)dateTime.Minute / 60);
        _hourHand.SetValue((float)dateTime.Hour / 12);
        _secondHand.SetValue((float)dateTime.Second / 60);
        _timeText.text = dateTime.ToString("H:mm:ss");
    }
}
