using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    [SerializeField] List<Image> m_stars = new List<Image>();
    [SerializeField] List<Image> m_emojis = new List<Image>();
    [SerializeField] List<Image> m_stages = new List<Image>();
    [SerializeField] List<Image> m_condition = new List<Image>();

    [SerializeField] GameObject m_containerGO;
    [SerializeField] Image m_overlayedEffect;

    [SerializeField] GameStateChannelSO m_gameStateChannel;

    [HideInInspector] [SerializeField] PlayerController PlayerController = null;

    void OnEnable()
    {
        m_gameStateChannel.OnEventRaised += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        m_gameStateChannel.OnEventRaised -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.FINISH:
                CalculateStar();
                break;
        }
    }

    private void Start()
    {
        m_overlayedEffect.enabled = false;
        m_containerGO.SetActive(false);
        PlayerController = PlayerController.Instance;
    }

    void CalculateStar()
    {
        CountDown cd = GameObject.FindGameObjectWithTag("CountDown").GetComponent<CountDown>();
        print(cd.CountTime);

        if (PlayerController.IsDead)
        {
            print("My Elepehant die");
        }

        if (cd.CountTime == 0)
        {
            HandleStars(0);
            return;
        }
        else if (cd.CountTime <= 4)
        {
            HandleStars(1);
            return;
        }
        else if (cd.CountTime >= 3)
        {

            return;
        }
    }


    void HandleStars(int starsCount)
    {
        StartCoroutine(Instance(starsCount));
    }

    IEnumerator Instance(int starsCount)
    {
        m_overlayedEffect.enabled = true;
        m_containerGO.SetActive(true);
        LeanTween.moveLocalY(m_containerGO, 0, .4f).setFrom(-500).setEaseOutBounce();
        SetActiveImageFromList(starsCount, m_stars);
        LeanTween.rotateZ(SetActiveImageFromList(starsCount, m_emojis), -10f, 1.5f).setEaseInOutCirc().setLoopPingPong();
        SetActiveImageFromList(0, m_condition);
        yield return 1;
    }

    private GameObject SetActiveImageFromList(int indexTarget, List<Image> images)
    {
        images.ForEach(val => val.gameObject.SetActive(false));
        images[indexTarget].gameObject.SetActive(true);
        return images[indexTarget].gameObject;
    }
}
