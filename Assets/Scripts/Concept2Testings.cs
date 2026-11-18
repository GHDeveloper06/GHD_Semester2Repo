using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Concept2Testings : MonoBehaviour
{
    public BoxCollider2D bCollider2d;

    [SerializeField] private LayerMask LayersquareLayer;

    public Vector3 rotation = Vector3.zero;

    [SerializeField]private Rigidbody2D rb2D;

    public float angularVelocity { get; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
        rotation = transform.eulerAngles;
    }
    private float speed = 0;
    // Update is called once per frame
    void Update()
    {
        
        //runs boxCast detecting colliders stuff
        if (isboxTouching() == true)
        {
            print("touching");
        }
        else 
        {
            print("not touching");
        }

        if (Keyboard.current.rKey.isPressed == true) 
        {
            speed = 10f;
        }
        else 
        {
            speed = 1f;
        }
        if (Keyboard.current.rKey.isPressed == false) 
        {
            
            if (rb2D.angularVelocity != 0) 
            {
                rb2D.angularDamping = 5f;
            }
        }
        rotation.z += Time.deltaTime * 10 * speed;

        transform.eulerAngles = rotation;
        
        Debug.Log(rb2D.angularVelocity);
        //GameObject.angularVelocity.

    }

    private bool isboxTouching()
    {
       return Physics2D.BoxCast(bCollider2d.bounds.center, bCollider2d.bounds.size, 0f, Vector3.down, 0.1f, LayersquareLayer);
    }
}
