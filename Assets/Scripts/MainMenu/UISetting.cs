using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [SerializeField] Slider m_bgmSlider;
    [SerializeField] Slider m_sfxSlider;

    private void OnEnable()
    {
        m_bgmSlider.onValueChanged.AddListener(HandleBgmSliderOnChange);
        m_sfxSlider.onValueChanged.AddListener(HandleSfxSliderOnChange);
    }

    private void HandleSfxSliderOnChange(float arg0)
    {
    }

    private void HandleBgmSliderOnChange(float arg0)
    {
    }
}
