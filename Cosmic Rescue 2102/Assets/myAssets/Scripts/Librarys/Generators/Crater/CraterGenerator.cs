using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterGenerator : MonoBehaviour
{
    private Vector3   planetObjectCenter;
    private Vector3[]   modifiedVertices;
    private Vector3[]   originalVertices;
    private int[]              triangles;

    
    private Mesh mesh;
    private int CraterPosition;

    public void GenerateCraters(GameObject _planetSurface, float[] CraterData)
    {
        float _craterPosVertIndex = CraterData[0];
        float _numberOfCraters    = CraterData[1];
        float _craterDepth        = CraterData[3];
        float _strength           = CraterData[4];
        float _craterRadius       = _craterDepth * 1.75f;

        //Get Mesh
        mesh = _planetSurface.GetComponent<MeshFilter>().mesh;
        
        //Get Vertices, triangles and make a mesh copy
        originalVertices = mesh.vertices;
        modifiedVertices = originalVertices;
        triangles = mesh.triangles;

        for (int j = 0; j < _numberOfCraters; j++)//Number of Craters
        {
            CraterPosition = Random.Range(0, 25);
            
            for (int i = 0; i < originalVertices.Length; i++)//Loop thru Vertices
            {
                Vector3 zero = new Vector3(0, 0, 0);

                Vector3 direction = zero - originalVertices[i];

                float distance = Vector3.Distance(originalVertices[i], originalVertices[CraterPosition]);

                if (distance < _craterRadius)//Check if Vertex is in Range of Crater Center
                {
                    modifiedVertices[i] = ModifyVertexPosition2(originalVertices[i], originalVertices[CraterPosition],direction,CraterData);
                }
                else if (distance > _craterRadius)//Check if Vertex is outside of crater center
                {
                    modifiedVertices[i] = originalVertices[i];
                }
               // CraterPosition = 0;
            }
        }
        RecalculateMesh(_planetSurface);
    }

    public void RecalculateMesh(GameObject go)
    {
        mesh.Clear();
        mesh.vertices = modifiedVertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        go.GetComponent<MeshFilter>().sharedMesh = mesh;
        go.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private Vector3 ModifyVertexPosition(Vector3 vertexPosition,Vector3 craterPosition, Vector3 direction, float[] craterData)
    {
        float signValue;
        float _craterDepth = craterData[3];
        float _strength = craterData[4];

        signValue = Mathf.Sin(Mathf.Sin(vertexPosition.x * (2.0f + _strength)) * Mathf.Sin(vertexPosition.y * (2.0f + _strength)) * Mathf.Sin(vertexPosition.z * (2.0f + _strength)));
         
        Vector3 deformation = _craterDepth * -signValue * Vector3.Normalize(vertexPosition);
       
        Vector3 mv4 = vertexPosition + deformation;

        return mv4;
    }

    private Vector3 ModifyVertexPosition2(Vector3 vertexPosition, Vector3 craterPosition, Vector3 direction, float[] craterData)
    {
        float signValue;
        float _craterDepth = craterData[3];
        float _strength = craterData[4];

        float angle = Vector3.Angle(vertexPosition, direction);
        signValue = Mathf.Sin(angle * _strength);

        Vector3 deformation = _craterDepth * -signValue * Vector3.Normalize(vertexPosition - direction);

        Vector3 mv = vertexPosition + deformation;

        return mv;
    }

    private Vector3 ModifyVertexPosition3(Vector3 vertexPosition, Vector3 craterPosition, Vector3 direction, float[] craterData)
    {
        float signValue;
        float _craterDepth = craterData[3];
        float _strength = craterData[4];

        Vector3 zero = new Vector3(0, 0, 0);

        float angle = Vector3.Angle(vertexPosition, direction);

        // Calculate the signed angle to determine the direction of the deformation
        signValue = Mathf.Sign(Vector3.Dot(Vector3.Cross(vertexPosition, direction), Vector3.up));

        // Apply the signed angle and strength to the deformation
        Vector3 deformation = _craterDepth * signValue * Mathf.Sin(angle * _strength) * Vector3.Normalize(vertexPosition);

        Vector3 mv = vertexPosition + deformation;

        return mv;
    }

    private float DistanceToCraterCenter(Vector3 vertexPosition, Vector3 craterPosition, float[] craterData)
    {
        float _craterRadius = craterData[2];

        float distance = Vector3.Distance(vertexPosition, craterPosition);

        float t1 = (distance * 100) / _craterRadius;

        return t1;
    }
}
