using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileEnemy : MonoBehaviour
{
    public Transform target; // The target GameObject to follow
    public float detectionRadius = 10f; // Radius to detect the target
    public float moveSpeed = 5f; // Speed of the GameObject
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Sprite upChar;
    public Sprite downChar;
    public Sprite rightChar;
    public Sprite leftChar;
    private Vector2 lastMovementDirection;
    public Animator m_Animation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (target == null)
        {
            Debug.LogError("Target not assigned to the script!");
        }
    }

    void Update()
    {
        // Check if the target is within the detection radius
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRadius)
        {
            // Move towards the target using Rigidbody2D velocity
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // Stop moving if the target is outside the detection radius
            rb.velocity = Vector2.zero;
        }
        UpdateAnimationAndSprite(rb.velocity);

        if (rb.velocity != Vector2.zero)
        {
            lastMovementDirection = rb.velocity;
        }
    }

    private void UpdateAnimationAndSprite(Vector2 movement)
    {
        if (movement.x > 0 && movement.y > 0)
        {
            if (movement.x > movement.y)
            {
                m_Animation.SetInteger("walkingState", 4);
                spriteRenderer.sprite = rightChar;
            }
            else
            {
                m_Animation.SetInteger("walkingState", 3);
                spriteRenderer.sprite = upChar;
            }
        } else if (movement.x < 0 && movement.y < 0)
        {
            if (movement.x < movement.y)
            {
                m_Animation.SetInteger("walkingState", 2);
                spriteRenderer.sprite = leftChar;
            }
            else
            {
                m_Animation.SetInteger("walkingState", 1);
                spriteRenderer.sprite = downChar;
            }
        } else if (movement.x < 0 && movement.y > 0)
        {
            if ((-1) * movement.x > movement.y)
            {
                m_Animation.SetInteger("walkingState", 2);
                spriteRenderer.sprite = leftChar;
            }
            else
            {
                m_Animation.SetInteger("walkingState", 3);
                spriteRenderer.sprite = upChar;
            }
        } else if (movement.x > 0 && movement.y < 0)
        {
            if (movement.x > (-1) * movement.y)
            {
                m_Animation.SetInteger("walkingState", 1);
                spriteRenderer.sprite = downChar;
            }
            else
            {
                m_Animation.SetInteger("walkingState", 4);
                spriteRenderer.sprite = rightChar;
                
            }
        }
    }

    private void LateUpdate()
    {
        // Set the sprite to the last movement direction in LateUpdate to ensure it overrides the default sprite
        if (rb.velocity == Vector2.zero)
        {
            spriteRenderer.sprite = GetLastDirectionSprite();
        }
    }

    private Sprite GetLastDirectionSprite()
    {
        if (lastMovementDirection.y > 0)
        {
            return upChar;
        }
        else if (lastMovementDirection.y < 0)
        {
            return downChar;
        }
        else if (lastMovementDirection.x < 0)
        {
            return leftChar;
        }
        else if (lastMovementDirection.x > 0)
        {
            return rightChar;
        }
        else
        {
            // Default sprite when no movement
            return downChar;
        }
    }

}
