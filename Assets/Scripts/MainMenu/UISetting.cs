using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [SerializeField] UI_MainMenu m_Ui_MainMenu;
    [SerializeField] Slider m_bgmSlider;
    [SerializeField] Slider m_sfxSlider;
    [SerializeField] GameObject m_preference, m_credit;

    private void OnEnable()
    {
        m_bgmSlider.onValueChanged.AddListener(HandleBgmSliderOnChange);
        m_sfxSlider.onValueChanged.AddListener(HandleSfxSliderOnChange);
    }

    private void Start()
    {
        m_bgmSlider.value = SoundManager.GetVolumePref(SoundManager.BGM_VOL);
        m_sfxSlider.value = SoundManager.GetVolumePref(SoundManager.SFX_VOL);

        m_preference.SetActive(true);
        m_credit.SetActive(false);
    }

    public void On_BackContainer()
    {
        if (m_preference.activeInHierarchy) m_Ui_MainMenu.OnClick_Overlay();
        if (m_credit.activeInHierarchy)
        {
            m_credit.SetActive(false);
            m_preference.SetActive(true);
        }
    }

    public void On_CreditClick()
    {
        if (m_credit.activeInHierarchy) return;
        else
        {
            m_preference.SetActive(false);
            m_credit.SetActive(true);
        }
    }

    void HandleSfxSliderOnChange(float value) => SoundManager.Instance.ChangeVolume(value, SoundManager.SFX_VOL);
    void HandleBgmSliderOnChange(float value) => SoundManager.Instance.ChangeVolume(value, SoundManager.BGM_VOL);
}
