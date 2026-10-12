using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public float moonRadius = 1;
    public float moonSpeed = 1;
    public Transform planetTransform;

    private float stepAngle = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = planetTransform.position * moonRadius;
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(moonRadius, moonSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target) 
    {
        Vector3 direction = target.position - transform.position;
        transform.up = direction * Time.deltaTime * speed;
        transform.position -= transform.right * Time.deltaTime * speed;
        //transform.position = planetTransform.position * moonRadius;

    }
}
