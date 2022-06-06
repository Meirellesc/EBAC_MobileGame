using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    public float duration;

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
}
