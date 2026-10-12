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
        //increase step angle each frame
        stepAngle += Time.deltaTime * speed;
        //convert step angle from degrees to radians
        float currentAngle = stepAngle * Mathf.Deg2Rad;
        //get X and Y position for angle
        float xPos = Mathf.Cos(currentAngle) * radius;
        float yPos = Mathf.Sin(currentAngle) * radius;
        //create vector with X & Y angle positions
        Vector3 orbiter = new Vector3(xPos + target.transform.position.x, yPos + target.transform.position.y, 0);
        
        //set the moon's transform to unit circle position
        transform.position = orbiter;

    }
}
/*
Vector3 direction = target.position - transform.position;
transform.up = direction * Time.deltaTime * speed;
transform.position -= transform.right * Time.deltaTime * speed;
//transform.position = planetTransform.position * moonRadius;
*/