using JetBrains.Annotations;
using UnityEngine;

public class SpaceRock : MonoBehaviour
{
    
    private Vector3 directionMovement = Vector3.zero;
    public int currentDirection = 0;
    private int randomDirectionNumber = 0;

    public bool shipBound;
    public bool activeMovement;

    public Vector3 playerCurrentVelocity;

    public Transform playerTransform;
    public Transform pullBombTransform;
    public float spacerockRadius = 1;
    public float spacerockRadiusX;
    public float spacerockRadiusY;
    public float spacerockSpeed = 25;

    private float stepAngle = 0;
    private float maxRockSpeed = 500f;

    private float startingAngleX;
    private float startingAngleY;
    //speed up
    //private float rockAccelerationTime = 6f;
    //private float rockAcceleration;

    public bool pulledByBomb = false;
    public float startingAngle;

    public float startingAngleCheck;

    public Vector3 shipCollision = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //generate random number for direction
        //direction correlates to numpad, 1 = left + down
        int randomDirectionNumber = Random.Range(1, 10);
        currentDirection = randomDirectionNumber;
        shipBound = false;
        activeMovement = false;

        startingAngleX = Mathf.Acos(transform.position.x / spacerockRadius);
        startingAngleY = Mathf.Asin(transform.position.y / spacerockRadius);

        startingAngle = 0f;
        startingAngleCheck = Mathf.Atan2(transform.position.x, transform.position.y);
        Debug.Log("starting Angle Check is " + startingAngleCheck);

        //rockAcceleration = maxRockSpeed / rockAccelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        spaceRockMove();
        

        if (pulledByBomb == true)
        {
            Debug.Log("starting Angle Check is " + startingAngleCheck + "Now starting Angle is " + startingAngle);


            //set starting angle and position for space rocks
            //float startingAngleX = Mathf.Acos(transform.position.x / spacerockRadius);

            //float startingAngleY = Mathf.Asin(transform.position.y / spacerockRadius);

            //make orbit speed increase over time
            spacerockSpeed += Time.deltaTime * 83.33f;
            stepAngle += Time.deltaTime * spacerockSpeed;
            float currentAngle = (stepAngle * Mathf.Deg2Rad) + startingAngle;
            float xPos = Mathf.Cos(currentAngle) * spacerockRadius;
            float yPos = Mathf.Sin(currentAngle) * spacerockRadius;
            Vector3 orbitBomb = new Vector3(xPos + pullBombTransform.position.x, yPos + pullBombTransform.position.y, 0);
            transform.position = orbitBomb;
        }
        else 
        { 
            startingAngleCheck = Mathf.Atan2(transform.position.x, transform.position.y);
        }
        
    }
    public void spaceRockMove() 
    {
        
        if (shipBound == true)
        {
            //transform.position += shipCollision;
            shipCollision += playerCurrentVelocity;
            
            activeMovement = false;
        }
        else if (activeMovement == true)
        {

            transform.position += shipCollision * Time.deltaTime;
            //activeMovement = true;
        }
        else 
        {
            activeMovement = true;
        }
        //Debug.Log("ship Collision" + (shipCollision + playerCurrentVelocity));
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
