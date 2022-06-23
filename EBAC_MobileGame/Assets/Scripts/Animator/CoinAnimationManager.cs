using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class CoinAnimationManager : Singleton<CoinAnimationManager>
{
    public List<ItemCollectableCoin> Coins;

    [Header("Coins Animations")]
    public float CoinMaxScale = 0.7f;
    public float TimeBetweenCoins = .1f;
    public float CoinScaleDuration = .1f;
    public Ease ScaleEase = Ease.OutBack;

    void Start()
    {
        Coins = new List<ItemCollectableCoin>();
    }

    #region Actions
    public void RegisterCoin(ItemCollectableCoin coin)
    {
        if (!Coins.Contains(coin))
        {
            Coins.Add(coin);

            // Set the pieces scale to zero for animation
            coin.transform.localScale = Vector3.zero;
        }
    }

    public void SortCoinList()
    {
        Coins = Coins.OrderBy(data => Vector3.Distance(this.transform.position, data.transform.position)).ToList();
    }

    public void StartCoinAnimation()
    {
        StartCoroutine(ScaleCoinsByTime());
    }
    #endregion

    #region Coroutines
    private IEnumerator ScaleCoinsByTime()
    {
        SortCoinList();

        yield return null;

        for (int i = 0; i < Coins.Count; i++)
        {
            // Do the scale
            Coins[i].transform.DOScale(CoinMaxScale, CoinScaleDuration).SetEase(ScaleEase);

            // Wait a time
            yield return new WaitForSeconds(TimeBetweenCoins);
        }
    }
    #endregion
}
