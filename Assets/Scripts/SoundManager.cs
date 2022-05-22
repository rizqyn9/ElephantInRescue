using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get => instance; }
    public AudioMixer mixer;

    public AudioClip BGM;
    public AudioClip ButtonSFX;

    public AudioSource soundFX, soundMusic;

    [SerializeField] VoidEventChannelSO m_handleButtonOnClick;

    private void OnEnable()
    {
        m_handleButtonOnClick.OnEventRaised += HandleButtonOnClick;
    }

    private void OnDisable()
    {
        m_handleButtonOnClick.OnEventRaised -= HandleButtonOnClick;
    }

    private void HandleButtonOnClick()
    {

    }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void PlaySound(AudioClip _audioClip)
    {
        Instance.soundFX.PlayOneShot(_audioClip);
    }

    public static void PlayButtonSFX() => Instance.soundFX.PlayOneShot(Instance.ButtonSFX);

    public enum AudioTarget
    {
        BGM,
        SFX
    }

    public void changeVolume(float _value, AudioTarget _target)
    {
        if (_target == AudioTarget.BGM) mixer.SetFloat("MusicVol", Mathf.Log10(_value) * 20);
        if (_target == AudioTarget.SFX) mixer.SetFloat("SFXVol", Mathf.Log10(_value) * 20);
    }

}