using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("Action Attributes")]
    public float duration;

    [Header("Animation")]
    public float RotationSpeed = 5f;

    private void Start()
    {
        transform.Rotate(20f, 0f, 0f);
    }

    private void Update()
    {
        Rotate();
    }

    #region Actions
    protected override void OnCollect()
    {
        base.OnCollect();
        HideObject();
        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    { 
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp() { }
    #endregion

    #region Utils
    private void Rotate()
    {
        transform.Rotate(0f, 45f * Time.deltaTime * RotationSpeed, 0f, Space.World);
    }
    #endregion
}
