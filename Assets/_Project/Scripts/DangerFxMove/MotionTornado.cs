using UnityEngine;

public class MotionTornado : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float radiusOffset = 0f;

    private Transform centerPoint;
    private float currentAngle = 0f;
    private float actualRadius;

    void Start()
    {
        centerPoint = transform.parent;

        if (centerPoint == null)
        {
            centerPoint = new GameObject("CenterPoint").transform;
            centerPoint.position = transform.position;
            transform.SetParent(centerPoint);
        }

        actualRadius = Vector3.Distance(transform.position, centerPoint.position) + radiusOffset;
    }

    void Update()
    {
        currentAngle += speed * Time.deltaTime;

        float x = Mathf.Cos(currentAngle) * actualRadius;
        float z = Mathf.Sin(currentAngle) * actualRadius;

        transform.localPosition = new Vector3(x, transform.localPosition.y, z);
    }

    public void SetRadius(float newRadius)
    {
        actualRadius = newRadius + radiusOffset;
    }
}

/*{
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
}*/