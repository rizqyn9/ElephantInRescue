using UnityEngine;

public class PlaneFinish : Plane
{
    [SerializeField] GameStateChannelSO m_gameStateChannelSO;
    [SerializeField] SpriteRenderer m_renderer;

    internal override void Start()
    {
        SetPlaneType(PlaneTypeEnum.FINISH);
        base.Start();
    }

    /// <summary>
    /// Do Something TODO
    /// - Win the game
    /// </summary>
    public override void OnElephant()
    {
        LeanTween
            .scale(m_renderer.gameObject, m_renderer.transform.localScale * 4, .35f)
            .setOnStart(() =>
            {
                m_renderer.sortingOrder = 30;
            })
            .setOnComplete(() =>
            {
                m_gameStateChannelSO.RaiseEvent(GameState.FINISH);
            })
            .setEaseInOutBounce();

        LeanTween
            .move(m_renderer.gameObject, Vector2.zero, .35f)
            .setEaseInOutBounce();
    }
}
