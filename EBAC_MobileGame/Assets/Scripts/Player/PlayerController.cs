using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : Singleton<PlayerController>
{
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

    private Vector3 _pos;
    private bool _canRun;
    [SerializeField] private float _currentSpeed;

    #region Start / Update
    private void Start()
    {
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

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }


    public void ResetSpeed()
    {
        _currentSpeed = initialSpeed;
    }
    #endregion

    #region Colliders
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(tagEnemy))
        {
            EndGame();
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
    public void PowerUpSpeedUp(float speedToIncrease)
    {
        SetSpeed(speedToIncrease);
    }
    #endregion
}
