using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparantBackgroundScript : MonoBehaviour
{
    float fadeDuration = 0.5f;
    private Material backgroundMaterial;

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null )
        {
            backgroundMaterial = renderer.material;
        }
    }

    void OnEnable()
    {
        if (backgroundMaterial != null )
        {
            StartCoroutine(FadeInBackground());
        }
    }

    IEnumerator FadeInBackground()
    {
        Color endColor = backgroundMaterial.color;
        Color startColor = new Color(endColor.r, endColor.g, endColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolasi warna secara bertahap
            backgroundMaterial.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);

            yield return null; // Tunggu frame berikutnya
        }

        // Pastikan alpha benar-benar mencapai 0
        backgroundMaterial.color = endColor;
    }
}
