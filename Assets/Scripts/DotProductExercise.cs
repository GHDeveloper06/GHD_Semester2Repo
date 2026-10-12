using UnityEngine;
using UnityEngine.InputSystem;

public class DotProductExercise : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;

    private void Update() 
    {
        Vector3 redVector = ComputerVectorFromAngle(redAngle);
        Vector3 blueVector = ComputerVectorFromAngle(blueAngle);

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);


        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            float dotProduct = ComputeDotProduct(redVector, blueVector);

            if (dotProduct < float.Epsilon) 
            {
                dotProduct = 0;
            }
            Debug.Log(dotProduct);

        }
    }

    private Vector3 ComputerVectorFromAngle(float angle) 
    {
        //convert degrees to radians
        float angleInRads = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos (angleInRads);
        float y = Mathf.Sin(angleInRads);

        return new Vector3(x, y, 0);
    }

    private float ComputeDotProduct(Vector3 a, Vector3 b) 
    {
        float dot = a.x * b.x + a.y * b.y + a.z * b.z;
        return dot;
    }
   // Vector3 redVector = new Vector3(1, 0);
   // Vector3 blueVector = new Vector3(0, 1);
   
}
