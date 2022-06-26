
public class CivilianBarier : Civilian, ICMovement
{
    public CivilianMovement CivilianMovement { get; set; }
    public bool CanMove { get; set; }

    internal override void OnEnable()
    {
        base.OnEnable();
        CivilianMovement = GetComponent<CivilianMovement>();

        m_sprite.enabled = false;
    }

    internal override void OnPlay()
    {
        base.OnPlay();
        m_sprite.enabled = true;

        CivilianFOV.Start();
        CivilianMovement.StartMove();
    }

    void ICMovement.OnReverse()
    {
    }

    void ICMovement.OnStartWalking()
    {
    }

    void ICMovement.OnUpdateTarget()
    {
    }

    public void SetCanMove(bool shouldMove)
    {
        CanMove = true;        
    }
}
