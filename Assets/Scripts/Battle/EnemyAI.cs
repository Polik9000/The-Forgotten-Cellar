using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange;
    public float speed;
    public Collider2D patrolArea;
    public LayerMask obstacleMask;
    public float arrivalThreshold = 1;
    public float directionChangeThreshold = 0.05f;
    public float stuckCheckTime = 2f; // čas pro detekci zaseknutí
    public float stuckMovementThreshold = 0.01f;

    private Vector2 startPos;
    private Vector2 patrolTarget;
    private Vector2 lastPosition;
    private float stuckTimer;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        player = FindAnyObjectByType<Player>().gameObject.transform;
        patrolArea = GameObject.Find("PatrolArea" + gameObject.transform.parent.name.PartBefore('.')).GetComponent<PolygonCollider2D>();
        startPos = transform.position;
        lastPosition = transform.position;
        ChooseNewPatrolTarget();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        RotateToDirection();
        CheckIfStuck();
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolTarget) < arrivalThreshold)
        {
            ChooseNewPatrolTarget();
            return;
        }

        Vector2 direction = (patrolTarget - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime + 0.2f, obstacleMask);
        if (hit.collider != null)
        {
            ChooseNewPatrolTarget();
            return;
        }

        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // Otočení na hráče při chase
        RotateTowardsPlayer(direction);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime + 0.2f, obstacleMask);
        if (hit.collider != null)
        {
            ChooseNewPatrolTarget();
            return;
        }

        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void ChooseNewPatrolTarget()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector2 potentialTarget = (Vector2)patrolArea.bounds.center +
                new Vector2(
                    Random.Range(-patrolArea.bounds.extents.x, patrolArea.bounds.extents.x),
                    Random.Range(-patrolArea.bounds.extents.y, patrolArea.bounds.extents.y)
                );

            if (patrolArea.OverlapPoint(potentialTarget))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, potentialTarget - (Vector2)transform.position,
                    Vector2.Distance(transform.position, potentialTarget), obstacleMask);

                if (hit.collider == null)
                {
                    patrolTarget = potentialTarget;
                    stuckTimer = 0f;
                    return;
                }
            }
        }

        patrolTarget = transform.position;
    }

    void RotateToDirection()
    {
        Vector2 movement = (Vector2)transform.position - lastPosition;

        if (movement.magnitude > directionChangeThreshold)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);  // Přidání +90°
        }

        lastPosition = transform.position;
    }

    void RotateTowardsPlayer(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);  // Otočení na hráče
    }

    void CheckIfStuck()
    {
        float movement = ((Vector2)transform.position - lastPosition).magnitude;

        if (movement < stuckMovementThreshold)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckCheckTime)
            {
                ChooseNewPatrolTarget();
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, patrolTarget);
            Gizmos.DrawWireSphere(patrolTarget, 0.1f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        if (patrolArea is PolygonCollider2D poly)
        {
            Gizmos.color = Color.cyan;
            Vector2 offset = patrolArea.offset;
            Vector2 pos = patrolArea.transform.position;

            for (int p = 0; p < poly.pathCount; p++)
            {
                Vector2[] points = poly.GetPath(p);
                for (int i = 0; i < points.Length; i++)
                {
                    Vector2 a = pos + points[i] + offset;
                    Vector2 b = pos + points[(i + 1) % points.Length] + offset;
                    Gizmos.DrawLine(a, b);
                }
            }
        }
    }
}
