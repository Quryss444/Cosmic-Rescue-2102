using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///////////////////////////////////////////////////////////////
///Quryss Krystyan 12-05-2023 
/// 
///Planet Object Structure
///
/// Empty Base
/// Surface - [MeshFilter,MeshRenderer,Collider,GR
/// Gravity Well - [ColliderTrigger,GravityWell Script]
/// 
/// 
///////////////////////////////////////////////////////////////
/// </summary>
public class PlanetGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D displacementMap;
    [Header("Generator Settings")]
    [Space(10)]
    [Header("Planet Type")]
    public string Type = "Planet";
    [Space(10)]
    [Header("Planet Mass")]
    public float Mass = 1200f;
    [Space(10)]
    [Header("Gravity Well Size")]
    public float Multiplier = 6f;

    public GameObject Generate(string name, int locX, int locY, int locZ, int subdivisions, Vector3 scale)
    {
        int index = 0;

        GameObject PlanetEmpty = GenerateBase(name);

        GameObject PlanetObject = new GameObject("Surface");

        MeshFilter meshFilter = PlanetObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = PlanetObject.AddComponent<MeshRenderer>();

        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = new Color(Random.Range(0, 255),Random.Range(0, 255),Random.Range(0, 255));
        
        Mesh mesh = new Mesh();

        int numVertices = (subdivisions + 1) * (subdivisions + 1);
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[subdivisions * subdivisions * 6];


        ///////////////////////////////////////////////////////////////////////
        for (int i = 0; i <= subdivisions; i++)
        {
            for (int j = 0; j <= subdivisions; j++)
            {
                float u = i / (float)subdivisions;
                float v = j / (float)subdivisions;

                float theta = u * Mathf.PI * 2;
                float phi = v * Mathf.PI;

                float x = Mathf.Sin(theta) * Mathf.Sin(phi);
                float y = Mathf.Cos(phi);
                float z = Mathf.Cos(theta) * Mathf.Sin(phi);

                vertices[i * (subdivisions + 1) + j] = new Vector3(x, y, z);
            }
        }

        for (int i = 0; i < subdivisions; i++)
        {
            for (int j = 0; j < subdivisions; j++)
            {
                triangles[index++] = i * (subdivisions + 1) + j;
                triangles[index++] = (i + 1) * (subdivisions + 1) + j;
                triangles[index++] = i * (subdivisions + 1) + j + 1;

                triangles[index++] = (i + 1) * (subdivisions + 1) + j;
                triangles[index++] = (i + 1) * (subdivisions + 1) + j + 1;
                triangles[index++] = i * (subdivisions + 1) + j + 1;
            }
        }
        ///////////////////////////////////////////////////////////////////////
        

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        PlanetObject.transform.position = new Vector3(locX, locY, locZ);
        PlanetObject.transform.localScale = scale;

        PlanetObject.transform.parent = PlanetEmpty.transform;
        PlanetEmpty.transform.position = PlanetObject.transform.position;

        MeshCollider meshCollider = PlanetObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        GenerateGravityWell(PlanetObject, PlanetEmpty,scale);

        return PlanetObject;
    }

    private GameObject GenerateBase(string n)
    {   //Create Planet Empty Object
        GameObject pe = new GameObject(n);
        pe.tag = Type;
        pe.AddComponent<Rigidbody>();
        pe.AddComponent<GravityReceiver>();
        pe.GetComponent<Rigidbody>().useGravity = false;

        return pe;
    }

    private void GenerateGravityWell(GameObject po,GameObject ep,Vector3 scale)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "Gravity Well";
        sphere.GetComponent<SphereCollider>().isTrigger = true;
        sphere.AddComponent<GravityWell>();
              
        sphere.transform.parent = ep.transform;//make parent of Empty Planet Object
        sphere.transform.position = po.transform.position;
        sphere.transform.rotation = po.transform.rotation;
        sphere.transform.localScale = new Vector3(scale[0]*Multiplier, scale[1] * Multiplier, scale[2] * Multiplier);

        Destroy(sphere.GetComponent<MeshRenderer>());
    }

    private float CalculateGravityWellSize(GameObject g)
    {
        return 0f;
    }
}


















































































































