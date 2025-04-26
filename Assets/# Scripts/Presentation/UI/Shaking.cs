using UnityEngine;
using System.Collections;

public class ShakeOnStart : MonoBehaviour
{
    public float duration = 0.5f;        // Время тряски
    public float magnitude = 10f;         // Сила тряски в пикселях
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float strength = Mathf.Lerp(magnitude, 0f, elapsed / duration); // Плавное затухание

            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
