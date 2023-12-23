using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexColorGenerator : MonoBehaviour
{
    private Mesh mesh;
    private Color[] colors;
    private Vector3[] Vertices;

    private float Emin;
    private float Emax;

    public void GenerateVertexColor(GameObject planetSurface, Gradient gradient)
    {
        mesh = planetSurface.GetComponent<MeshFilter>().mesh;
        Vertices = mesh.vertices;
        colors = new Color[Vertices.Length];

        SetElevationMinMax(Vertices);
        SetVertexColor(mesh, Vertices, colors, gradient);
    }

    private void SetElevationMinMax(Vector3[] Verts)
    {
        for (int i = 0; i < Verts.Length; i++)
        {
            Vector3 zero = new Vector3(0, 0, 0);
            float distance = Vector3.Distance(Verts[i], zero);

            Add_ElevationMinMax(distance);
        }
    }

    private void SetVertexColor(Mesh _mesh,Vector3[] Verts,Color[] _colors,Gradient _gradient)
    {
        Vector3 zero = new Vector3(0, 0, 0);

        for (int g = 0; g < Verts.Length; g++)
        {
            float distance = (float) Vector3.Distance(Verts[g], zero);

            float h = Mathf.InverseLerp(Emin, Emax, Vector3.Magnitude(Verts[g]));

            _colors[g] = _gradient.Evaluate(h);
        }
        _mesh.colors = _colors;
    }

    private void Add_ElevationMinMax(float val)
    {
        if(val < Emin)
        {
            Emin = val;
        }
        if(val > Emax)
        {
            Emax = val;
        }
    }

}
