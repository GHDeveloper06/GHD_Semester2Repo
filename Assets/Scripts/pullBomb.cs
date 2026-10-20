using UnityEngine;

public class pullBomb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float timer;
    private float timerMax = 5;
    void Start()
    {
        Destroy (gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= timerMax) 
        {
            transform.position += Time.deltaTime * Vector3.down;
        }
    }
}
