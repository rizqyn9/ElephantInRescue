using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Throwable : MonoBehaviour
{
    public Animator Animator { get; set; }
    public SpriteRenderer Renderer { get; set; }
    public Civilian Civilian { get; set; }
    public Transform TargetTransform { get; set; }

    private void OnEnable()
    {
        Animator = GetComponentInChildren<Animator>();
        Renderer = Animator.GetComponent<SpriteRenderer>();
        Renderer.enabled = false;
    }

    public void Init(Civilian civilian, Transform target)
    {
        Renderer.enabled = true;
        Civilian = civilian;
        TargetTransform = target;

        Animator.SetFloat("X", Civilian.Direction.x);
        Animator.SetFloat("Y", Civilian.Direction.y);
        LeanTween
            .move(gameObject, target.position, .8f)
            .setOnComplete(() => { Renderer.enabled = false; });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Civilian) return;
        if(collision.TryGetComponent(out PlayerController playerController))
        {
            playerController.OnHitCivilian(Civilian);
        }
    }
}
