using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpCoins : PowerUpBase
{
    [Header("Collector Attribute")]
    public float CollectorRadius = 7;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.SetPowerUpCoinCollector(CollectorRadius);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetPowerUpCoinCollector();
    }
}
