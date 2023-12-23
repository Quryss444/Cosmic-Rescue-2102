using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    ColorSettings colorSettings;    

    public ColorGenerator(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
    }

    public void UpdateElevation(float emin, float emax)
    {
        colorSettings.planetMaterial.SetVector("_elevationMinMax",new Vector4(emin,emax,0,0));
    }

    public void GenerateColors()
    {

    }
}
