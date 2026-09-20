using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    // Update is called once per frame

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
        //DetectAsteroids(float inMaxRange, List < Transform > inAsteroids)
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
                //float playerenemyX = transform.position.x * enemyTransform.position.x;
                //float playerenemyY = transform.position.y * enemyTransform.position.y;
                //EnemyPlayerRatio = (playerenemyX + playerenemyY) * 0.01f;

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
}
