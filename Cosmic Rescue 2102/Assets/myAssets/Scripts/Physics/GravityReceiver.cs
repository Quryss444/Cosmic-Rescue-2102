using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityReceiver : MonoBehaviour
{
    [Header("Target Gravitational Influence")]
    public GravityWell Gravity_Well;
    [Space(10)]
    [Header("RigidBody Orientation Settings")]    
    public bool autoOrient = true;
    public float autoOrientSpeed = 0.5f;
    public float Gravity = 9.81f;

    private Rigidbody rb;
    

    private MathClass math = new MathClass();

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        }
     
    void FixedUpdate()
    {
        ProcessGravity();
    }

    private void ProcessGravity()
    {
        if (Gravity_Well)
        {            
            Vector3 Diff = transform.position - Gravity_Well.transform.position;
            rb.AddForce(-Diff.normalized * Gravity * (rb.mass));

            if(autoOrient) {AutoOrient(-Diff);}
        }
    }

    private float CalculateForce()
    {
        float v = 1f;
        return v;
    }

    private void AutoOrient(Vector3 down)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, autoOrientSpeed * Time.deltaTime);
    }
}
