using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    void Update()
    {
        InputKeyboard();
        //InputTouch();
    }

    
    private void InputKeyboard()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            _endPosition = Input.mousePosition;

            float deltaDist = _endPosition.x - _startPosition.x;

            Move(deltaDist);
        }
    }

    

    private void InputTouch()
    {
        Touch currentTouch = Input.GetTouch(0);

        if(currentTouch.phase == TouchPhase.Began)
        {
            touchStartPos = currentTouch.position;
        }
        else if (currentTouch.phase == TouchPhase.Moved || currentTouch.phase == TouchPhase.Ended)
        {
            touchEndPos = currentTouch.position;

            float deltaDist = touchEndPos.x - touchStartPos.x;

            Move(deltaDist);
        }

    }

    private void Move(float delta)
    {
        transform.position += Vector3.right * Time.deltaTime * delta * speed;
    }
}
