using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound; // ���� ������
    [SerializeField] private int lineNumber; // ����� �����, � ������� ��������� ��� ������
    private AudioSource audioSource;

    private static int? activeLine = null; // ����������� ����������: ����� ����� ������ ������

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
            Debug.Log("�� ���� ����� ��� ������� ������ ������!");
            return; // �� ���� ����� ��� ������ ������ � ����������
        }

        PlaySound();
        activeLine = lineNumber; // �������� �����
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
        // ���� ���� �������� �� ����� � ����������� �����
        if (audioSource != null && !audioSource.isPlaying && activeLine == lineNumber)
        {
            activeLine = null;
        }
    }
}
