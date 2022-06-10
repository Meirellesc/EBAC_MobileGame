using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform Container;

    public List<GameObject> Levels;

    [Header("Pieces List")]
    public List<LevelPieceBase> LevelPiecesMiddle;
    public List<LevelPieceBase> LevelPiecesStart;
    public List<LevelPieceBase> LevelPiecesEnd;

    [Header("Pieces' Quantity")]
    public int pieceNumberMiddle = 5;
    public int pieceNumberStart = 3;
    public int pieceNumberEnd = 1;


    [SerializeField] private int _index;
    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedPieces;

    #region Start / Update
    private void Awake()
    {
        //SpawnNextLevel();
        CreateLevelPieces();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnNextLevel();
        }
    }
    #endregion

    #region Level
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
    #endregion

    #region Piece
    private void CreateLevelPieces()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < pieceNumberStart; i++)
        {
            CreateLevelPiece(LevelPiecesStart);
        }

        for (int i = 0; i < pieceNumberMiddle; i++)
        {
            CreateLevelPiece(LevelPiecesMiddle);
        }

        for (int i = 0; i < pieceNumberEnd; i++)
        {
            CreateLevelPiece(LevelPiecesEnd);
        }
    }

    private void CreateLevelPiece(List<LevelPieceBase> levelPieces)
    {
        // Get next random piece
        LevelPieceBase piece = levelPieces[Random.Range(0, levelPieces.Count)];

        // Instantiate the new piece
        LevelPieceBase spawnedPiece = Instantiate(piece, Container);

        // Check if already exist pieces in the list
        if(_spawnedPieces.Count > 0)
        {
            // Get the last piece
            LevelPieceBase lastPiece = _spawnedPieces.Last();

            spawnedPiece.transform.position = lastPiece.EndPiece.position;
        }

        // Fix the position of the new piece, conecting it with the last one
        _spawnedPieces.Add(spawnedPiece);
    }
    #endregion

}
