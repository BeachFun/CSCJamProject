using UnityEngine;

public class ToolButtonController : MonoBehaviour
{
    [SerializeField] private ZombieLineDetector lineDetector; // ������ �� ������� ���� �����

    public void OnToolButtonPressed()
    {
        if (lineDetector == null)
        {
            Debug.LogWarning("LineDetector �� �������� � ������!");
            return;
        }

        ZombieController randomZombie = lineDetector.GetRandomZombie();
        if (randomZombie != null)
        {
            Destroy(randomZombie.gameObject); // ������ ������� �����
        }
        else
        {
            Debug.Log("�� ����� ��� �����!");
        }
    }
}
