using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get => instance; }

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource sourceSFX, sourceBGM;

    public AudioClip BGM;
    public AudioClip ButtonSFX;

    [SerializeField] VoidEventChannelSO m_handleButtonOnClick;

    public static readonly string BGM_VOL = "BGM";
    public static readonly string SFX_VOL = "SFX";

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
        PlaySound(ButtonSFX);
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

    private void Start()
    {
        ChangeVolume(GetVolumePref(SFX_VOL), SFX_VOL);
        ChangeVolume(GetVolumePref(BGM_VOL), BGM_VOL);
        //mixer.SetFloat(SFX_VOL, );
        //mixer.SetFloat(BGM_VOL, GetVolumePref(SFX_VOL));
    }

    public void PlayBGM()
    {
        Instance.sourceBGM.Play();
    }

    public void ChangeVolume(float value, string target)
    {
        mixer.SetFloat(target, Mathf.Log10(value) * 20);
        SetVolumePref(target, value);
    }

    public static float GetVolumePref(string target) => PlayerPrefs.GetFloat(target, 1);

    public static void SetVolumePref(string target, float value) => PlayerPrefs.SetFloat(target, value);

    public static void PlayButtonSFX() => Instance.sourceSFX.PlayOneShot(Instance.ButtonSFX);

    public static void PlaySound(AudioClip audioClip)
    {
        Instance.sourceSFX.PlayOneShot(audioClip);
    }
}