using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    //get reference to player transform
    public Transform playerTransform;
    //get reference to meteor transforms
    public List<Transform> meteorTransforms;

    public List<Vector3> targetNodes = new(5);
    public List<Vector3> points = new();

    [SerializeField] private Vector3 temporaryTarget = Vector3.zero;
    [SerializeField] private Vector3 currentTarget = Vector3.zero;

    public Transform obstacle;

    public float obstacleRadius = 2;

    //create rotation speed
    public float rotationSpeed = 70f;
    public float enemySpeed = 2f;

    public bool forceTarget = true;
    [SerializeField] private float pauseEnemyMovementTimer = 1;
    // public float enemyMeteorDistance;

    public int targetCounter = 0;

    void Update() 
    {
        obstaclePointCreator(obstacleRadius, 8);
        obstaclePointPursuer(playerTransform.position);
        
        float enemyObstacleDistance = Vector3.Distance(currentTarget, transform.position);
        createPathPoint(enemyObstacleDistance);

        float enemyObjectDistance = Vector3.Distance(obstacle.position, transform.position);

        
        if (forceTarget == true) 
        {
            currentTarget = playerTransform.position;
        }
        if (enemyObjectDistance <= 3.1f)
        {
            
            float distanceCheck = Vector3.Distance(targetNodes[targetCounter], transform.position);
            
   
                if (targetCounter > 4) 
                {
                    //currentTarget = playerTransform.position;
                    forceTarget = true;
                }
                else 
                {
                    forceTarget = false;
                    currentTarget = targetNodes[targetCounter];
                }
                
                if (distanceCheck < 0.5) 
                {
                    targetCounter++;
                }
            
        }


        /*if (enemyObstacleDistance <= 1.5f)
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
        }*/
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
    [SerializeField] private List<float> closestPoints = new();
    [SerializeField] private List<float> closestPointstoEnemy = new();
    public void obstaclePointPursuer(Vector3 transformPlayer) 
    {
        float enemyObjectDistance = Vector3.Distance(obstacle.position, transform.position);
        
        //check distance between enemy and object points 
        if (enemyObjectDistance <= 3.5f && enemyObjectDistance > 3.1f)
        {
            pauseEnemyMovementTimer = 0;

            //List<float> closestPoints = new();
            //check if player is close to points
            for (int i = 0; i < points.Count - 1; i++)
            {
                float playerObjectPointDistance = Vector3.Distance(points[i], transformPlayer);
                if (closestPoints.Count != 8)
                {
                    closestPoints.Add(playerObjectPointDistance);
                }

                float enemyObjectPointDistance = Vector3.Distance(points[i], transform.position);
                if (closestPointstoEnemy.Count != 8) 
                {
                    closestPointstoEnemy.Add(enemyObjectPointDistance);
                }
                if (closestPoints.Count == 8 & closestPointstoEnemy.Count == 8) 
                {
                    //sort both lists from smallest to largest
                    closestPoints.Sort();
                    closestPointstoEnemy.Sort();

                    //make sorted distance correlate with vector points
                    for (int count = 0; count < 8; count++)
                    {
                        playerObjectPointDistance = Vector3.Distance(points[i], transformPlayer);
                        enemyObjectPointDistance = Vector3.Distance(points[i], transform.position);

                        if (closestPointstoEnemy[0] >= enemyObjectPointDistance)
                        {
                            targetNodes[0] = points[i];
                        }
                        for (int c = 0; c < 3; c++) 
                        {
                            if (closestPoints[c] >= playerObjectPointDistance) 
                            {
                                if (c == 0)
                                {
                                    targetNodes[1] = points[i];
                                }
                                if (c == 1) 
                                {
                                    targetNodes[2] = points[i];
                                }
                                if (c == 2)
                                {
                                    targetNodes[3] = points[i];
                                }
                            }
                        }

                    }
                }
            }

        }
        else 
        {
            closestPoints.Clear();
            closestPointstoEnemy.Clear();
        }
    }
    public void obstaclePointCreator(float radius, int circlePoints)
    {
        //determine size of angle to add up to full rotation
        float stepAngle = (2 * Mathf.PI) / circlePoints;

        //temporary list to store x,y of each stepAngle
        //List<Vector3> points = new();

        float currentAngle = stepAngle;

        for (int i = 0; i < circlePoints; i++)
        {

            if (points.Count != circlePoints)
            {
                //scale x,y by radius
                float xPos = Mathf.Cos(currentAngle) * radius;
                float yPos = Mathf.Sin(currentAngle) * radius;

                Vector3 newPoint = new Vector3(xPos, yPos, 0);

                points.Add(newPoint);
                currentAngle += stepAngle;
            }

            //keeps current angle changing

        }

        for (int i = 0; i < circlePoints - 1; i++)
        {
            //check if object is close to enemy
            float distance = Vector3.Distance(obstacle.position, transform.position);

            Vector3 startPoint = obstacle.position + points[i];
            Vector3 endPoint = obstacle.position + points[i + 1];

            if (distance <= radius)
            {
                Debug.DrawLine(startPoint, endPoint, Color.red);
            }
            else
            {
                Debug.DrawLine(startPoint, endPoint, Color.green);
            }


            if (i == circlePoints - 2)
            {
                startPoint = obstacle.position + points[i + 1];
                endPoint = obstacle.position + points[0];

                if (distance <= radius)
                {
                    Debug.DrawLine(startPoint, endPoint, Color.red);
                }
                else
                {
                    Debug.DrawLine(startPoint, endPoint, Color.green);
                }
            }
        }
    }
}
