using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [SerializeField] UI_MainMenu m_Ui_MainMenu;
    [SerializeField] Slider m_bgmSlider;
    [SerializeField] Slider m_sfxSlider;
    [SerializeField] GameObject m_preference, m_credit;
    [SerializeField] UIDialog m_dialog;
    [SerializeField] bool m_isMainMenu;

    private void OnEnable()
    {
        m_bgmSlider.onValueChanged.AddListener(HandleBgmSliderOnChange);
        m_sfxSlider.onValueChanged.AddListener(HandleSfxSliderOnChange);
        m_dialog = GetComponent<UIDialog>();
    }

    private void Start()
    {
        m_bgmSlider.value = SoundManager.GetVolumePref(SoundManager.BGM_VOL);
        m_sfxSlider.value = SoundManager.GetVolumePref(SoundManager.SFX_VOL);

        if(m_isMainMenu)
        {
            m_preference.SetActive(true);
            m_credit.SetActive(false);
        }
    }

    public void On_BackContainer()
    {
        if (!m_isMainMenu) return;
        if (m_preference.activeInHierarchy) m_dialog.OnClose();
        if (m_credit.activeInHierarchy)
        {
            m_credit.SetActive(false);
            m_preference.SetActive(true);
        }
    }

    public void On_CreditClick()
    {
        if (!m_isMainMenu) return;
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
