using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    public float speed = 1f;

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

    // Update is called once per frame
    void Update()
    {
        if (!_canRun) { return; }

        MovePlayer();
        MoveLerp(target);
    }

    #region Movement
    private void MovePlayer()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
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
}
