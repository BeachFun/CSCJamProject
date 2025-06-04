using FriendNote.UI;
using TMPro;
using UnityEngine;

public class LoadingScreen : ScreenBase, IDataUpdatable<float>
{
    [Header("Bindings")]
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TMP_Text textNeedConfirm;


    private void OnEnable() => ShowConfirmLabel(false);
    private void OnDisable() => ShowConfirmLabel(false);


    public void UpdateData(float data)
    {
        progressBar.UpdateData(data);
    }

    /// <summary>
    /// Переключение бара загрузки на надпись о подтверждении
    /// </summary>
    public void ShowConfirmLabel(bool isShow)
    {
        progressBar.gameObject.SetActive(!isShow);
        textNeedConfirm.gameObject.SetActive(isShow);
    }
}
