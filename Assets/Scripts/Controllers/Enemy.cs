using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    //get reference to player transform
    public Transform playerTransform;
    //get reference to meteor transforms
    public List<Transform> meteorTransforms;

    //create rotation speed
    public float rotationSpeed = 70f;
    private float enemySpeed = 4.6f;

    void Update() 
    {
        targetPoint();
        transform.position += Time.deltaTime * transform.up * enemySpeed;
    }

    public void targetPoint()
    {
        Vector3 directionToTarget = (playerTransform.position - transform.position).normalized;
        float deltaAngle = ComputeShortestAngle(transform.up, directionToTarget);

        //gives 1 or -1
        float rotateDirection = Mathf.Sign(deltaAngle);
        float angleStep = rotationSpeed * Time.deltaTime;

        if (angleStep < Mathf.Abs(deltaAngle))
        {
            transform.Rotate(0, 0, rotateDirection * rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, deltaAngle);
        }

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        float dotProduct = Vector3.Dot(transform.up, directionToTarget);

        if (dotProduct >= 0)
        {
            Debug.Log("In Front");
        }
        else
        {
            Debug.Log("Behind");
        }
    }

    private float ComputeShortestAngle(Vector3 a, Vector3 b)
    {//A is current, B is target
        float angleA = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(b.y, b.x) * Mathf.Rad2Deg;

        return Mathf.DeltaAngle(angleA, angleB);
    }

}
