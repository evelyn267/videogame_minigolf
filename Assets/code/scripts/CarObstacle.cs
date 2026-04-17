using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    private Transform pointA;
    private Transform pointB;
    private float speed = 3f;
    private CarSpawner spawner;

    private Rigidbody2D rb;
    private Vector2 target;

    public void Setup(Transform a, Transform b, CarSpawner s)
    {
        pointA = a;
        pointB = b;
        spawner = s;
        rb = GetComponent<Rigidbody2D>();
        target = pointB.position;
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        Vector2 dir = (target - rb.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            if (target == (Vector2)pointB.position)
            {
                spawner.CarDestroyed();
                Destroy(gameObject);
            }
            else
            {
                target = pointB.position;
            }
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