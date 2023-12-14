using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneDoor : MonoBehaviour
{
    public Image option1;
    public Image option2;
    public GameObject targetObject;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Sprite door;
    public Canvas myCanvas;
    public AudioClip doorOpens;

    private int selectedOption = 1;
    
    private AudioSource audioSource;

    void Start()
    {
        // Set the alpha of the initial selected option to 1 (fully visible)
        option1.color = new Color(option1.color.r, option1.color.g, option1.color.b, 1f);
        option2.color = new Color(option2.color.r, option2.color.g, option2.color.b, 0f);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (selectedOption != 0) {
        // Check for input to switch between options
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (selectedOption == 1)
            {
                SwitchOption(2);
            } else
            {
                SwitchOption(1);
            }
        }

        // Check for input to select the hovered option
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Perform action based on the selected option (selectedOption variable)
            if (selectedOption == 2) {
                Application.Quit();
            } else {
                // Change the sprite of the targetObject
                targetObject.GetComponent<SpriteRenderer>().sprite = door;
                audioSource.PlayOneShot(doorOpens, 0.2f);
                myCanvas.enabled = false;

                // Start the animations with the trigger "End"
                StartCoroutine(PlayAndLeave());

            }
        }
        }
    }

    IEnumerator PlayAndLeave() {
        yield return new WaitForSeconds(3.0f);
        
        animator1.SetTrigger("End");
        animator3.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        animator2.SetTrigger("End");
                
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene(2);
    }

    void SwitchOption(int newOption)
    {
        // Set the alpha of the non-hovered option to 0
        if (newOption == 1)
        {
            option1.color = new Color(option1.color.r, option1.color.g, option1.color.b, 1f);
            option2.color = new Color(option2.color.r, option2.color.g, option2.color.b, 0f);
        }
        else if (newOption == 2)
        {
            option1.color = new Color(option1.color.r, option1.color.g, option1.color.b, 0f);
            option2.color = new Color(option2.color.r, option2.color.g, option2.color.b, 1f);
        }

        // Update the selected option
        selectedOption = newOption;
    }
}
