using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleHelper : MonoBehaviour
{
    [Header("Scale Animation")]
    public float ScaleDuration = .1f;
    public float MaxScale = 1f;
    public Ease ScaleEase = Ease.OutBack;

    public void Scale()
    {
        transform.DOScale(MaxScale,ScaleDuration).SetEase(ScaleEase);
    }

    public void ScaleByEndValue(Vector3 endScale)
    {
        transform.DOScale(endScale, ScaleDuration).SetEase(ScaleEase);
    }
}
