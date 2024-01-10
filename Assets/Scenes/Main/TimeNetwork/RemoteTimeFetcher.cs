using System;
using UnityEngine;

public abstract class RemoteTimeFetcher : MonoBehaviour
{
    public abstract void GetTime(Action<DateTime> onResult, Action<string> onError);
}
