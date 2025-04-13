using UnityEngine;

public class MotionBurya : MonoBehaviour

{
    [Header("Movement Settings")]
    [SerializeField] private float distance = 5f;  // Максимальное расстояние движения
    [SerializeField] private float speed = 2f;     // Скорость движения
    [SerializeField] private LayerMask collisionMask; // Слой для проверки столкновений

    private Vector3 startPos;                      // Начальная позиция объекта
    private Vector3 direction = new Vector3(1, 0, 1).normalized; // Начальное направление
    private float currentDistance;                 // Текущее пройденное расстояние
    private bool movingForward = true;             // Направление движения

    void Start()
    {
        startPos = transform.position;
        currentDistance = 0f;
    }

    void Update()
    {
        // Вычисляем движение
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

        // Вычисляем новую позицию
        Vector3 newPosition = startPos + direction * currentDistance;

        // Проверяем коллизию перед перемещением
        if (!Physics.Linecast(transform.position, newPosition, collisionMask))
        {
            transform.position = newPosition;
        }
        else
        {
            // При столкновении меняем направление
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // Генерируем случайное новое направление в плоскости XZ
        float randomAngle = Random.Range(0f, 360f);
        direction = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            0f,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad)
        ).normalized;

        // Обновляем стартовую позицию для нового направления
        startPos = transform.position;
        currentDistance = 0f;
        movingForward = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Дополнительная проверка столкновений через физику
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