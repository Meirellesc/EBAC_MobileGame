using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece : MonoBehaviour
{
    public GameObject CurrentArt;

    public void ChangeArtPiece(GameObject artPiece)
    {
        if(CurrentArt != null) { Destroy(CurrentArt); }

        CurrentArt = Instantiate(artPiece, transform);
        CurrentArt.transform.localPosition = Vector3.zero;
    }
}
