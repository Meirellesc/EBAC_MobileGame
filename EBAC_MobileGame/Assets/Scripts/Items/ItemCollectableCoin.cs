using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public float LerpSpeed = 5f;
    public float MinDistance = 1f;

    private bool _collected;

    [SerializeField] private FloatHelper _floatHelper;

    private void OnValidate()
    {
        _floatHelper = GetComponent<FloatHelper>();
    }

    private void Start()
    {
        CoinAnimationManager.Instance.RegisterCoin(this);   
    }

    private void Update()
    {
        if(_collected)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, LerpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < MinDistance)
            {
                ItemManager.Instance.AddCoins();
                HideObject();
            }
        }
    }

    protected override void OnCollect()
    {
        _floatHelper.Stop();
        base.OnCollect();
        _collected = true;
    }
}
