using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    public enum LastPowerUp
    {
        SPEED_UP,
        INVENCIBLE,
        FLY
    }

    [Header("Player Attributes")]
    public float initialSpeed = 1f;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Tags")]
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";

    [Header("Screens")]
    public GameObject startScreenUI;
    public GameObject endScreenUI;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private bool _isInvencible;
    private Vector3 _startPosition;
    private LastPowerUp _lastPowerUp;

    #region Start / Update
    private void Start()
    {
        uiTextPowerUp.text = "";
        _startPosition = transform.position;
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) { return; }

        MovePlayer();
        MoveLerp(target);
    }
    #endregion

    #region Movement
    private void MovePlayer()
    {
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void MoveLerp(Transform target)
    {
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
    }
    #endregion

    #region Controllers
    public void SetRun(bool run)
    {
        _canRun = run;
    }

    private void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }


    private void ResetSpeed()
    {
        _currentSpeed = initialSpeed;
    }

    private void SetHeight(float height, float powerUpDuration, float animDuration, Ease ease)
    {
        transform.DOMoveY(height, animDuration).SetEase(ease);
    }

    private void ResetHeight(float animDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y, animDuration).SetEase(ease);
    }
    #endregion

    #region Colliders
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(tagEnemy))
        {
            if (!_isInvencible)
            {
                EndGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagEndLine)
        {
            EndGame(); 
        }
    }
    #endregion

    #region Game Manager
    public void StartGame()
    {
        SetRun(true);
        startScreenUI.SetActive(false);
        endScreenUI.SetActive(false);
    }

    public void EndGame()
    {
        SetRun(false);
        endScreenUI.SetActive(true);
    }
    #endregion

    #region Power Up Actions
    public void SetUiTextPowerUp(string text = "")
    {
        uiTextPowerUp.text = text;
    }

    public void SetPowerUpSpeedUp(float speedToIncrease)
    {
        _lastPowerUp = LastPowerUp.SPEED_UP;
        SetSpeed(speedToIncrease);
        SetUiTextPowerUp("Speeding Up");
    }

    public void ResetPowerUpSpeedUp()
    {
        ResetSpeed();
        if (_lastPowerUp == LastPowerUp.SPEED_UP) { SetUiTextPowerUp(); }
    }

    public void SetPowerUpInvencible()
    {
        _lastPowerUp = LastPowerUp.INVENCIBLE;
        _isInvencible = true;
        SetUiTextPowerUp("Invencible");
    }

    public void ResetPowerUpInvencible()
    {
        _isInvencible = false;
        if (_lastPowerUp == LastPowerUp.INVENCIBLE) { SetUiTextPowerUp(); }
    }

    public void SetPowerUpFly(float height, float powerUpDuration, float animDuration, Ease ease)
    {
        _lastPowerUp = LastPowerUp.FLY;
        SetUiTextPowerUp("Flying");
        SetHeight(height, powerUpDuration, animDuration, ease);
    }

    public void ResetPowerUpFly(float animDuration, Ease ease)
    {
        SetUiTextPowerUp();
        ResetHeight(animDuration, ease);
        if (_lastPowerUp == LastPowerUp.FLY) { SetUiTextPowerUp(); }
    }
    #endregion
}
