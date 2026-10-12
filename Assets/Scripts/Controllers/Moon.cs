using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public float moonRadius = 1;
    public float moonSpeed = 1;
    public Transform planetTransform;

    private float stepAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        //transform.position = planetTransform.position * moonRadius;
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(moonRadius, moonSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target) 
    {
        stepAngle += Time.deltaTime * speed;
        float currentAngle = stepAngle * Mathf.Deg2Rad;
        float xPos = Mathf.Cos(currentAngle) * radius;
        float yPos = Mathf.Sin(currentAngle) * radius;

        Vector3 spawnPoint = new Vector3(xPos + target.transform.position.x, yPos + target.transform.position.y, 0);
        Debug.Log(spawnPoint);
        transform.position = spawnPoint;

    }
}
/*
Vector3 direction = target.position - transform.position;
transform.up = direction * Time.deltaTime * speed;
transform.position -= transform.right * Time.deltaTime * speed;
//transform.position = planetTransform.position * moonRadius;
*/