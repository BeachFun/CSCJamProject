using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound; // Звук кнопки
    [SerializeField] private int lineNumber; // Номер линии, к которой относится эта кнопка
    private AudioSource audioSource;

    private static int? activeLine = null; // Статическая переменная: какая линия сейчас занята

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnButtonPressed()
    {
        if (activeLine != null && activeLine == lineNumber)
        {
            Debug.Log("На этой линии уже активна другая кнопка!");
            return; // На этой линии уже играет кнопка — пропускаем
        }

        PlaySound();
        activeLine = lineNumber; // Занимаем линию
    }

    private void PlaySound()
    {
        if (buttonSound != null)
        {
            audioSource.clip = buttonSound;
            audioSource.Play();
        }
    }

    private void Update()
    {
        // Если звук проиграл до конца — освобождаем линию
        if (audioSource != null && !audioSource.isPlaying && activeLine == lineNumber)
        {
            activeLine = null;
        }
    }
}
