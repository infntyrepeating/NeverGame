using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public float targetAlpha = 0.3f;
    public Animator m_Animation; // Reference to your animation clip
    public Animator m2_Animation; // Reference to your animation clip



    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHovered = false;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

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
        // Start the specified animation on mouse click
        if (m_Animation != null)
        {
            m2_Animation.SetTrigger("FadeIn");
            // Turn the screen white while the animation plays
            StartCoroutine(PlayAnimationAndWait());
        }
    }

    IEnumerator PlayAnimationAndWait()
    {
        // Trigger the animation
        m_Animation.SetTrigger("Play");

        yield return new WaitForSeconds(2.0f);

        // Call the function to change the scene
        ChangeScene();
    }

    // Function to change the scene
    void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
