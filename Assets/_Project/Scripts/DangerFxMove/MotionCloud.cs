using UnityEngine;

public class MotionCloud : MonoBehaviour
{
    [Header("Настройки масштабирования")]
    [SerializeField] private float targetScaleMultiplier = 2f; 
    [SerializeField] private float scaleSpeed = 1f;            
    [SerializeField] private float holdDuration = 2f;          

    private Vector3 originalScale;     
    private Vector3 targetScale;       
    private float progress = 0f;       
    private bool isScalingUp = true;   
    private bool isHolding = false;    
    private float holdTimer = 0f;      

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * targetScaleMultiplier;
    }

    void Update()
    {
        if (isScalingUp && !isHolding)
        {
            
            progress += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);

           
            if (progress >= 1f)
            {
                progress = 0f;
                isScalingUp = false;
                isHolding = true;
            }
        }
        else if (isHolding)
        {
            
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdDuration)
            {
                holdTimer = 0f;
                isHolding = false;
            }
        }
        else
        {
            
            progress += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);

            
            if (progress >= 1f)
            {
                progress = 0f;
                isScalingUp = true;
            }
        }
    }
}