using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceGenerator : MonoBehaviour
{
    private GameObject sphere;
    private MeshRenderer renderer;
    private int scale = 4000;  
    
    public GameObject GenerateSurface(string name, Material surfaceMaterial)
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;

        renderer = sphere.GetComponent<MeshRenderer>();
        renderer.material = surfaceMaterial;

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        Destroy(sphereCollider);
        sphere.AddComponent<MeshCollider>();

        sphere.transform.localScale = new Vector3(scale, scale, scale);

        if (GetComponent<SphereCollider>() != null)
        {
            Debug.Log("Collider not Destroyed");
        }

        return sphere;
    }

}
