using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private float _volumeChangeOverTime = .25f;

    [SerializeField]
    private float _waitToGoBackToBaseMusic = 4f;

    [SerializeField]
    private AudioClip _baseClip;
    [SerializeField]
    private AudioClip _chaseClip;
    [SerializeField]
    private AudioClip _combatClip;

    private AudioSource _audioBase;
    private AudioSource _audioChase;
    private AudioSource _audioCombat;

    private bool _raiseBase;
    private bool _raiseChase;
    private bool _raiseCombat;

    private float _timeToGoBackToBase = -1;

    private void Awake()
    {
        _audioBase = gameObject.AddComponent<AudioSource>();
        _audioChase = gameObject.AddComponent<AudioSource>();
        _audioCombat = gameObject.AddComponent<AudioSource>();

        _audioBase.Stop();
        _audioChase.Stop();
        _audioCombat.Stop();

        _audioBase.clip = _baseClip;
        _audioChase.clip = _chaseClip;
        _audioCombat.clip = _combatClip;

        GetComponent<WaveManager>().OnEntitySpawn.AddListener(OnEntitySpawn);
    }

    public void Init()
    {
        _audioBase.Play();
        _audioChase.Play();
        _audioCombat.Play();

        _raiseBase = true;
        _raiseChase = false;
        _raiseCombat = false;

        _audioBase.volume = 1f;
        _audioChase.volume = 0;
        _audioCombat.volume = 0;
    }

    public void PlayBase()
    {
        _raiseBase = true;
        _raiseChase = false;
        _raiseCombat = false;
    }

    public void PlayChase()
    {
        _raiseBase = false;
        _raiseChase = true;
        _raiseCombat = false;

        _timeToGoBackToBase = Time.timeSinceLevelLoad + _waitToGoBackToBaseMusic;
    }

    public void PlayCombat()
    {
        _raiseBase = false;
        _raiseChase = false;
        _raiseCombat = true;

        _timeToGoBackToBase = Time.timeSinceLevelLoad + _waitToGoBackToBaseMusic;
    }

    private void OnEntitySpawn(GameObject entity)
    {
        EnemyHP enemyHp = entity.GetComponent<EnemyHP>();
        if (enemyHp)
        {
            enemyHp.Injured.AddListener(PlayCombat);
        }

        Stamina stamina = entity.GetComponent<Stamina>();
        if (stamina)
        {
            stamina.Injured.AddListener(PlayCombat);
        }

        EnemyAI enemyAI = entity.GetComponent<EnemyAI>();
        if (enemyAI)
        {
            enemyAI.ChasingEnemy.AddListener(PlayChase);
        }
    }

    private void Update()
    {
        _audioBase.volume += CalculateVolumeDelta(_raiseBase, _audioBase.volume);
        _audioChase.volume += CalculateVolumeDelta(_raiseChase, _audioChase.volume);
        _audioCombat.volume += CalculateVolumeDelta(_raiseCombat, _audioCombat.volume);

        if (_timeToGoBackToBase > 0)
        {
            if (Time.timeSinceLevelLoad >= _timeToGoBackToBase)
            {
                _timeToGoBackToBase = -1;
                PlayBase();
            }
        }
    }

    private float CalculateVolumeDelta(bool onRising, float volume)
    {
        float delta = 0f;
        if (onRising)
        {
            if (volume < 1f)
            {
                delta = _volumeChangeOverTime * Time.deltaTime;
            }
        }
        else
        {
            if (volume > 0f)
            {
                delta = - _volumeChangeOverTime * Time.deltaTime;
            }
        }

        float extra = delta + volume - 1f;
        if (extra > 0f) delta -= extra;

        return delta;
    }

}
