using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator3 : MonoBehaviour
{
    [Header("Number of Planet Vertices")]
    [Range(2, 256)]
    public int resolution = 100;

    public bool AutoUpdate = true;

    public enum FaceRenderMask {All,Top,Bottom,Left,Right,Front,Back};
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    public ShapeGenerator shapeGenerator;
    public ColorGenerator colorGenerator;
    
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    

    [HideInInspector]
    public bool shapeSettingsFoldOut;

    [HideInInspector]
    public bool colorSettingsFoldOut;

    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);
        colorGenerator = new ColorGenerator(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0) 
        {
            meshFilters = new MeshFilter[6];      
        }

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0;i<6;i++) 
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);

            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }

    }

    public void GenerateMesh()
    {
        for(int i = 0; i<6; i++) 
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].GenerateFace();
            }
        }
         
        colorGenerator.UpdateElevation(shapeSettings.PlanetRadius + shapeGenerator._elevationMinMax.Min, shapeGenerator._elevationMinMax.Max);

    }

    public void GenerateColors()
    {
        foreach(MeshFilter mf in meshFilters)
        {
            mf.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.PlanetColor;
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnColorSettingsUpdated()
    {
        if (AutoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }

    public void OnShapeSettingsUpdated()
    {
        if (AutoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

}
