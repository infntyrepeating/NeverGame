using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static bool Alive = true;
    public float baseSpeed = 5.0f;
    public Tilemap groundTilemap;

    private Rigidbody2D body;
    private Vector2 lastMovementDirection;
    private BoxCollider2D playerCollider;
    private Vector3 nextPosition;

    public Animator m_Animation;
    private SpriteRenderer spriteRenderer;

    public Sprite upChar;
    public Sprite downChar;
    public Sprite rightChar;
    public Sprite leftChar;
    public Sprite upCharS;
    public Sprite downCharS;
    public Sprite rightCharS;
    public Sprite leftCharS;

    public static int playerSize;

    
    public float destroyRadius = 5f;
    public float coneAngle = 30f;
    public LayerMask targetLayer;

    private void Start()
    {
        playerSize = 1;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        m_Animation.SetInteger("playerSize", playerSize);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement;
        }

        // Calculate the next position
        nextPosition = transform.position + new Vector3(movement.x * baseSpeed, movement.y * baseSpeed, 0f);

        // Check if the next position is within the tilemap bounds
        if (IsNextPositionValid(nextPosition))
        {
            body.velocity = new Vector2(movement.x * baseSpeed, movement.y * baseSpeed);
            UpdateAnimationAndSprite(movement);
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    private bool IsNextPositionValid(Vector3 position)
    {
        // Check if the entire bounding box of the player is within the tilemap bounds
        if (groundTilemap == null)
        {
            Debug.LogError("Ground Tilemap not assigned to PlayerMovement script.");
            return false;
        }

        Bounds bounds = playerCollider.bounds;
        BoundsInt boundsInt = new BoundsInt((int)bounds.min.x, (int)bounds.min.y, (int)bounds.min.z, (int)bounds.size.x, (int)bounds.size.y, (int)bounds.size.z);


        for (int x = boundsInt.x; x < boundsInt.x + boundsInt.size.x; x++)
        {
            for (int y = boundsInt.y; y < boundsInt.y + boundsInt.size.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                
                // Check if the cell position is within the bounds of the tilemap
                if (!groundTilemap.HasTile(cellPosition))
                {
                    return false;
                }
            }
        }


        return true;
    }

    private void UpdateAnimationAndSprite(Vector2 movement)
    {
        if (movement.y > 0)
        {
            m_Animation.SetInteger("walkingState", 3);
            if (playerSize == 1)
            {
            spriteRenderer.sprite = upChar;
            } else { spriteRenderer.sprite = upCharS; }
        }
        else if (movement.y < 0)
        {
            m_Animation.SetInteger("walkingState", 1);
            if (playerSize == 1)
            {
            spriteRenderer.sprite = downChar;
            } else { spriteRenderer.sprite = downCharS; }
        }
        else
        {
            if (movement.x < 0)
            {
                m_Animation.SetInteger("walkingState", 2);
            if (playerSize == 1)
            {
                spriteRenderer.sprite = leftChar;
            } else { spriteRenderer.sprite = leftCharS; }
            }
            else if (movement.x > 0)
            {
                m_Animation.SetInteger("walkingState", 4);
            if (playerSize == 1)
            {
                spriteRenderer.sprite = rightChar;
            } else { spriteRenderer.sprite = rightCharS; }
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


private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Book"))
        {
            Toolbar.OffenseLocked = false;
            SceneManager.LoadScene(3);
        }
    }

public void DestroyObjectsInConeArea(Vector3 origin, Vector3 coneDirection)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(origin, destroyRadius, targetLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Vector3 directionToCollider = (hitCollider.transform.position - origin).normalized;
            float angleToCollider = Vector3.Angle(transform.up, directionToCollider);

            if (angleToCollider <= coneAngle / 2f)
            {
                // Destroy the GameObject within the cone and specified layer
                Destroy(hitCollider.gameObject);
            }
        }
    }   
private void OnDrawGizmos()
    {
            // Draw cone visualization in the scene view
            Vector3 forward = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            Vector3 coneDirection1 = Quaternion.AngleAxis(-coneAngle / 2f, Vector3.forward) * forward;
            Vector3 coneDirection2 = Quaternion.AngleAxis(coneAngle / 2f, Vector3.forward) * forward;

            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, coneDirection1 * destroyRadius);
            Gizmos.DrawRay(transform.position, coneDirection2 * destroyRadius);

            Gizmos.DrawLine(transform.position + coneDirection1 * destroyRadius, transform.position + coneDirection2 * destroyRadius);
            Gizmos.DrawLine(transform.position - coneDirection1 * destroyRadius, transform.position - coneDirection2 * destroyRadius);
    }

}

