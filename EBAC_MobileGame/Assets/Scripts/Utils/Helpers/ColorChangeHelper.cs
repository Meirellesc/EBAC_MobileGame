using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChangeHelper : MonoBehaviour
{
    public MeshRenderer MeshRenderer;

    public Color InitialColor = Color.white;

    public float LerpDuration = 1f;

    private Color _finalColor;

    private void OnValidate()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    public void Start()
    {
        _finalColor = MeshRenderer.materials[0].GetColor("_Color");

        LerpColor();
    }

    public void LerpColor()
    {
        MeshRenderer.materials[0].SetColor("_Color", InitialColor);

        MeshRenderer.materials[0].DOColor(_finalColor, LerpDuration).SetDelay(.3f);
    }
}
