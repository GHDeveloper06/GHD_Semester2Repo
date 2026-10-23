using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;

    public GameObject pullBombPrefab;
    public GameObject spawnedPullBombPrefab;
    private float pullBombStepAngle = 0f;
    public pullBomb pullBombScript;

    //public List<GameObject> asteroidTransforms;
    public List<GameObject> asteroidTransforms;
    public SpaceRock asteroidScript;
    private SpaceRock asteroidScriptREAL;

    public GameObject powerUp;
    public float puRadius = 3;
    public int puAmount = 3;

    public float radarRadius = 3f;
    public int radarSideCount = 5;

    public float maxSpeed = 1f;
    public float accelerationTime;
    private float acceleration;

    private int o;
    //0 is un assigned, 1 is not pressed, 2 is pressed

    private Vector3 velocity = Vector3.zero;

    //public Transform playerTransform;

    void Start() 
    {
        acceleration = maxSpeed / accelerationTime; // a = delta V / delta T
        //enemyRadar(3, 10);
        for (int i = 0; i < asteroidTransforms.Count; i++) 
        {
            asteroidScriptREAL = asteroidTransforms[i].GetComponent<SpaceRock>();
        }
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
        
        enemyRadar(radarRadius, radarSideCount);
        //UniversalTimer += Time.deltaTime;
        PlayerController();
        asteroidScriptREAL.playerCurrentVelocity = velocity;

        DetectAsteroids(2.5f, asteroidTransforms);
        if (Keyboard.current.uKey.wasPressedThisFrame) 
        {
            SpawnPowerUps(puRadius, puAmount);
        }
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

            PullBombMethod();
        }
        
    }
    #region Bombs
    //create method to spawn bomb
    public void spawnBombAtOffset(Vector2 playerPosition)
    {
        //Instantiate(bombPrefab, playerPosition + new Vector2 (0, -1), Quaternion.identity);
        Instantiate(bombPrefab, playerPosition + bombOffset, Quaternion.identity);
    }

    public void PullBombMethod()
    {
        //instantiate pull bomb
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            spawnedPullBombPrefab = Instantiate(pullBombPrefab, transform.position, transform.rotation);
            pullBombScript = spawnedPullBombPrefab.GetComponent<pullBomb>();

        }
        
        if (pullBombScript.timer >= 5f) 
        {
            Debug.Log("pull bomb activate");
            //check distance between bomb and asteroids
            for (int i = 0; i < asteroidTransforms.Count; i++)
            {
                //float distance = Vector3.Distance(inAsteroids[i].position, transform.position);
                float pullCount = 0f;
                float pullDistance = Vector3.Distance(asteroidTransforms[i].transform.position, spawnedPullBombPrefab.transform.position);
                if (pullDistance <= 15f)
                {
                    pullCount += 1f * Time.deltaTime;
                    SpaceRock asteroidScriptREAL = asteroidTransforms[i].GetComponent<SpaceRock>();
                    asteroidScriptREAL.pullBombTransform = spawnedPullBombPrefab.GetComponent<Transform>();
                    asteroidScriptREAL.pulledByBomb = true;
                    if (asteroidScriptREAL.pulledByBomb == true) 
                    {
                        //asteroidScriptREAL.playerTransform = transform;
                        asteroidScriptREAL.spacerockRadiusX = asteroidTransforms[i].transform.position.x;
                        asteroidScriptREAL.spacerockRadiusY = asteroidTransforms[i].transform.position.y;
                        asteroidScriptREAL.spacerockRadius = pullDistance - pullCount;
                        asteroidScriptREAL.currentDirection = 5;
                    }
                    if (asteroidScriptREAL.startingAngle == 0) 
                    {
                        asteroidScriptREAL.startingAngle = asteroidScriptREAL.startingAngleCheck;
                    }
                    
                }
            }
        }
        
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
    public void SpawnPowerUps(float radius, int numberOfPowerups) 
    {
        //determine size of angle to add up to full rotation
        float stepAngle = (2 * Mathf.PI) / (numberOfPowerups);

        float currentAngle = stepAngle;

        for (int i = 0; i < numberOfPowerups; i++) 
        {
            //scale x,y by radius
            float xPos = Mathf.Cos(currentAngle) * radius;
            float yPos = Mathf.Sin(currentAngle) * radius;

            Vector3 spawnPoint = new Vector3(xPos + transform.position.x, yPos + transform.position.y, 0);
            Instantiate(powerUp, spawnPoint, Quaternion.identity);
            currentAngle += stepAngle;
        }
    }
    #region Radar
    public void DetectAsteroids(float inMaxRange, List<GameObject> inAsteroids) 
    { // if asteriod in list is within Max Range draw a 2.5 long line from player position to asteroid
        for (int i = 0; i < inAsteroids.Count; i++) 
        {
            //float distance = Vector3.Distance(inAsteroids[i].position, transform.position);
            float distance = Vector3.Distance(inAsteroids[i].transform.position, transform.position);
            if (distance <= inMaxRange) 
            {
                Debug.DrawLine(transform.position, inAsteroids[i].transform.position, Color.red);
            }
            if (distance < 0.5f)
            {
                asteroidScriptREAL = inAsteroids[i].GetComponent<SpaceRock>();
                asteroidScriptREAL.shipBound = true;
                asteroidScriptREAL.shipCollision += Time.deltaTime * velocity;
                asteroidScriptREAL.transform.position += Time.deltaTime * velocity;

                //asteroidScriptREAL.shipCollision += Time.deltaTime * velocity;
                //asteroidScriptREAL.transform.position += Time.deltaTime * velocity;
                if (distance <= 0.4f) 
                {
                    asteroidScriptREAL.shipCollision += asteroidScriptREAL.playerCurrentVelocity * Time.deltaTime;
                }
            }
            else 
            {
                asteroidScriptREAL.shipBound = false;
            }
        }
    }

    public void enemyRadar(float radius, int circlePoints) 
    {
        //determine size of angle to add up to full rotation
        float stepAngle = (2 * Mathf.PI) / circlePoints;

        //temporary list to store x,y of each stepAngle
        List<Vector3> points = new();

        float currentAngle = stepAngle;

        for (int i = 0; i < circlePoints; i++) 
        {
            //scale x,y by radius
            float xPos = Mathf.Cos(currentAngle) * radius;
            float yPos = Mathf.Sin(currentAngle) * radius;

            Vector3 newPoint = new Vector3(xPos, yPos, 0);
            points.Add(newPoint);

            //keeps current angle changing
            currentAngle += stepAngle;
        }

        for (int i = 0; i < circlePoints - 1; i++) 
        {
            //check if enemy is close to player
            float distance = Vector3.Distance(enemyTransform.position, transform.position);

            Vector3 startPoint = transform.position + points[i];
            Vector3 endPoint = transform.position + points[i + 1];

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
                startPoint = transform.position + points[i + 1];
                endPoint = transform.position + points[0];

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
    #endregion
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
        //Debug.Log("velocity" + velocity);
    }
    #endregion
}
