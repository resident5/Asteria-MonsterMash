using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public Transform centerPoint;

    public float radius;
    public float speed;

    public float currentAngle;

    void Start()
    {

    }

    void Update()
    {
        currentAngle = Time.time;

        float x = centerPoint.position.x + Mathf.Cos(currentAngle) * radius;
        float z = centerPoint.position.z + Mathf.Sin(currentAngle) * radius;


        transform.position = new Vector3(x, transform.position.y, z);
    }
}
