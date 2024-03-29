using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    public static int life = 3;
    
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

    private float oldHInput = 0;
    private float oldVInput = 0;

    
    public float destroyRadius = 5f;
    public float coneAngle = 30f;
    public LayerMask targetLayer;

    public GameObject attackPrefab;

    public GameObject Timer;
    
    public Collider2D colliderToIgnore;

    private void Start()
    {
        playerSize = 1;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Vector2 pointToCheck = transform.position;
        pointToCheck.y += -0.6f;
        if (!PlayerMovement.Alive) { SceneManager.LoadScene(5); }

        if (colliderToIgnore.OverlapPoint(pointToCheck))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), colliderToIgnore, true);
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), colliderToIgnore, false);
        }

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
        if (oldHInput != movement.x)
        {
            oldHInput = movement.x;
            if (movement.x != 0 )
            {
                if (movement.x < 0)
                {
                    m_Animation.SetInteger("walkingState", 2);
                    if (playerSize == 1)
                    {
                        spriteRenderer.sprite = leftChar;
                    }
                    else
                    {
                        spriteRenderer.sprite = leftCharS; 
                    }
                }
                else
                {
                    m_Animation.SetInteger("walkingState", 4);
                    if (playerSize == 1)
                    {
                        spriteRenderer.sprite = rightChar;
                    } else { spriteRenderer.sprite = rightCharS; }
                }
            }
        } else if (oldVInput != movement.y)
        {
            oldVInput = movement.y;
            if (movement.y != 0)
            {
                if (movement.y < 0)
                {
                    m_Animation.SetInteger("walkingState", 1);
                    if (playerSize == 1)
                    {
                        spriteRenderer.sprite = downChar;
                    }
                    else
                    {
                        spriteRenderer.sprite = downCharS;
                    }
                }
                else
                {
                    m_Animation.SetInteger("walkingState", 3);
                    if (playerSize == 1)
                    {
                        spriteRenderer.sprite = upChar;
                    } else { spriteRenderer.sprite = upCharS; }
                }
            }
        } if (movement.x == 0 && movement.y == 0)
        {
            {
                m_Animation.SetInteger("walkingState", 0);
                oldVInput = 0;
                oldHInput = 0;
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
            if (playerSize == 1) { return upChar; }
            else { return upCharS; }
        }
        else if (lastMovementDirection.y < 0)
        {
            if (playerSize == 1) { return downChar; }
            else { return downCharS; }
        }
        else if (lastMovementDirection.x < 0)
        {
            if (playerSize == 1) { return leftChar; }
            else { return leftCharS; }
        }
        else if (lastMovementDirection.x > 0)
        {
            if (playerSize == 1) { return rightChar; }
            else { return rightCharS; }
        } else
        {
            return spriteRenderer.sprite;
        }
}


private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Book"))
        {
            if ((SceneManager.GetActiveScene().buildIndex) == 2)
            {
                Toolbar.OffenseLocked = false;
                SceneManager.LoadScene(3);
            } else if ((SceneManager.GetActiveScene().buildIndex) == 3)
            {
                Toolbar.UtilityLocked = false;
                SceneManager.LoadScene(4);
            } else if ((SceneManager.GetActiveScene().buildIndex) == 4)
            {
                SceneManager.LoadScene(6);
            }
        } else if (other.CompareTag("Boss"))
        {
            BossSpawners.boss = true;
            Timer.SetActive(true);
        }
    }

public void DestroyObjectsInConeArea(Vector3 origin, Vector3 attackDirection)
    {       

            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            angle -= 90f;
            // Get the rotation of the rotationReference object
            Quaternion referenceRotation = transform.rotation;

            // Apply the rotation to the attack prefab
            Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward) * referenceRotation;

            // Instantiate attack object with the final rotation
            GameObject attackInstance = Instantiate(attackPrefab, transform.position, finalRotation);

            // Move the attack object in the direction of the mouse
            attackInstance.GetComponent<Rigidbody2D>().velocity = attackDirection.normalized * 8f;

    }   

}

