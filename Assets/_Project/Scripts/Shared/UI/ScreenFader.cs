using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeSpeed = 2f;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator FadeOut()
    {
        float alpha = _fadeImage.color.a;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * _fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float alpha = _fadeImage.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * _fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
    }

    void SetAlpha(float value)
    {
        Color c = _fadeImage.color;
        c.a = Mathf.Clamp01(value);
        _fadeImage.color = c;
    }
}