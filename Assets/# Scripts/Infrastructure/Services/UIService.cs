using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UIService : MonoBehaviour
{
    [SerializeField] private KeyCanvasPair[] _canvasHashTable;

    [Inject] private DiContainer _container;

    private Dictionary<string, CanvasBase> _canvases = new();


    private void Awake()
    {
        print("UI Service is initialized");
    }

    private void Start()
    {
        print("UI Service is Started");
    }

    public void OpenCanvas(string canvasName)
    {
        var pair = _canvasHashTable.FirstOrDefault(x => x.canvasName == canvasName);

        if (pair is null)
        {
            Debug.LogWarning("Не удалось создать Screen на сцене. Screen с таким именем в хэш-таблице не существует...");
            return;
        }

        CanvasBase canvasPrefab = pair.canvasPrefab;

        if (canvasPrefab is null)
        {
            Debug.LogError("Не удалось создать Screen на сцене. Нет ссылки на префаб canvas с UI!");
            return;
        }

        GameObject canvasGO = Instantiate(canvasPrefab.gameObject);

        CanvasBase canvas = canvasGO.GetComponent<CanvasBase>();
        if (canvas is null)
        {
            Debug.LogWarning("Не удалось создать Screen на сцене. К canvas не прикреплен скрипт для UI...");
            DestroyImmediate(canvasGO);
            return;
        }
        canvas.Show(true);
        canvas.OnVisibleUpdated += OnCanvasVisibleUpdatedHandler; // Смысла в отписке от события нет, ведь Сервис UI обязан быть всегда
        _container.Inject(canvas);

        _canvases.Add(canvasName, canvas);
        print($"Screen {canvasName} открыт");
    }

    public void CloseCanvas(string canvasName)
    {
        if (!_canvases.ContainsKey(canvasName)) return;

        _canvases[canvasName].Show(false);
    }

    private void OnCanvasVisibleUpdatedHandler(CanvasBase canvas, bool isShow)
    {
        if (isShow) return;

        string key = _canvases.FirstOrDefault(kv => kv.Value == canvas).Key;
        if (key != null)
        {
            _canvases.Remove(key);
            Destroy(canvas.gameObject);
        }
    }
}

[System.Serializable]
public class KeyCanvasPair
{
    public string canvasName;
    public CanvasBase canvasPrefab;
}
