using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 _pastPosition;

    void Update()
    {
        InputController();
    }

    private void InputController()
    {
        if(Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - _pastPosition.x);
        }

        _pastPosition = Input.mousePosition;
    }

    private void Move(float delta)
    {
        transform.position += Vector3.right * Time.deltaTime * delta * speed;
    }
}
