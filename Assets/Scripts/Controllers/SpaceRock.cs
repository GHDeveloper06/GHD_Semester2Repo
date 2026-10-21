using JetBrains.Annotations;
using UnityEngine;

public class SpaceRock : MonoBehaviour
{
    
    private Vector3 directionMovement = Vector3.zero;
    public int currentDirection = 0;
    private int randomDirectionNumber = 0;

    public bool shipBound;

    public Transform playerTransform;
    public Transform pullBombTransform;
    public float spacerockRadius = 1;
    public float spacerockRadiusX;
    public float spacerockRadiusY;
    public float spacerockSpeed = 25;

    private float stepAngle = 0;
    private float maxRockSpeed = 500f;

    //speed up
    //private float rockAccelerationTime = 6f;
    //private float rockAcceleration;

    public bool pulledByBomb = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //generate random number for direction
        //direction correlates to numpad, 1 = left + down
        int randomDirectionNumber = Random.Range(1, 10);
        currentDirection = randomDirectionNumber;
        shipBound = false;

        //rockAcceleration = maxRockSpeed / rockAccelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shipBound);
        /*if (shipBound == false) 
        {
            rockMovement();
        }
        if (shipBound == true)
        {
            OrbitalMotion(spacerockRadius, spacerockSpeed, playerTransform);
        }*/
        if (pulledByBomb == true) 
        {
            //make orbit speed increase over time
            spacerockSpeed += Time.deltaTime * 83.33f;
            stepAngle += Time.deltaTime * spacerockSpeed;
            float currentAngle = stepAngle * Mathf.Deg2Rad;
            float xPos = Mathf.Cos(currentAngle) * spacerockRadius;
            float yPos = Mathf.Sin(currentAngle) * spacerockRadius;
            Vector3 orbitBomb = new Vector3(xPos + pullBombTransform.position.x, yPos + pullBombTransform.position.y, 0);
            transform.position = orbitBomb;
            spacerockRadius -= 1f;
        }
        
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
    public void rockMovement() 
    {
        if (currentDirection == 1)
        {
            transform.position += Time.deltaTime * new Vector3(-1f, -1f, 0f);
        }
        else if (currentDirection == 2) 
        {
            transform.position += Time.deltaTime * Vector3.down;
        }
        else if (currentDirection == 3)
        {
            transform.position += Time.deltaTime * new Vector3(1f, -1f, 0f);
        }
        else if (currentDirection == 4)
        {
            transform.position += Time.deltaTime * Vector3.left;
        }
        else if (currentDirection == 5)
        {
            transform.position += Vector3.zero;
        }
        else if (currentDirection == 6)
        {
            transform.position += Time.deltaTime * Vector3.right;
        }
        else if (currentDirection == 7)
        {
            transform.position += Time.deltaTime * new Vector3(-1f, 1f, 0f);
        }
        else if (currentDirection == 8)
        {
            transform.position += Time.deltaTime * Vector3.up;
        }
        else if (currentDirection == 9)
        {
            transform.position += Time.deltaTime * new Vector3(1f, 1f, 0f);
        }
    }

   /* public void generateStartingDirection()
    {
        //generate random number for direction
        //direction correlates to numpad, 1 = left + down
        int randomDirectionNumber = Random.Range(1, 10);
    }*/
}
