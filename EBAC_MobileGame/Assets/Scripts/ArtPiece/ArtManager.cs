using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
        TYPE_04
    }

    public List<ArtSetup> ArtSetups;

    public ArtSetup GetArtSetupByType(ArtType artType)
    {
        return ArtSetups.Find(data => data.ArtType == artType);
    }
}

[Serializable]
public class ArtSetup
{
    public ArtManager.ArtType ArtType;

    public GameObject GameObject;
}
