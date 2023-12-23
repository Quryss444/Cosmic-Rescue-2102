using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathClass
{
    public MathClass()
    {

    }

/// Percentage Conversions

    public float ValuetoPercent(float currentValue,float valueRange)
    {
        float val = (currentValue*100)/valueRange;
        return val;
    }

    public float PercentToValue(float currentPercent, float valueRange)
    {
        float val = (currentPercent * valueRange) / 100;
        return val;
    }

/// Temp Conversions    

    public float TempF_TempC(float F)
    {
        return 0f;
    }

    public float TempC_TempF(float C)
    {
        return 0f;
    }
}
