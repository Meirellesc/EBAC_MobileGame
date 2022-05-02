using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [Header("Items")]
    public int coins;

    [Header("UI")]
    public TMP_Text pointsUI;

    private void Awake()
    {
        Instance = this;
        Reset();
    }

    private void Update()
    {
        UpdatePointsUI();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
    }

    #region UI
    private void UpdatePointsUI()
    {
        pointsUI.text = coins.ToString();
    }
    #endregion
}
