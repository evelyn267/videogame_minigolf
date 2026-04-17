using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 target;
    private Vector2 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB.position;
        startPosition = pointA.position;
    }

    private void FixedUpdate()
    {
        Vector2 dir = (target - rb.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            target = target == (Vector2)pointA.position
                ? pointB.position
                : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        LevelManager.main.RestartLevel();
    }
    else
    {
        rb.position = pointA.position;
        target = pointB.position;
    }
}
}