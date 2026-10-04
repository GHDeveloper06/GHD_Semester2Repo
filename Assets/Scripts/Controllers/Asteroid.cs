using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void Start()
    {
        //create position for meteorite to move right away
        generateRandomPosition();
    }
    //max float distance
    private float maxFloatDistance = 10;

    //movespeed
    private float moveSpeed = 2f;

    //arrival distance
    private float arrivalDistance = 0.5f;

    private float randomPositionCheck;

    private float randomPositionX;
    private float randomPositionY;
    private float distanceCheck;

    private Vector3 destination = Vector3.zero;

    private float coinFlip;

    private void Update()
    {
        //calculate distance
        distanceCheck = Vector3.Distance(destination, transform.position);
        //Debug.Log(distanceCheck);

        movementDirector();
    }

    private void generateRandomPosition()
    {
        //choose point in random direction
        randomPositionX = Random.Range(0, transform.position.x + 1 + maxFloatDistance);
        //X and Y position should add up to the max possible distance
        randomPositionY = maxFloatDistance - (randomPositionX - transform.position.x);
        //make it random whether or not the positions are negative or positive
        coinFlip = Random.Range(1, 5);
        if (coinFlip == 1)
        {
            randomPositionX *= -1;
        }
        else if (coinFlip == 2)
        {
        }
        else if (coinFlip == 3)
        {
            randomPositionY *= -1;
        }
        else if (coinFlip == 4)
        {
            randomPositionX *= -1;
            randomPositionY *= -1;
        }
        destination.x = randomPositionX;
        destination.y = randomPositionY;
    }

    private void movementDirector()
    {

        //if meteor is far away from generated position move it there
        if (distanceCheck > arrivalDistance)
        {
            if (transform.position.x < randomPositionX)
            {
                transform.position += Time.deltaTime * Vector3.right * moveSpeed;
            }
            if (transform.position.x > randomPositionX)
            {
                transform.position += Time.deltaTime * Vector3.left * moveSpeed;
            }
            if (transform.position.y < randomPositionY)
            {
                transform.position += Time.deltaTime * Vector3.up * moveSpeed;
            }
            if (transform.position.y > randomPositionY)
            {
                transform.position += Time.deltaTime * Vector3.down * moveSpeed;
            }
        }
        // if meteor gets close enough to generated position, generate a new one
        else
        {
            generateRandomPosition();
        }

        // if meteor is close enough generate new position

    }
    //move asteroid in that direction
    // once it gets close enough choose new random direction
}
