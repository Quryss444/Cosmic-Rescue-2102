using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory2 : MonoBehaviour
{
    [SerializeField] private PlanetGenerator planetGenerator;
    [SerializeField] private PlanetGenerator4 planetGenerator4;

    public PlanetFactory2() { }

    public void CreatePlanet(string name,int locX,int locY,int locZ, int numberOfVerts,Vector3 scale)
    {
        if (planetGenerator != null)
        {
            Debug.Log("A Planet named " + name + " was Discovered");
            planetGenerator.Generate(name,locX,locY,locZ,numberOfVerts,scale);
        }
        else
        {
            Debug.LogWarning("No Planet Generator Found");
            //GameObject.createPrimitive(PrimitiveType.Sphere);
        }
    }

    public void CreatePlanet4(string name, string type,Vector3 loc, Vector3 scale)
    {
        if (planetGenerator4 != null)
        {          
            Debug.Log("A Planet named " + name + " was Discovered");
            planetGenerator4.GeneratePlanet(name,type,loc,scale);
        }
        else
        {
            Debug.LogWarning("No Planet Generator Found");
            //GameObject.createPrimitive(PrimitiveType.Sphere);
        }
    }
}
