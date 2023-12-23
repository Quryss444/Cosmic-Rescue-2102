using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    public ShapeSettings settings;
    INoiseFilter[] noiseFilters;
    
    public MinMax _elevationMinMax;

    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];

        for(int i = 0;i< noiseFilters.Length;i++)
        {
            noiseFilters[i] = NoiseFilter_Factory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }

        _elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0f;
        float el = 0;

        if(noiseFilters.Length>0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        }
        if(settings.noiseLayers[0].enabled) 
        {
            el = firstLayerValue;
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                el += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;               
            }
        }
        
        el = settings.PlanetRadius * (1 + el);

        _elevationMinMax.AddValue(el);

        return pointOnUnitSphere * el;
    }
}
