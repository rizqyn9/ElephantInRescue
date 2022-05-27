using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] int countTime = 100;
    [SerializeField] GameStateChannelSO m_gameStateChannelSO;

    private void OnEnable()
    {
        m_gameStateChannelSO.OnEventRaised += HandleOnGameStateChanged;
    }

    private void OnDisable()
    {
        m_gameStateChannelSO.OnEventRaised -= HandleOnGameStateChanged;        
    }

    private void Start()
    {
        countTime = LevelManager.Instance.CountTimeOut;
        text.text = countTime.ToString();
    }

    private void HandleOnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PLAY:
                StartCoroutine(ICountDown(countTime));
                break;
            case GameState.FINISH:
                StopAllCoroutines();
                break;
        }
    }

    IEnumerator ICountDown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            text.text = counter.ToString();
        }
        OnTimeOut();
    }

    void OnTimeOut()
    {
        print("Time Out");
        m_gameStateChannelSO.RaiseEvent(GameState.TIME_OUT);
    }

}
