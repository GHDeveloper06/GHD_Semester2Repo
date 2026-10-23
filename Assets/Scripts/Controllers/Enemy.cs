using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    //get reference to player transform
    public Transform playerTransform;
    //get reference to meteor transforms
    public List<Transform> meteorTransforms;

    [SerializeField] private Vector3 temporaryTarget = Vector3.zero;
    [SerializeField] private Vector3 currentTarget = Vector3.zero;

    public Transform obstacle;

    //create rotation speed
    public float rotationSpeed = 70f;
    public float enemySpeed = 2f;
    // public float enemyMeteorDistance;

    void Update() 
    {
        
        float enemyObstacleDistance = Vector3.Distance(currentTarget, transform.position);
        createPathPoint(enemyObstacleDistance);
        if (enemyObstacleDistance <= 1.5f)
        {
            //rotationSpeed = -70;
            createPathPoint(enemyObstacleDistance);
            //targetPoint();
        }
        else 
        {
            currentTarget = obstacle.transform.position;
            rotationSpeed = 70;
            //targetPoint();
        }
        targetPoint();
        
        transform.position += Time.deltaTime * transform.up * enemySpeed;
        /*for (int i = 0; i < meteorTransforms.Count; i++) 
        {
            //find distance between meteor and enemy
            float enemyMeteorDistance = Vector3.Distance(meteorTransforms[i].transform.position, transform.position);
            Debug.Log("Meteor distance" + enemyMeteorDistance);

            //if meteor is too close to ship, stop pointing at player
            if (enemyMeteorDistance <= 2f)
            {
                Debug.Log("Meteor too close");
            }
            else
            {
                targetPoint();
            }
        }*/


    }
    public void createPathPoint(float calcDistance) 
    {
        //if enemy gets close to object
        //give them a new temporary target
        if (calcDistance <= 1.5f) 
        {
            temporaryTarget = obstacle.transform.position + Vector3.up;
            currentTarget = temporaryTarget;
        }
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
