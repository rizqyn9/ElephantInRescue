using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICMovement
{
    bool CanMove { get; set; }
    void SetCanMove(bool shouldMove);
    void OnStartWalking();
    void OnUpdateTarget();
    void OnReverse();
}

[RequireComponent(typeof(Civilian))]
[System.Serializable]
public class CivilianMovement: MonoBehaviour
{
    [SerializeField][Range(0,3)] internal float Speed;
    [SerializeField] List<Plane> Routes = new List<Plane>();

    public Civilian Civilian { get; set; }
    public ICMovement ICMovement { get; private set; }
    public Coroutine coroutine { get; internal set; }

    public Plane CurrentPlane { get => Civilian.CurrentPlane; }
    bool CanMove { get => ICMovement.CanMove; }
    int RoutesCount { get => Routes.Count; }

    public int CurrentIndex { get; private set; }
    public int NextTarget { get; private set; }

    private void OnEnable()
    {
        Civilian = GetComponent<Civilian>();

        if (!(Civilian is ICMovement)) throw new System.Exception("ICMovement not found");
        ICMovement = Civilian as ICMovement;

        CurrentIndex = 0;
        NextTarget = 1;

        Civilian.SetCurrentPlane(Routes[CurrentIndex]);
        transform.position = CurrentPlane.transform.position;

        ValidateRoutes();
    }

    public void StartMove()
    {
        coroutine = StartCoroutine(IStartMove());
        ICMovement.OnStartWalking();
    }

    public void RecalculateTarget()
    {
        // NEXT
        if (CurrentIndex < NextTarget)
        {
            CurrentIndex++;
            if (NextTarget + 1 >= RoutesCount) // Reserve
                NextTarget--;
            else
                NextTarget++;
        }

        // PREV
        else if (CurrentIndex > NextTarget)
        {
            CurrentIndex--;
            if (NextTarget <= 0) //Reserve
                NextTarget++;
            else
                NextTarget--;
        }
    }

    public void ReverseDirection()
    {
        if (CurrentIndex < NextTarget)
        {
            CurrentIndex = NextTarget;
            NextTarget--;
        }
        else if (CurrentIndex > NextTarget)
        {
            CurrentIndex = NextTarget;
            NextTarget++;
        }
        ICMovement.OnReverse();
    }

    IEnumerator IStartMove()
    {
        ICMovement.SetCanMove(true);

        while (CanMove)
        {
            yield return
                Civilian.StartCoroutine(
                    IMove(Routes[NextTarget].transform,
                        () => {
                            RecalculateTarget();
                        }
                    ));
        }
    }

    IEnumerator IMove(Transform target, System.Action cb)
    {
        UpdateDirection(Civilian.transform, target);
        while ( CanMove && ShouldMoveByDistance(Civilian.transform, target) )
        {
            Civilian.transform.position = MoveTowards(Civilian.transform, target);
            yield return null;
        }
        cb();
    }

    public void Stop()
    {
        ICMovement.SetCanMove(false);   
    }

    Vector2 MoveTowards(Transform from, Transform to) =>
        Vector2.MoveTowards(from.position, to.position, Speed * Time.deltaTime);

    bool ShouldMoveByDistance(Transform from, Transform to) =>
        Vector3.Distance(from.position, to.position) > .05f;

    void UpdateDirection(Transform from, Transform to) =>
        Civilian.SetDirection(Utils.DecideDirection(from.position, to.position));

    void ValidateRoutes() {
        if(RoutesCount <= 1)
            throw new System.Exception("Routes must greater than 1");
    }
}
