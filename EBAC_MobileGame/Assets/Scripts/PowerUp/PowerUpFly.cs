using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PowerUpFly : PowerUpBase
{
    [Header("Fly Attribute")]
    public float AmountHeight;
    public float AnimDuration;
    public Ease Ease;


    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.SetPowerUpFly(AmountHeight, duration, AnimDuration, Ease);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetPowerUpFly(AnimDuration, Ease);
    }
}
