using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CivilianMovement
{
    Civilian Civilian { get; set; }

    Plane CurrentPlane { get => Civilian.CurrentPlane; }
    List<Plane> Routes { get => Civilian.CivilianConfig.Routes; }
    float Speed { get => Civilian.CivilianConfig.Speed; }
    bool CanMove { get => Civilian.CanMove; }
    int RoutesCount { get => Civilian.CivilianConfig.Routes.Count; }

    public int CurrentIndex { get; private set; }
    public int NextTarget { get; private set; }
    public Coroutine coroutine { get; internal set; }

    public CivilianMovement(Civilian civilian)
    {
        Civilian = civilian;
        CurrentIndex = 0;
        NextTarget = 1;
        ValidateRoutes();
    }

    public void Start()
    {
        coroutine = Civilian.StartCoroutine(IStartMove());
    }

    public void RecalculateTarget()
    {
        // NEXT
        if (CurrentIndex < NextTarget)
        {
            CurrentIndex++;
            if (NextTarget + 1 >= RoutesCount) // Reserve
            {
                NextTarget--;
            }
            else
            {
                NextTarget++;
            }
        }

        // PREV
        else if (CurrentIndex > NextTarget)
        {
            CurrentIndex--;
            if (NextTarget <= 0) //Reserve
            {
                NextTarget++;
            }
            else
            {
                NextTarget--;
            }
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
    }

    IEnumerator IStartMove()
    {
        if (!Civilian) yield return new WaitUntil(() => Civilian != null);

        Civilian.SetCanMove(true);

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
        Civilian.SetCanMove(false);
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
