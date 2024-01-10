using System;
using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private RemoteTimeFetcher _remoteTimeFetcher;
    [SerializeField] private ClockView _clockView;

    private DateTime _currentDateTime;
    private Coroutine _timeTickProcess;

    private const int TimeBetweenChecks = 3600;

    private void Awake()
    {
        SetTime(DateTime.Now);
        StartCoroutine(CheckTimeProcess());
    }

    public void StartClock()
    {
        if (_timeTickProcess != null)
        {
            StopCoroutine(_timeTickProcess);
        }

        _timeTickProcess = StartCoroutine(TimeTickProcess());
    }

    private void UpdateTimeView()
    {
        _clockView.SetTime(_currentDateTime);
    }

    private void GetTimeNetwork()
    {
        _remoteTimeFetcher.GetTime(ResultTimeNetworkHandler, ErrorTimeNetworkHandler);
    }

    private void SetTime(DateTime dateTime)
    {
        _currentDateTime = dateTime;
        UpdateTimeView();
        StartClock();
    }

    private void ResultTimeNetworkHandler(DateTime dateTime)
    {
        Debug.Log(dateTime.ToString());
        SetTime(dateTime);
    }

    private void ErrorTimeNetworkHandler(string error)
    {
        Debug.LogError(error);
    }

    private IEnumerator TimeTickProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Tick();
        }
    }

    private void Tick()
    {
        _currentDateTime = _currentDateTime.AddSeconds(1);
        UpdateTimeView();
    }

    private IEnumerator CheckTimeProcess()
    {
        while (true)
        {
            GetTimeNetwork();
            yield return new WaitForSeconds(TimeBetweenChecks);
        }
    }
}
