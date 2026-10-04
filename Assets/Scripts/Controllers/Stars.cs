using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float counter;
    private float starTimer;
    private int i;

    //private float drawTimer;

    // Update is called once per frame
    void Update()
    {
        counter = drawingTime / starTransforms.Count;
        starTimer += Time.deltaTime;
        if (starTimer >= drawingTime) 
        { 
            starTimer = 0;
        }
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        if (starTimer >= counter) 
        {
            Debug.DrawLine(starTransforms[0].position, starTransforms[1].position);
        }
        if (starTimer >= counter * 2) 
        {
            Debug.DrawLine(starTransforms[1].position, starTransforms[2].position);
        }
        if (starTimer >= counter * 3)
        {
            Debug.DrawLine(starTransforms[2].position, starTransforms[3].position);
        }
        if (starTimer >= counter * 4)
        {
            Debug.DrawLine(starTransforms[3].position, starTransforms[4].position);
        }
        if (starTimer >= counter * 5)
        {
            Debug.DrawLine(starTransforms[4].position, starTransforms[5].position);
        }
        if (starTimer >= counter * 6)
        {
            Debug.DrawLine(starTransforms[5].position, starTransforms[6].position);
        }
        //for (int i = 0; i < starTransforms.Count; i++) 
        //{

        //  startPosition = starTransforms[0].position;
        // Debug.DrawLine(startPosition);
        //}
    }
}
