using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 0.01f;
    [SerializeField] private float maxRadius = 1.2f;
    [SerializeField] private float fadePauseRadius = 0.2f;
    [SerializeField] private float fadePauseTime = 1f;
    [SerializeField] private float fadePauseSpeedMult = 5f;

    private static readonly int Radius = Shader.PropertyToID("_Radius");
    private static readonly int Center = Shader.PropertyToID("_Center");
    private static readonly int Aspect = Shader.PropertyToID("_Aspect");

    // Cache
    private Canvas _canvas;
    private Camera _cam;

    private void Awake()
    {
        _canvas = fadeImage.canvas;
        _cam = _canvas && _canvas.renderMode == RenderMode.ScreenSpaceCamera
            ? _canvas.worldCamera
            : Camera.main;

        // Ensure unique material instance for this Image
        fadeImage.material = new Material(fadeImage.material);
    }

    private Vector2 GetFadeTargetPos01()
    {
        // 1) Get pixel position (mouse or world target)
        Vector2 pixel = Input.mousePosition;
        if (ScreenFaderTarget.Instance != null)
            pixel = _cam ? (Vector2)_cam.WorldToScreenPoint(ScreenFaderTarget.Instance.transform.position) : pixel;

        // 2) Normalize by the camera's pixelRect (important if you use Camera.rect/letterbox)
        Rect pr = _cam ? _cam.pixelRect : new Rect(0, 0, Screen.width, Screen.height);
        Vector2 uv = new(
            (pixel.x - pr.x) / pr.width,
            (pixel.y - pr.y) / pr.height);

        // 3) Flip Y only if your shader expects (0,0) at top-left
        uv.y = 1f - uv.y;

        // 4) Pass aspect to the shader (width/height of the *camera rect*)
        float aspect = pr.width / pr.height;
        fadeImage.material.SetFloat(Aspect, aspect);

        return uv; // (0..1) in camera viewport space
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
            fadeImage.material.SetVector(Center, GetFadeTargetPos01());
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
            fadeImage.material.SetVector(Center, GetFadeTargetPos01());
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