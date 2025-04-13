using UnityEngine;

public class MotionGaizer : MonoBehaviour
{
    [SerializeField] private float teleportRadius = 5f; 
    [SerializeField] private float teleportCooldown = 3f;
    [SerializeField] private float shrinkTime = 0.5f; 
    [SerializeField] private float growTime = 0.5f; 

    private Vector3 originalScale; 
    private float timer = 0f;
    private bool isShrinking = false;
    private bool isGrowing = false;

    void Start()
    {
        originalScale = transform.localScale; 
        timer = teleportCooldown; 
    }

    void Update()
    {
        timer += Time.deltaTime;

       
        if (timer >= teleportCooldown && !isShrinking && !isGrowing)
        {
            StartShrinking();
        }

        
        if (isShrinking)
        {
            ShrinkAnimation();
        }

        
        if (isGrowing)
        {
            GrowAnimation();
        }
    }

    void StartShrinking()
    {
        isShrinking = true;
        timer = 0f;
    }

    void ShrinkAnimation()
    {
        
        float progress = timer / shrinkTime;
        transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);

        
        if (progress >= 1f)
        {
            Teleport();
            isShrinking = false;
            isGrowing = true;
            timer = 0f;
        }
    }

    void Teleport()
    {
        
        Vector2 randomCircle = Random.insideUnitCircle * teleportRadius;
        Vector3 newPosition = new Vector3(randomCircle.x, transform.position.y, randomCircle.y);

        
        transform.position = newPosition;
    }

    void GrowAnimation()
    {
       
        float progress = timer / growTime;
        transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, progress);

        
        if (progress >= 1f)
        {
            isGrowing = false;
            timer = 0f;
        }
    }
}