using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{ 
    public enum FilterType { Simple,Rigid};
    public FilterType filterType;

    public SimpleNoiseSettings simpleNoiseSettings;
    public RigidNoiseSettings rigidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {

        [Range(1, 8)] public int numLayers = 4;
        [Range(-10, 10)] public float strength = 0.4f;
        [Range(-1, 10)] public float baseRoughness = 1.8f;
        [Range(-10, 10)] public float roughness = 3.35f;
        [Range(0, 2)] public float persistance = 0.42f;
        [Range(0, 2)] public float minValue = 0.97f;

        public Vector3 center;
    }

    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {

        [Range(-1, 1)] public float WeightMultiplier = 0.8f;

    }
}
