using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    //make timer for bombs to recharge
    public float timerValue;
    public float timerMax = 5f;
    public Vector2 bombOffset = new Vector2(0, -1);
    void Update()
    {
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
}
