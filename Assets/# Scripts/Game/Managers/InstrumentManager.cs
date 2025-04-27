using UnityEngine;
using UniRx;
using Zenject;
using RGames.Core;
using System.Linq;
using Cysharp.Threading.Tasks;

public class InstrumentManager : MonoBehaviour, IManager
{
    [Header("Settings - main")]
    [SerializeField] private float _intervalSpawn;
    [SerializeField] private float _ins1Cooldown;
    [SerializeField] private float _ins2Cooldown;
    [SerializeField] private float _ins3Cooldown;


    //[SerializeField] private float _intervalAcceleration;
    //[SerializeField, Range(0.0f, 1.0f)] private float _stepAccelerationTime;
    //[SerializeField] private float _minSpawnInternval;


    [Header("Binding")]
    [SerializeField] private SpriteRenderer[] _roadsInstrImage;
    [SerializeField] private Transform[] _soundSpawnPoints;

    [Header("References")]
    [SerializeField] private Sprite _bassGuitarSprite;
    [SerializeField] private Sprite _lectroGuitarSprite;
    [SerializeField] private Sprite _synthezatorSprite;
    [SerializeField] private GameObject _instrument1;
    [SerializeField] private GameObject _instrument2;
    [SerializeField] private GameObject _instrument3;

    [Inject] private GameManager _gameManager;


    //private bool m_accelerationIsOn = true;
    private float m_lastElapsedTime;
    private float m_timerSpawn;
    private bool m_instument1IsOn;
    private bool m_instument2IsOn;
    private bool m_instument3IsOn;

    //private float m_timerAcceleration;

    private InstrumentEnum[] _instruments = new InstrumentEnum[3];


    public ManagerStatus Status { get; private set; }


    private void Awake()
    {
        Status = ManagerStatus.Initializing;

        _gameManager.CurrentGameState.Subscribe(OnGameStateChangedHandler);
        _gameManager.ElapsedTime.Subscribe(OnTimeUpdateHandler);

        if (_instrument1 is null) m_instument1IsOn = false;
        if (_instrument2 is null) m_instument2IsOn = false;
        if (_instrument3 is null) m_instument3IsOn = false;

        print("Instrument Manager is initialized");
    }

    private void Start()
    {
        Status = ManagerStatus.Started;

        print("Instrument Manager is Started");
    }


    private void OnTimeUpdateHandler(float elapsedTime)
    {
        if (Status != ManagerStatus.Started) return;

        float interval = (elapsedTime - m_lastElapsedTime);

        m_timerSpawn += interval;

        // Проигрывание звука, если время спавна пришло
        if (m_timerSpawn >= _intervalSpawn)
        {
            m_timerSpawn = 0;

            int index;

            if (m_instument1IsOn)
            {
                index = _instruments.ToList().IndexOf(InstrumentEnum.BassGuitar);

                if (index != -1)
                {
                    Instantiate(_instrument1, _soundSpawnPoints[index]);
                    Instument1Coldown();
                }
            }

            if (m_instument2IsOn)
            {
                index = _instruments.ToList().IndexOf(InstrumentEnum.ElectroGuitar);

                if (index != -1)
                {
                    Instantiate(_instrument2, _soundSpawnPoints[index]);
                    Instument2Coldown();
                }
            }

            if (m_instument3IsOn)
            {
                index = _instruments.ToList().IndexOf(InstrumentEnum.Synthezator);

                if (index != -1)
                {
                    Instantiate(_instrument3, _soundSpawnPoints[index]);
                    Instument3Coldown();
                }
            }
        }

        m_lastElapsedTime = elapsedTime;
    }

    public void ChangeInstrument(int roadIndex, InstrumentEnum value)
    {
        // Если на этой дорожке интрумент есть и это None
        if (_instruments[roadIndex] == value 
            || _instruments[roadIndex] == InstrumentEnum.None) return;

        // Случай когда инстумент был на другой дорожке
        int index = _instruments.ToList().IndexOf(value);
        if (index != -1)
        {
            _roadsInstrImage[index].sprite = null;
            _instruments[index] = InstrumentEnum.None;
        }

        _instruments[roadIndex] = value;
        if (_roadsInstrImage[roadIndex] is not null)
            _roadsInstrImage[roadIndex].sprite = GetInstrumentSprite(ref value);

        OnInstrumentChangedHandler();
    }


    private void OnGameStateChangedHandler(GameState state)
    {
        if (state == GameState.Played)
        {
            Status = ManagerStatus.Started;
        }
        else
        {
            Status = ManagerStatus.Suspended;
        }
    }

    private void OnInstrumentChangedHandler()
    {

    }

    private Sprite GetInstrumentSprite(ref InstrumentEnum value)
    {
        return value switch
        {
            InstrumentEnum.BassGuitar => _bassGuitarSprite,
            InstrumentEnum.ElectroGuitar => _lectroGuitarSprite,
            InstrumentEnum.Synthezator => _synthezatorSprite,
            _ => null
        };
    }

    private async void Instument1Coldown()
    {
        m_instument1IsOn = false;
        await UniTask.Delay((int)_ins1Cooldown);
        m_instument1IsOn = true;
    }

    private async void Instument2Coldown()
    {
        m_instument2IsOn = false;
        await UniTask.Delay((int)_ins2Cooldown);
        m_instument2IsOn = true;
    }

    private async void Instument3Coldown()
    {
        m_instument3IsOn = false;
        await UniTask.Delay((int)_ins3Cooldown);
        m_instument3IsOn = true;
    }
}

public enum InstrumentEnum
{
    None,
    BassGuitar,
    ElectroGuitar,
    Synthezator
}