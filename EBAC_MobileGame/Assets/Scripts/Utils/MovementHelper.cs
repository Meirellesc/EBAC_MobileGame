using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> TargetPositions;
    
    public float DurationBetweenTargets = 1f;

    private int _targetIndex = 0;

    private void Start()
    {
        // Set the initial position as the first target position
        _targetIndex = Random.Range(0, TargetPositions.Count);
        transform.localPosition = TargetPositions[_targetIndex].position;
        NextTargetIndex();

        StartCoroutine(StartMovement());
    }

    IEnumerator StartMovement()
    {
        float time = 0;

        while(true)
        {
            // Keeps the current position
            Vector3 currentPosition = transform.position;

            while(time < DurationBetweenTargets)
            {
                // Do the lerp moviment
                transform.position = Vector3.Lerp(currentPosition, TargetPositions[_targetIndex].position, (time / DurationBetweenTargets));

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
        if (_targetIndex >= TargetPositions.Count) { _targetIndex = 0; }
    }
}
