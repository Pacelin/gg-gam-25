using UnityEngine;

public class MotionBurya : MonoBehaviour

{
    [Header("Movement Settings")]
    [SerializeField] private float distance = 5f;  // ������������ ���������� ��������
    [SerializeField] private float speed = 2f;     // �������� ��������
    [SerializeField] private LayerMask collisionMask; // ���� ��� �������� ������������

    private Vector3 startPos;                      // ��������� ������� �������
    private Vector3 direction = new Vector3(1, 0, 1).normalized; // ��������� �����������
    private float currentDistance;                 // ������� ���������� ����������
    private bool movingForward = true;             // ����������� ��������

    void Start()
    {
        startPos = transform.position;
        currentDistance = 0f;
    }

    void Update()
    {
        // ��������� ��������
        float moveStep = speed * Time.deltaTime;

        if (movingForward)
        {
            currentDistance += moveStep;
            if (currentDistance >= distance)
            {
                currentDistance = distance;
                movingForward = false;
            }
        }
        else
        {
            currentDistance -= moveStep;
            if (currentDistance <= 0f)
            {
                currentDistance = 0f;
                movingForward = true;
            }
        }

        // ��������� ����� �������
        Vector3 newPosition = startPos + direction * currentDistance;

        // ��������� �������� ����� ������������
        if (!Physics.Linecast(transform.position, newPosition, collisionMask))
        {
            transform.position = newPosition;
        }
        else
        {
            // ��� ������������ ������ �����������
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // ���������� ��������� ����� ����������� � ��������� XZ
        float randomAngle = Random.Range(0f, 360f);
        direction = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            0f,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad)
        ).normalized;

        // ��������� ��������� ������� ��� ������ �����������
        startPos = transform.position;
        currentDistance = 0f;
        movingForward = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // �������������� �������� ������������ ����� ������
        if (((1 << collision.gameObject.layer) & collisionMask) != 0)
        {
            ChangeDirection();
        }
    }
}












/*{
    [SerializeField] private float distance = 5f;  
    [SerializeField] private float speed = 2f;     

    private Vector3 startPos;                      
    private Vector3 direction = new Vector3(1, 0, 1).normalized; 

    void Start()
    {
        startPos = transform.position; 
    }

    void Update()
    {
        
        float pingPongValue = Mathf.PingPong(Time.time * speed, distance);

        
        transform.position = startPos + direction * pingPongValue;
    }
}*/