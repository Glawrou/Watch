using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class YandexRemoteTimeFetcher : RemoteTimeFetcher
{
    private const string timeSyncUrl = "https://yandex.com/time/sync.json";

    public override void GetTime(Action<DateTime> onResult, Action<string> onError)
    {
        StartCoroutine(GetTimeProcess(onResult, onError));
    }

    private IEnumerator GetTimeProcess(Action<DateTime> onResult, Action<string> onError)
    {
        var site = UnityWebRequest.Get(timeSyncUrl);
        yield return site.SendWebRequest();
        if (site.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke($"Failed to fetch time from server. Error: {site.error}");
        }
        else
        {
            var responseText = site.downloadHandler.text;
            onResult?.Invoke(JsonUtility.FromJson<YandexTimeData>(responseText).ToDateTime());
        }
    }

    [Serializable]
    private class YandexTimeData
    {
        /// <summary>
        /// Milliseconds
        /// </summary>
        public long time;

        private readonly DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime ToDateTime()
        {
            return StartTime.AddMilliseconds(time);
        }
    }
}
