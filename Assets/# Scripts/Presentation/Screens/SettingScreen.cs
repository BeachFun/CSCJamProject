using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingScreen : ScreenBase
{
    [Header("Bindings")]
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderDrums;
    [SerializeField] private Slider sliderBass;
    [SerializeField] private Slider sliderGuitar;
    [SerializeField] private Slider sliderSynth;
    [SerializeField] private Slider sliderSFX;

    [Inject] private SettingService service;

    protected override void Awake()
    {
        base.Awake();

        sliderMusic?.onValueChanged.AddListener(OnMusicChanged);
        sliderDrums?.onValueChanged.AddListener(OnDrumsChanged);
        sliderBass?.onValueChanged.AddListener(OnBassChanged);
        sliderGuitar?.onValueChanged.AddListener(OnGuitarChanged);
        sliderSynth?.onValueChanged.AddListener(OnSynthChanged);
        sliderSFX?.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnEnable()
    {
        sliderMusic.value = service.MusicVolume.Value;
        sliderSFX.value = service.SFXVolume.Value;
    }


    private void OnMusicChanged(float volume) => service.MusicVolume.Value = volume;
    private void OnDrumsChanged(float volume) => service.DrumsVolume.Value = volume;
    private void OnBassChanged(float volume) => service.BassVolume.Value = volume;
    private void OnGuitarChanged(float volume) => service.GuitarVolume.Value = volume;
    private void OnSynthChanged(float volume) => service.SynthVolume.Value = volume;
    private void OnSFXChanged(float volume) => service.SFXVolume.Value = volume;
}
