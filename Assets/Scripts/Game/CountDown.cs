using System.Collections;
using TMPro;
using UnityEngine;

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

    private void HandleOnGameStateChanged(GameState before, GameState gameState)
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


    public int CountTime { get; private set; }
    IEnumerator ICountDown(int seconds)
    {
        CountTime = seconds;
        while (CountTime > 0)
        {
            yield return new WaitForSeconds(1);
            CountTime--;
            text.text = CountTime.ToString();
        }
        OnTimeOut();
    }

    void OnTimeOut()
    {
        print("Time Out");
        m_gameStateChannelSO.RaiseEvent(GameState.FINISH);
    }

    float RemainingTime() => 1f;

}
