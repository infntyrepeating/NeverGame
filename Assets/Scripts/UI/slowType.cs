using System.Collections;
using TMPro;
using UnityEngine;

public class slowType : MonoBehaviour
{
    float typingSpeed = 0.07f;
    float waitSpeed = 2.5f; // Adjust this value as needed

    string Line1 = "I have this closed door in the back of my mind,";
    string Line2 = "That I wish to hide away under lock and key.";
    string Line3 = "Only I can open and close this door, as I wish,";
    string Line4 = "but open only to tuck stuff away,";
    string Line5 = "other than that I keep it closed.";
    
    private TMP_Text textComponent;
    public TMP_Text text2Component;
    public Animator m_Animation;
    public Animator player_Animation;
    public Animator door_Animation;
    public Animator secondText_Animation;
    
    public Canvas myCanvas;
    public Animator myAnimator;

    private void Start()
    {
        myCanvas.enabled = false;

        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        yield return TypeTextSegment(Line1);
        yield return new WaitForSeconds(waitSpeed);

        yield return TypeTextSegment(Line2);
        yield return new WaitForSeconds(waitSpeed);

        yield return TypeTextSegment(Line3);
        yield return new WaitForSeconds(waitSpeed);

        yield return TypeTextSegment(Line4);
        yield return new WaitForSeconds(waitSpeed);

        yield return TypeTextSegment(Line5);
        yield return new WaitForSeconds(waitSpeed);

        m_Animation.SetTrigger("Play");
        yield return new WaitForSeconds(2.0f);

        player_Animation.SetTrigger("Play");
        yield return new WaitForSeconds(7.0f);

        door_Animation.SetTrigger("Play");
        yield return new WaitForSeconds(3.0f);

        secondText_Animation.SetTrigger("Play");
        yield return new WaitForSeconds(0.5f);
        yield return TypeText2Segment("A door...");
        yield return new WaitForSeconds(0.5f);

        player_Animation.SetTrigger("Play2");
        yield return new WaitForSeconds(6.0f);
        EnableCanvas();
    }

    IEnumerator TypeTextSegment(string segment)
    {
        for (int i = 0; i <= segment.Length; i++)
        {
            textComponent.text = segment.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    IEnumerator TypeText2Segment(string segment)
    {
        for (int i = 0; i <= segment.Length; i++)
        {
            text2Component.text = segment.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EnableCanvas()
    {
        // Enable the Canvas
        myCanvas.enabled = true;

        // You can also play animations here when enabling the Canvas
        myAnimator.SetTrigger("Play");
    }
        
}
