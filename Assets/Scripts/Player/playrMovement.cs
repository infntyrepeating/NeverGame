using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 5.0f;

    private Rigidbody2D body;
    private Vector2 lastMovementDirection;

    public Animator m_Animation;
    private SpriteRenderer spriteRenderer;

    public Sprite upChar;
    public Sprite downChar;
    public Sprite rightChar;
    public Sprite leftChar;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement;
        }

        body.velocity = new Vector2(movement.x * baseSpeed, movement.y * baseSpeed);

        UpdateAnimationAndSprite(movement);
    }

    private void UpdateAnimationAndSprite(Vector2 movement)
    {
        if (movement.y > 0)
        {
            m_Animation.SetInteger("walkingState", 3);
            spriteRenderer.sprite = upChar;
        }
        else if (movement.y < 0)
        {
            m_Animation.SetInteger("walkingState", 1);
            spriteRenderer.sprite = downChar;
        }
        else
        {
            if (movement.x < 0)
            {
                m_Animation.SetInteger("walkingState", 2);
                spriteRenderer.sprite = leftChar;
            }
            else if (movement.x > 0)
            {
                m_Animation.SetInteger("walkingState", 4);
                spriteRenderer.sprite = rightChar;
            }
            else
            {
                m_Animation.SetInteger("walkingState", 0);
                // Do not change the sprite when there's no movement
            }
        }
    }

    private void LateUpdate()
    {
        // Set the sprite to the last movement direction in LateUpdate to ensure it overrides the default sprite
        if (body.velocity == Vector2.zero)
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
