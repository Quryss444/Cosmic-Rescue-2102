using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator4 : MonoBehaviour
{
    [Header("Generators")]
    [SerializeField] private SurfaceGenerator surfaceGenerator;
    [SerializeField] private CraterGenerator craterGenerator;
    [SerializeField] private VertexColorGenerator vertexColorGenerator;
    [Space(1)]

    [Header("Planet Settings")] 
    public float Mass = 12f;
    public float Multiplier = 6f;
    [Space(1)]
    
    [Header("Surface Settings")]
    public Material surfaceMaterial;
    public Gradient gradient = new Gradient();
    [Space(1)]

    [Header("Ocean Settings")]
    [Space(1)]

    [Header("Atmosphere Settings")]
    [Space(1)]

    [Header("Crater Settings")]
    [Range(0, 150)]        public float craterPosVertIndex = 1f;
    [Range(0, 20)]         public int numberOfCraters      = 1;
    [Range(-0.5f, 0.5f)]   public float strength           = 0.5f;
    [Range(0, 0.32f)]      public float craterDepth        = 0.12f;

    private float[] CraterData;

    public void GeneratePlanet(string name, string type, Vector3 Loc, Vector3 scale)
    {
        CraterData = new float[10];
        SetCraterData(CraterData);

        //Create Base Planet Object
        GameObject PlanetObject = GenerateBase(name,type);
        //Create Planet Surface
        GameObject PlanetSurface = surfaceGenerator.GenerateSurface("Surface", surfaceMaterial);
        //Create Craters
        craterGenerator.GenerateCraters(PlanetSurface,CraterData);
        //Create Mesh Colors
        vertexColorGenerator.GenerateVertexColor(PlanetSurface, gradient);

        PlanetSurface.transform.parent   = PlanetObject.transform;
        PlanetSurface.transform.position = PlanetObject.transform.position;
        PlanetSurface.transform.rotation = PlanetObject.transform.rotation;

        PlanetObject.transform.position = Loc;
        PlanetObject.transform.localScale = scale;
    }

    private GameObject GenerateBase(string n,string type)
    {   
        GameObject pe = new GameObject(n);
        pe.tag = type;
        //pe.AddComponent<Rigidbody>();
        pe.AddComponent<GravityReceiver>();
        //pe.GetComponent<Rigidbody>().useGravity = false;

        return pe;//new Planet Empty Base Object
    }

    private void SetCraterData(float[] craterData)
    {
        CraterData[0] = craterPosVertIndex; CraterData[1] = (float)numberOfCraters; CraterData[2] = 0f;
        CraterData[3] = craterDepth; CraterData[4] = strength; CraterData[5] = 0f;
    }
}
