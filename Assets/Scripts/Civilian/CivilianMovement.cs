using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CivilianMovement
{
    private Civilian Civilian;
    public List<Plane> Routes { get; set; }
    public float Speed { get; private set; }
    public int CurrentIndex { get; private set; }
    public int NextTarget { get; private set; }
    public int RoutesCount { get; private set; }
    public bool CanMove { get; internal set; }

    public CivilianMovement(Civilian civilian)
    {
        Civilian = civilian;
        Routes = civilian.CivilianConfig.Routes;
        Speed = civilian.CivilianConfig.Speed;
        CurrentIndex = 0;
        NextTarget = 1;
        RoutesCount = civilian.CivilianConfig.Routes.Count;
        CanMove = false;
        ValidateRoutes();
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

    public IEnumerator IStartMove()
    {
        if (!Civilian) yield return new WaitUntil(() => Civilian != null);

        CanMove = true;
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

    public IEnumerator IMove(Transform target, System.Action cb)
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
        SetCanMove(false);
    }

    public void SetCanMove(bool canMove)
    {
        CanMove = canMove;
        Civilian.OnCanMoveChange();
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
