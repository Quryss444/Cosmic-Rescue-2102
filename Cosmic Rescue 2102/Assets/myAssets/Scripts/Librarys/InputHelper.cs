using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHelper
{
    public InputHelper() { }

    public float Mouse_axisLimits(float currentValue,float min, float max,out float val)
    {
        if (currentValue > max) { val = max; }
        if (currentValue < min) { val = min; }
        else {  val = currentValue; }
        return currentValue;
    }

}
