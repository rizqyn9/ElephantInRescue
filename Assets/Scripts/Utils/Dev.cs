using UnityEngine;
using UnityEngine.UI;

public class Dev : MonoBehaviour
{
    [SerializeField] GameObject DevUI;
    [SerializeField] Text levelTarget, stageTarget;

    private void OnEnable()
    {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
        Destroy(gameObject);        
#endif
    }

    public void BtnDevTogle()
    {
        DevUI.SetActive(!DevUI.activeInHierarchy);
    }

    public void Load()
    {
        GameManager.LoadGameLevel(GameManager.GetLevelDataByLevelStage(int.Parse(levelTarget.text), int.Parse(stageTarget.text)));
    }
}

