using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatHelper : MonoBehaviour
{
    public float DurationBetweenTargets = 1f;
    public float MaxHeight = 0.5f;
    public float MinHeight = 0.5f;


    private List<Vector3> _targetPositions = new List<Vector3>();
    private int _targetIndex = 0;

    private bool _isFloating;

    private void Start()
    {
        SetUpTargets();

        // Set the initial position as the first target position
        _targetIndex = Random.Range(0, _targetPositions.Count);
        transform.localPosition = _targetPositions[_targetIndex];
        NextTargetIndex();

        _isFloating = true;
        StartCoroutine(FloatCoroutine());
    }

    public void Stop()
    {
        _isFloating = false;
        StopAllCoroutines();
    }

    private void SetUpTargets()
    {
        Vector3 maxTarget = transform.localPosition;
        maxTarget.y += MaxHeight;

        Vector3 minTarget = transform.localPosition;
        minTarget.y -= MinHeight;

        _targetPositions.Add(maxTarget);
        _targetPositions.Add(minTarget);
    }

    IEnumerator FloatCoroutine()
    {
        float time = 0;

        while (_isFloating)
        {
            // Keeps the current position
            Vector3 currentPosition = transform.localPosition;

            while (time < DurationBetweenTargets)
            {
                // Do the lerp moviment
                transform.localPosition = Vector3.Lerp(currentPosition, _targetPositions[_targetIndex], (time / DurationBetweenTargets));

                // Add the delta time
                time += Time.deltaTime;
                yield return null;
            }

            // Get next target position
            NextTargetIndex();

            // Reset the time reference to go to the next target
            time = 0;

            // Wait a frame to return
            yield return null;
        }
    }

    private void NextTargetIndex()
    {
        _targetIndex++;

        // Check if achieved the last index and reset it
        if (_targetIndex >= _targetPositions.Count) { _targetIndex = 0; }
    }
}
