using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBaseSetup : ScriptableObject
{
    [Header("Pieces List")]
    public List<LevelPieceBase> LevelPiecesStart;
    public List<LevelPieceBase> LevelPiecesMiddle;
    public List<LevelPieceBase> LevelPiecesEnd;

    [Header("Pieces' Quantity")]
    public int pieceNumberStart = 3;
    public int pieceNumberMiddle = 5;
    public int pieceNumberEnd = 1;

    [Header("Art")]
    public ArtManager.ArtType ArtType;
}
