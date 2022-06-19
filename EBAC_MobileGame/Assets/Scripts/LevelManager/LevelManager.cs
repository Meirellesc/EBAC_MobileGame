using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform Container;

    public List<GameObject> Levels;

    public List<LevelPieceBaseSetup> LevelPieceBaseSetups;

    private int _index;
    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup _currentPBSetup;

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
            //SpawnNextLevel();
            CreateLevelPieces();
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
        CleanSpawnedPieces();

        // Check the index of level piece base setup
        if(_currentPBSetup != null)
        {
            _index++;

            if(_index >= LevelPieceBaseSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        // Get the current level piece base setup
        _currentPBSetup = LevelPieceBaseSetups[_index];

        // Instantiate the start pieces
        for (int i = 0; i < _currentPBSetup.pieceNumberStart; i++)
        {
            CreateLevelPiece(_currentPBSetup.LevelPiecesStart);
        }

        // Instantiate the middle pieces
        for (int i = 0; i < _currentPBSetup.pieceNumberMiddle; i++)
        {
            CreateLevelPiece(_currentPBSetup.LevelPiecesMiddle);
        }

        // Instantiate the end pieces
        for (int i = 0; i < _currentPBSetup.pieceNumberEnd; i++)
        {
            CreateLevelPiece(_currentPBSetup.LevelPiecesEnd);
        }

        // Change the colors of scenario accordlying of current setup art type
        ColorManager.Instance.ChangeColorByType(_currentPBSetup.ArtType);
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
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        List<ArtPiece> currentArtPieces = spawnedPiece.GetComponentsInChildren<ArtPiece>().ToList();

        if (currentArtPieces != null && currentArtPieces.Count > 0)
        {
            foreach (ArtPiece currentArtPiece in currentArtPieces)
            {
                GameObject artPieceToUpdate = ArtManager.Instance.GetArtSetupByType(_currentPBSetup.ArtType).GameObject;
                currentArtPiece.ChangeArtPiece(artPieceToUpdate);
            }
        }

        // Fix the position of the new piece, conecting it with the last one
        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count - 1; i > 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }
    #endregion

}
