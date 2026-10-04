using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private float directionTimer;
    private float directionTimerMax = 3.5f;
    private float oneDirection;

    //private Vector3 enemyVelocity = Vector3.zero;
    private void Update()
    {
        directionTimer += Time.deltaTime;
        if (transform.position.x <= -23)
        {
            oneDirection = 5;
        }
        else if (transform.position.x <= -23 && transform.position.y <= -9)
        {
            oneDirection = 8;
        }
        else if (transform.position.x <= -23 && transform.position.y >= 9)
        {
            oneDirection = 3;
        }
        else if (transform.position.y <= -9)
        {
            oneDirection = 7;
        }
        else if (transform.position.y >= 9)
        {
            oneDirection = 2;
        }
        else if (transform.position.x >= 23)
        {
            oneDirection = 4;
        }
        else if (transform.position.x >= 23 && transform.position.y <= -9)
        {
            oneDirection = 6;
        }
        else if (transform.position.x >= 23 && transform.position.y >= 9)
        {
            oneDirection = 1;
        }
        else if (directionTimer > directionTimerMax)
        {
            oneDirection = Random.Range(1, 9);
            directionTimer = 0;
        }
        enemyMovement();
    }

    private void enemyMovement()
    {
        //enemyVelocity += Time.deltaTime * Vector3.up;
        if (oneDirection == 1)
        {
            transform.position += Time.deltaTime * new Vector3(-1, -1, 0);
        }
        else if (oneDirection == 2) 
        {
            transform.position += Time.deltaTime * Vector3.down;
        }
        else if (oneDirection == 3)
        {
            transform.position += Time.deltaTime * new Vector3(1, -1, 0);
        }
        else if (oneDirection == 4)
        {
            transform.position += Time.deltaTime * Vector3.left;
        }
        else if (oneDirection == 5)
        {
            transform.position += Time.deltaTime * Vector3.right;
        }
        else if (oneDirection == 6)
        {
            transform.position += Time.deltaTime * new Vector3(-1, 1, 0);
        }
        else if (oneDirection == 7)
        {
            transform.position += Time.deltaTime * Vector3.up;
        }
        else if (oneDirection == 8)
        {
            transform.position += Time.deltaTime * new Vector3(1, 1, 0);
        }

    }
}
