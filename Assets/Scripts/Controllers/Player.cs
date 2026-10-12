using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    [SerializeField] private List<float> pointCircles = new ();
    
    //make list for vector positions
    [SerializeField] private List<Vector3> radarPoints = new();

    public float maxSpeed = 1f;
    public float accelerationTime;
    private float acceleration;

    private int count = 1;

    //0 is un assigned, 1 is not pressed, 2 is pressed

    private Vector3 velocity = Vector3.zero;

    //public Transform playerTransform;

    void Start() 
    {
        acceleration = maxSpeed / accelerationTime; // a = delta V / delta T
        //enemyRadar(3, 10);
    }

    //public float UniversalTimer;

    //create public Vector2 bomboffset

    //create limit to how many bombs can be held
    public int bombLimit = 3;
    public int bombCount = 3;

    public float bombSpacing = -1f;

    //public float EnemyPlayerRatio;

    //make timer for bombs to recharge
    public float timerValue;
    public float timerMax = 5f;
    public Vector2 bombOffset = new Vector2(0, -1);
    void Update()
    {
        enemyRadar(1, 3);
        //UniversalTimer += Time.deltaTime;
        PlayerController();
        
        DetectAsteroids(2.5f, asteroidTransforms);

        if (bombCount != bombLimit) 
        {//cooldown only starts of bombs need to be recharged
            timerValue += Time.deltaTime;
        }
        if (timerValue >= timerMax) 
        {
            bombCount += 1;
            timerValue = 0;
        }
        //add limit to bombs so they can't be spammed
        if (bombCount > 0) 
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                spawnBombAtOffset(transform.position);
                bombCount -= 1;
            }

            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                bombSpacing = -1f;
                spawnBombTrail(bombSpacing, bombCount);
            }

            if (Keyboard.current.cKey.wasPressedThisFrame) 
            {
                float CornerNumber = Random.Range(1, 5);
                SpawnBombOnRandomCorner(CornerNumber);
                bombCount -= 1;
            }

            if (Keyboard.current.wKey.wasPressedThisFrame) 
            {


                Vector3 playerNormalized = Vector3.Normalize(transform.position);
                Vector3 enemyNormalized = Vector3.Normalize(enemyTransform.position);
                float playerenemyX = playerNormalized.x * enemyNormalized.x;
                float playerenemyY = playerNormalized.y * enemyNormalized.y;
                float EnemyPlayerRatio = (playerenemyX + playerenemyY);
                Debug.Log(EnemyPlayerRatio);

                WarpPlayer(enemyTransform, EnemyPlayerRatio);
            }
        }
        
    }
    #region Bombs
    //create method to spawn bomb
    public void spawnBombAtOffset(Vector2 playerPosition)
    {
        //Instantiate(bombPrefab, playerPosition + new Vector2 (0, -1), Quaternion.identity);
        Instantiate(bombPrefab, playerPosition + bombOffset, Quaternion.identity);
    }

    public void spawnBombTrail(float inBombSpacing, int inNumberOfBombs) 
    {
        //make loop to spawn multiple bombs
        for (int i = 0; i < inNumberOfBombs; i++) 
        {
            Instantiate(bombPrefab, transform.position + new Vector3(0, inBombSpacing), Quaternion.identity);
            bombCount -= 1;
            inBombSpacing -= 1f;
        }
    }

    public void SpawnBombOnRandomCorner(float inDistance)
    {//if statements are organized in a clockwise motion
        if (inDistance == 1)//top left
        {
            Instantiate(bombPrefab, transform.position + new Vector3(-2f, 2f), Quaternion.identity);
        }
        else if (inDistance == 2)//top right 
        {
            Instantiate(bombPrefab, transform.position + new Vector3(2f, 2f), Quaternion.identity);
        }
        else if (inDistance == 3)//bottom right
        {
            Instantiate(bombPrefab, transform.position + new Vector3(2f, -2f), Quaternion.identity);
        }
        else if (inDistance == 4)//bottom left
        {
            Instantiate(bombPrefab, transform.position + new Vector3(-2f, -2f), Quaternion.identity);
        }
    }
    #endregion
    public void WarpPlayer(Transform target, float ratio) 
    {
        transform.position = target.position * ratio;
        
    }

    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids) 
    { // if asteriod in list is within Max Range draw a 2.5 long line from player position to asteroid
        for (int i = 0; i < inAsteroids.Count; i++) 
        {
            float distance = Vector3.Distance(inAsteroids[i].position, transform.position);
            if (distance <= inMaxRange) 
            {
                Debug.DrawLine(transform.position, inAsteroids[i].position, Color.red);
            }
        }
    }

    public void enemyRadar(float radius, int circlePoints) 
    {
        //check how much each point should take up
        int circlePieces = 360 / circlePoints;

        if (count >= circlePoints)
        {
            //do nothing
        }
        else
        //determine what each circlePoint should be
        {
            for (int i = 0; i < circlePoints; i++)
            {
                //for each element in the list, determine their x & y
                float pointValues = circlePieces * (i + 1);
                pointCircles.Add(pointValues);
                count++;
                //convert angle to rads
                float angle = pointCircles[i] * Mathf.Deg2Rad;
                //increase/decrease x & y with radius
                float xPos = Mathf.Cos(angle) * radius;
                float yPos = Mathf.Sin(angle) * radius;

                Vector3 circlePointXY = new Vector3(xPos, yPos, 0);
                radarPoints.Add(circlePointXY);


                Debug.DrawLine(transform.position, transform.position + circlePointXY);
            }
        }
        for (int i = 0; i < circlePoints; i++) 
        {
            Debug.DrawLine(radarPoints[i], transform.position, Color.green);
        }
    }
#region Movement
    public void PlayerController() 
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            //transform.position += Vector3.up * maxSpeed;
            velocity += Time.deltaTime * acceleration * Vector3.up;
            
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            velocity += Time.deltaTime * acceleration * Vector3.right;
            
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            velocity += Time.deltaTime * acceleration * Vector3.left;

        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            velocity += Time.deltaTime * acceleration * Vector3.down;

        }

        if (velocity.magnitude > maxSpeed) 
        {
            velocity = velocity.normalized * maxSpeed;
        }
        //velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += Time.deltaTime * velocity;
        Debug.Log(velocity.magnitude);
    }
    #endregion
}
