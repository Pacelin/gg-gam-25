using UnityEngine;

public class MotionTornado : MonoBehaviour
{
    [SerializeField] private float radius = 5f;    
    [SerializeField] private float speed = 2f;     

    private float angle = 0f;                      

    void Update()
    {
        
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        
        transform.position = new Vector3(x, transform.position.y, z);
    }
}