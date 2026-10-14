using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem.iOS.LowLevel;

public class Meteor : MonoBehaviour
{
    public List<GameObject> asteroidsList = new();

    //get list that tracks meteors

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*for (int i = 0; i < asteroidsList.Count; i++) 
        {
            //asteroidsList[i].GetComponent<SpaceRock>();
            //generateRandomPosition();
        }*/
     //generate random starting vector
    }

    // Update is called once per frame
    void Update()
    {
        //move meteor
        //check distance between meteors
        //if meteors collide with each other add their vectors to each other

        //check if meteors have entered out of bounds
        //teleport meteors back on screen once they have left the screen

    }


}
