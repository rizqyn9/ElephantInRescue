using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] AudioClip m_sfx;

    private void OnEnable()
    {
        SoundManager.PlaySFXLoop(m_sfx);
    }

    private void OnDestroy()
    {
        SoundManager.StopSFXLoop();
    }
}
