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
    public Vector2 bombOffset = new Vector2(0, -1);
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) 
        {
            spawnBombAtOffset(transform.position);
        }
    }
    //create method to spawn bomb
    void spawnBombAtOffset(Vector2 playerPosition)
    {
        //Instantiate(bombPrefab, playerPosition + new Vector2 (0, -1), Quaternion.identity);
        Instantiate(bombPrefab, playerPosition + bombOffset, Quaternion.identity);
    }
}
