using UnityEngine;
using UnityEngine.Audio;
using UniRx;

public class SettingService : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private ReactiveProperty<float> _volumeMusic = new();
    [SerializeField] private ReactiveProperty<float> _volumeDrums = new();
    [SerializeField] private ReactiveProperty<float> _volumeBass = new();
    [SerializeField] private ReactiveProperty<float> _volumeGuitar = new();
    [SerializeField] private ReactiveProperty<float> _volumeSynth = new();
    [Header("SFX Settings")]
    [SerializeField] private ReactiveProperty<float> _volumeSFX = new();
    [Header("References")]
    [SerializeField] private AudioMixer _mixer;

    public ReactiveProperty<float> MusicVolume => _volumeMusic;
    public ReactiveProperty<float> DrumsVolume => _volumeDrums;
    public ReactiveProperty<float> BassVolume => _volumeBass;
    public ReactiveProperty<float> GuitarVolume => _volumeGuitar;
    public ReactiveProperty<float> SynthVolume => _volumeSynth;
    public ReactiveProperty<float> SFXVolume => _volumeSFX;


    private void Awake()
    {
        MusicVolume.Subscribe(x =>
        {
            _mixer.SetFloat("MusicVolume", ToLinear(x));
            PlayerPrefs.SetFloat("MusicVolume", _volumeMusic.Value);
        }).AddTo(this);

        DrumsVolume.Subscribe(x =>
        {
            _mixer.SetFloat("DrumsVolume", ToLinear(x));
            PlayerPrefs.SetFloat("DrumsVolume", _volumeDrums.Value);
        }).AddTo(this);

        BassVolume.Subscribe(x => 
        {
            _mixer.SetFloat("BassVolume", ToLinear(x));
            PlayerPrefs.SetFloat("BassVolume", _volumeBass.Value);
        }).AddTo(this);

        GuitarVolume.Subscribe(x =>
        {
            _mixer.SetFloat("GuitarVolume", ToLinear(x));
            PlayerPrefs.SetFloat("GuitarVolume", _volumeGuitar.Value);
        }).AddTo(this);

        SynthVolume.Subscribe(x =>
        {
            _mixer.SetFloat("SynthVolume", ToLinear(x));
            PlayerPrefs.SetFloat("SynthVolume", _volumeSynth.Value);
        }).AddTo(this);

        SFXVolume.Subscribe(x =>
        {
            _mixer.SetFloat("SFXVolume", ToLinear(x));
            PlayerPrefs.SetFloat("SFXVolume", _volumeSFX.Value);
        }).AddTo(this);

        print("Setting Service is initialized");
    }

    private void Start()
    {
        _volumeMusic.SetValueAndForceNotify(PlayerPrefs.GetFloat("MusicVolume", _volumeMusic.Value));
        _volumeDrums.SetValueAndForceNotify(PlayerPrefs.GetFloat("DrumsVolume", _volumeDrums.Value));
        _volumeBass.SetValueAndForceNotify(PlayerPrefs.GetFloat("BassVolume", _volumeBass.Value));
        _volumeGuitar.SetValueAndForceNotify(PlayerPrefs.GetFloat("GuitarVolume", _volumeGuitar.Value));
        _volumeSynth.SetValueAndForceNotify(PlayerPrefs.GetFloat("SynthVolume", _volumeSynth.Value));
        _volumeSFX.SetValueAndForceNotify(PlayerPrefs.GetFloat("SFXVolume", _volumeSFX.Value));

        print("Setting Service is Started");
    }


    private float ToLinear(float volume) => Mathf.Approximately(volume, 0f) ? -80f : 20f * Mathf.Log10(volume);
}