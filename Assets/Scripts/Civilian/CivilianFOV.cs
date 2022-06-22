using System.Collections;
using UnityEngine;

public class CivilianFOV
{
    public Civilian Civilian { get; private set; }
    public Coroutine Coroutine { get; private set; }
    float Radius { get => Civilian.CivilianConfig.Radius; }
    Vector2 Direction { get => Civilian.Direction; }
    bool CanSeePlayer { get => Civilian.CanSeePlayer; }

    public CivilianFOV (Civilian civilian)
    {
        Civilian = civilian;
    }

    public void Start()
    {
        Coroutine = Civilian.StartCoroutine(FOVCheck());
    }

    IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while (true)
        {
            yield return wait;
            FOV();
        } 
    }

    void FOV()
    {
        RaycastHit2D[] hits =
            Physics2D
            .RaycastAll(Civilian.transform.position, Direction, Radius);

        float playerDistance = float.MaxValue, obstacleDistance = float.MaxValue;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player")) playerDistance = hit.distance;
            else if (hit.collider.CompareTag("Obstacle")) obstacleDistance = hit.distance;
        }

        Civilian.SetCanSeePlayer(playerDistance < obstacleDistance);
    }
}
