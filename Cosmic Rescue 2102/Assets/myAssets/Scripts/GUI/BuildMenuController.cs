using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.SceneManagement;
//using UnityEngine.Debug;

public class BuildMenuController : MonoBehaviour
{
    [Header("Planet Factory Settings")]
    public PlanetFactory2 planetFactory;

    private int MaxSpawnDistance = 175;
    private int MaxSpawnCount = 20;
    private float scaleMin = 80f;
    private float scaleMax = 280f;

    public string DestroyPlanet = "Planet4";

    public void GeneratePlanet4()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        float val = 1;// Random.Range(scaleMin, scaleMax);

        Vector3 loc = new Vector3(6, 0, 0);
        Vector3 scale = new Vector3(val, val, val);

        planetFactory.CreatePlanet4(" Venus-ish " , "Planet4", loc, scale);

        sw.Stop();
        System.TimeSpan et = sw.Elapsed;
        UnityEngine.Debug.Log("Planet Generated in: " + et);
    }

    public void GeneratePlanet()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        float scale = Random.Range(scaleMin, scaleMax);
        planetFactory.CreatePlanet("Earth-ish:" + MaxSpawnCount, Random.Range(-MaxSpawnDistance, MaxSpawnDistance), Random.Range(-MaxSpawnDistance, MaxSpawnDistance), Random.Range(-MaxSpawnDistance, MaxSpawnDistance), 100, new Vector3(scale, scale, scale));


        sw.Stop();
        System.TimeSpan et = sw.Elapsed;
        UnityEngine.Debug.Log("Planet Generated in: " + et);
    }

    public void GenerateSolarSystem()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();     

        for (int i = 0;i<= MaxSpawnCount;i++)
        {
            float scale = Random.Range(scaleMin, scaleMax);
            planetFactory.CreatePlanet("Earth-ish:"+(MaxSpawnCount + i), Random.Range(-MaxSpawnDistance, MaxSpawnDistance), Random.Range(-MaxSpawnDistance, MaxSpawnDistance), Random.Range(-MaxSpawnDistance, MaxSpawnDistance), 100, new Vector3(scale, scale, scale));
        }

        sw.Stop();

        System.TimeSpan et = sw.Elapsed;

        UnityEngine.Debug.Log("Solar System Generated in: " + et);
    }

    public void DestroyAllPlanets()
    {
        GameObject[] _Planets = GameObject.FindGameObjectsWithTag(DestroyPlanet);
        
        foreach(GameObject _planet in _Planets)
        {
            Object.Destroy(_planet); 
            UnityEngine.Debug.Log("Destroyed " + _planet.name);
        }
    }

    public void GoToGarage()
    {
        SceneManager.LoadScene("Garage_1");
    }
}
