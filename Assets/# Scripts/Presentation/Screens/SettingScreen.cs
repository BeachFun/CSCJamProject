using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingScreen : ScreenBase
{
    [Header("Bindings")]
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    [Inject] private SettingService service;

    protected override void Awake()
    {
        base.Awake();

        sliderMusic?.onValueChanged.AddListener(OnMusicChanged);
        sliderSFX?.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnEnable()
    {
        sliderMusic.value = service.MusicVolume.Value;
        sliderSFX.value = service.SFXVolume.Value;
    }


    private void OnMusicChanged(float volume) => service.MusicVolume.Value = volume;
    private void OnSFXChanged(float volume) => service.SFXVolume.Value = volume;
}
