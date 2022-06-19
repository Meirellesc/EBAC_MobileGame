using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArtManager;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> ColorMaterials;
    public List<ColorSetup> ColorSetups;

    public void ChangeColorByType(ArtType artType)
    {
        ColorSetup cSetup = ColorSetups.Find(data => data.ArtType == artType);

        for(int i = 0; i < ColorMaterials.Count; i++)
        {
            ColorMaterials[i].SetColor("_Color", cSetup.Colors[i]);
        }
    }
}

[Serializable]
public class ColorSetup
{
    public ArtManager.ArtType ArtType;
    public List<Color> Colors;
}
