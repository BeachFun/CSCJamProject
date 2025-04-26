using UnityEngine;

public class UIService : MonoBehaviour
{
    public UIService()
    {
        print("UIService class is initialized");
    }

    private void Awake()
    {
        print("UI Service is initialized");
    }

    private void Start()
    {
        print("UI Service is Started");
    }

    public void OpenSettings()
    {

    }
}
