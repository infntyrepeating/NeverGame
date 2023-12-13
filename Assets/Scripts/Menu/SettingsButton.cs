using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public float fadeDuration = 0.2f;
    public float targetAlpha = 0.8f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHovered = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovered)
        {
            // Calculate the new alpha value based on time and target alpha
            float t = Mathf.PingPong(Time.time / fadeDuration, 1f);
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(originalColor.a, targetAlpha, t);
            
            // Apply the new color to the sprite renderer
            spriteRenderer.color = newColor;
        }
    }

    void OnMouseEnter()
    {
        isHovered = true;
    }

    void OnMouseExit()
    {
        isHovered = false;
    }

    void OnMouseDown()
    {
        Debug.Log("Settings has been opened! TBH");
    }
}
