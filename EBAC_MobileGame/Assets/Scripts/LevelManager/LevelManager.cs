using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform Container;

    public List<GameObject> Levels;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private void Awake()
    {
        SpawnNextLevel();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnNextLevel();
        }
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if(_index >= Levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(Levels[_index], Container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

}
