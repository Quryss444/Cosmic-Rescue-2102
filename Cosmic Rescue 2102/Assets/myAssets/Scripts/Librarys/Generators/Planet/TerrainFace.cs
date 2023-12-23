using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    ShapeGenerator shapeGenerator;

    Mesh mesh;
    Vector2[] uv;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    float h = 0;

    public TerrainFace(ShapeGenerator shapeGenerator,Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);        

    }

    public void GenerateFace()
    {

        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution * resolution)*6];
        Color[] colors = new Color[vertices.Length]; 
        uv = new Vector2[vertices.Length];
        int triIndex = 0;

        Vector3 pointOnUnitCube;

        for (int y = 0;y<resolution;y++)
        {
            for(int x = 0;x<resolution;x++)
            {
                int i = x + y * resolution;

                Vector2 percent = new Vector2(x, y) / (resolution-1);
                pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
 
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);
                uv[i] = new Vector2(x,y);

                triIndex = GenerateTriangles(i, x, y, triIndex, triangles);
            }
        }

        for (int g = 0; g < vertices.Length - 1; g++)
        {
            h = Mathf.InverseLerp(shapeGenerator._elevationMinMax.Min, shapeGenerator._elevationMinMax.Max, Vector3.Magnitude(vertices[g]));

            colors[g] = shapeGenerator.settings.gradient.Evaluate(h);
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

    public void GenerateUVS()
    {

    }

    private int GenerateTriangles(int ii,int xx,int yy,int TI, int[] ta)
    {        
     
        if (xx != resolution - 1 && yy != resolution - 1)
        {
            ta[TI] = ii;
            ta[TI + 1] = ii + resolution + 1;
            ta[TI + 2] = ii + resolution;

            ta[TI + 3] = ii;
            ta[TI + 4] = ii + 1;
            ta[TI + 5] = ii + resolution + 1;

            TI += 6;
            return TI;
        }
        return TI;
    }
}
