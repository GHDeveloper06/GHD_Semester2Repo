using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Concept2Testings : MonoBehaviour
{
    public BoxCollider2D bCollider2d;

    [SerializeField] private LayerMask LayersquareLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
    }

    private bool isboxTouching()
    {
       return Physics2D.BoxCast(bCollider2d.bounds.center, bCollider2d.bounds.size, 0f, Vector3.down, 0.1f, LayersquareLayer);
    }
}
