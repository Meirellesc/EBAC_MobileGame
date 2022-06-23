using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using Cinemachine;

public class PlayerController : Singleton<PlayerController>
{
    public enum LastPowerUp
    {
        SPEED_UP,
        INVENCIBLE,
        FLY,
        COIN_COLLECTOR
    }

    [Header("Movement Attributes")]
    public Transform TargetLerp;
    public float LerpSpeed = 1f;
    public float InitialSpeed = 1f;

    [Header("Tags")]
    public string TagEnemy = "Enemy";
    public string TagEndLine = "EndLine";
    public string TagLeftCamera = "LeftCamera";
    public string TagRightCamera = "RightCamera";
    public string TagUpCamera = "UpCamera";

    [Header("Camera Settings")]
    public CinemachineVirtualCamera CameraRight;
    public CinemachineVirtualCamera CameraUp;
    public CinemachineVirtualCamera CameraLeft;

    [Header("Screens")]
    public GameObject StartScreenUI;
    public GameObject EndScreenUI;

    [Header("Coin Setup")]
    public GameObject CoinCollector;

    [Header("Animator")]
    public PlayerAnimatorManager AnimatorManager;

    [Header("Text")]
    public TextMeshPro UiTextPowerUp;

    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private bool _isInvencible;
    private Vector3 _startPosition;
    private Vector3 _initialCoinCollectorScale;
    private LastPowerUp _lastPowerUp;
    private float _baseSpeedToAnimation = 7f;

    #region Start / Update
    private void Start()
    {
        UiTextPowerUp.text = "";
        _startPosition = transform.position;
        _initialCoinCollectorScale = CoinCollector.transform.localScale;
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) { return; }

        MovePlayer();
        MoveLerp(TargetLerp);
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

        transform.position = Vector3.Lerp(transform.position, _pos, LerpSpeed * Time.deltaTime);
    }

    private void MoveTransformZ(Transform transform, float endValue, float duration)
    {
        transform.DOMoveZ(endValue, duration).SetRelative();
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
        _currentSpeed = InitialSpeed;
    }

    private void SetHeight(float height, float powerUpDuration, float animDuration, Ease ease)
    {
        transform.DOMoveY(height, animDuration).SetEase(ease);
    }

    private void ResetHeight(float animDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y, animDuration).SetEase(ease);
    }

    private void SetCoinCollectorRadius(float radius)
    {
        CoinCollector.transform.localScale = Vector3.one * radius;
    }

    private void ResetCoinCollectorRadius()
    {
        CoinCollector.transform.localScale = _initialCoinCollectorScale;
    }
    #endregion

    #region Colliders
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(TagEnemy))
        {
            if (!_isInvencible)
            {
                MoveTransformZ(collision.transform, 0.7f, 0.3f);
                MoveTransformZ(this.transform, -0.7f, 0.3f);
                EndGame(PlayerAnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == TagEndLine)
        {
            EndGame(PlayerAnimatorManager.AnimationType.IDLE); 
        }

        if (other.transform.tag == TagLeftCamera)
        {
            CameraRight.gameObject.SetActive(false);
            CameraUp.gameObject.SetActive(false);
            CameraLeft.gameObject.SetActive(true);
        }
        else if (other.transform.tag == TagRightCamera)
        {
            CameraLeft.gameObject.SetActive(false);
            CameraUp.gameObject.SetActive(false);
            CameraRight.gameObject.SetActive(true);
        }
        else if (other.transform.tag == TagUpCamera)
        {
            CameraLeft.gameObject.SetActive(false);
            CameraRight.gameObject.SetActive(false);
            CameraUp.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Game Manager
    public void StartGame()
    {
        SetRun(true);
        StartScreenUI.SetActive(false);
        EndScreenUI.SetActive(false);

        AnimatorManager.Play(PlayerAnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    public void EndGame(PlayerAnimatorManager.AnimationType animType)
    {
        SetRun(false);
        EndScreenUI.SetActive(true);
        AnimatorManager.Play(animType);
    }
    #endregion

    #region Power Up Actions
    public void SetUiTextPowerUp(string text = "")
    {
        UiTextPowerUp.text = text;
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
        ResetHeight(animDuration, ease);
        if (_lastPowerUp == LastPowerUp.FLY) { SetUiTextPowerUp(); }
    }

    public void SetPowerUpCoinCollector(float radius)
    {
        _lastPowerUp = LastPowerUp.COIN_COLLECTOR;
        SetUiTextPowerUp("Coin Collector");
        SetCoinCollectorRadius(radius);
    }

    public void ResetPowerUpCoinCollector()
    {
        ResetCoinCollectorRadius();
        if (_lastPowerUp == LastPowerUp.COIN_COLLECTOR) { SetUiTextPowerUp(); }
    }
    #endregion
}
