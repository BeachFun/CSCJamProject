using UnityEngine;
using UnityEngine.Audio;
using UniRx;
using RGames.Core;

public class SettingService : ServiceBase
{
    [Header("Audio Settings")]
    [SerializeField] private ReactiveProperty<float> _volumeMaster = new(1f);
    [SerializeField] private ReactiveProperty<float> _volumeMusic = new();
    [SerializeField] private ReactiveProperty<float> _volumeSFX = new();
    [SerializeField] private ReactiveProperty<float> _volumeVoice = new();
    [Header("References")]
    [SerializeField] private AudioMixer _mixer;

    public ReactiveProperty<float> MasterVolume => _volumeMaster;
    public ReactiveProperty<float> MusicVolume => _volumeMusic;
    public ReactiveProperty<float> SFXVolume => _volumeSFX;
    public ReactiveProperty<float> VoiceVolume => _volumeVoice;


    #region [Методы] Инициализация и запуск

    public override void Startup()
    {
        MasterVolume
            .Subscribe(x => OnVolumeUpdated("MasterVolume", "MasterVolume", x))
            .AddTo(this);

        MusicVolume
            .Subscribe(x => OnVolumeUpdated("MusicVolume", "MusicVolume", x))
            .AddTo(this);

        SFXVolume
            .Subscribe(x => OnVolumeUpdated("SFXVolume", "SFXVolume", x))
            .AddTo(this);

        VoiceVolume
            .Subscribe(x => OnVolumeUpdated("VoiceVolume", "VoiceVolume", x))
            .AddTo(this);

        this.status.Value = ServiceStatus.Started;
    }

    #endregion


    private void Start()
    {
        _volumeMaster.SetValueAndForceNotify(PlayerPrefs.GetFloat("MasterVolume", _volumeMaster.Value));
        _volumeMusic.SetValueAndForceNotify(PlayerPrefs.GetFloat("MusicVolume", _volumeMusic.Value));
        _volumeSFX.SetValueAndForceNotify(PlayerPrefs.GetFloat("SFXVolume", _volumeSFX.Value));

        print("Setting Service is Started");
    }


    private void OnVolumeUpdated(string mixerParameter, string prefsParameter, float value)
    {
        _mixer.SetFloat(mixerParameter, ToLinear(value));

        if (Status.Value == ServiceStatus.Started)
            PlayerPrefs.SetFloat(prefsParameter, value);
    }

    private float ToLinear(float volume) => Mathf.Approximately(volume, 0f) ? -80f : 20f * Mathf.Log10(volume);
}
