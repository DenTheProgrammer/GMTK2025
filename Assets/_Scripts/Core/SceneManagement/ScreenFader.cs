using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 10f;
    [SerializeField] private float maxRadius = 1800f;
    [SerializeField] private float fadePauseRadius = 200f;
    [SerializeField] private float fadePauseTime = 1f;
    [SerializeField] private float fadePauseSpeedMult = 5f;
    
    private static readonly int Radius = Shader.PropertyToID("_Radius");
    private static readonly int Center = Shader.PropertyToID("_Center");
    
    private Vector2 GetFadeTargetPos()
    {
        Vector2 pos = Input.mousePosition;

        if (ScreenFaderTarget.Instance != null)
        {
            pos = Camera.main.WorldToScreenPoint(ScreenFaderTarget.Instance.transform.position);
        }
        
        pos.y = Screen.height - pos.y;
        return pos;
    }


    public async Awaitable FadeOutAsync()
    {
        fadeImage.gameObject.SetActive(true);
        
        bool stopped = false;
        float stoppedTimer = 0;
        float curRadius = maxRadius;
        float progress = 0;
        float fadeSpeedMult = 1f;
        
        while (curRadius > 0)
        {
            fadeImage.material.SetVector(Center, GetFadeTargetPos());
            if (curRadius < fadePauseRadius && !stopped)
            {
                stoppedTimer += Time.deltaTime;
                if (stoppedTimer < fadePauseTime)
                {
                    await Awaitable.EndOfFrameAsync();
                    continue;
                }
                stopped = true;
                fadeSpeedMult /= fadePauseSpeedMult;
            }
            progress += fadeSpeedMult * fadeSpeed * Time.deltaTime;
            curRadius = Mathf.Lerp(maxRadius, 0f,  progress);
            fadeImage.material.SetFloat(Radius, curRadius);
            await Awaitable.EndOfFrameAsync();
        }
        fadeImage.material.SetVector(Center, Vector4.zero);
    }
    
    public async Awaitable FadeInAsync(float duration)
    {
        bool stopped = false;
        float stoppedTimer = 0;
        float curRadius = 0;
        float progress = 0;
        float fadeSpeedMult = 1f / fadePauseSpeedMult;
        
        while (curRadius < maxRadius)
        {
            fadeImage.material.SetVector(Center, GetFadeTargetPos());
            if (curRadius > fadePauseRadius && !stopped)
            {
                stoppedTimer += Time.deltaTime;
                if (stoppedTimer < fadePauseTime)
                {
                    await Awaitable.EndOfFrameAsync();
                    continue;
                }
                stopped = true;
                fadeSpeedMult *= fadePauseSpeedMult;
            }
            progress += fadeSpeedMult * fadeSpeed * Time.deltaTime;
            curRadius = Mathf.Lerp(0f, maxRadius,  progress);
            fadeImage.material.SetFloat(Radius, curRadius);
            await Awaitable.EndOfFrameAsync();
        }

        fadeImage.gameObject.SetActive(false);
        fadeImage.material.SetVector(Center, Vector4.zero);
    }
}