using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilter_Factory
{ 
    public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
    {
        INoiseFilter nf;

        switch (settings.filterType) 
        { 
            case NoiseSettings.FilterType.Simple :
                nf = new NoiseFilter(settings.simpleNoiseSettings);
                return nf;
            case NoiseSettings.FilterType.Rigid :
                nf = new RigidNoiseFilter(settings.rigidNoiseSettings);
                return nf;
        }
        return null;
    }
}
