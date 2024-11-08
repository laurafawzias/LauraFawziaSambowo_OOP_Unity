using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;

    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on player object");
            return;
        }

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        float horizontalInput = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1, 1);
        float verticalInput = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 1);
        moveDirection = new Vector2(horizontalInput, verticalInput);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            moveDirection.Normalize();
            Vector2 targetVelocity = new Vector2(
                moveDirection.x * maxSpeed.x,
                moveDirection.y * maxSpeed.y
            );

            rb.velocity = Vector2.SmoothDamp(
                rb.velocity,
                targetVelocity,
                ref moveVelocity,
                moveFriction.magnitude * Time.fixedDeltaTime
            );
        }
        else
        {
            rb.velocity = Vector2.MoveTowards(
                rb.velocity,
                Vector2.zero,
                stopFriction.magnitude * Time.fixedDeltaTime
            );

            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }

        MoveBound();
    }

    private Vector2 GetFriction()
    {
        return (moveDirection != Vector2.zero) ? moveFriction : stopFriction;
    }

    public bool IsMoving()
    {
        return rb.velocity.sqrMagnitude > 0.01f;
    }

    public void MoveBound()
    {
    if (mainCamera == null) return;

    Vector2 min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

    transform.position = new Vector2(
        Mathf.Clamp(transform.position.x, min.x + (transform.localScale.x / 2), max.x - (transform.localScale.x / 2)),
        Mathf.Clamp(transform.position.y, min.y + (transform.localScale.y / 10), max.y - (transform.localScale.y / 1.5f))
    );
    }
}