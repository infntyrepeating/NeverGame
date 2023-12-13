using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class slowType : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public string fullText;
    
    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
