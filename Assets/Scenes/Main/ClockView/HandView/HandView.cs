using System.Collections;
using UnityEngine;

public class HandView : MonoBehaviour
{
    private const float RotationSpeed = 0.1f;
    private const float Circle = -360f;

    private Coroutine _rotateProcess;

    public void SetValue(float value)
    {
        var targetZRotation = Circle * value;
        if (_rotateProcess != null)
        {
            StopCoroutine(_rotateProcess);
        }

        _rotateProcess = StartCoroutine(RotateTo(targetZRotation));
    }

    private IEnumerator RotateTo(float targetZRotation)
    {
        var startRotation = transform.rotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, 0, targetZRotation));
        var elapsedTime = 0f;
        while (elapsedTime < RotationSpeed)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / RotationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
