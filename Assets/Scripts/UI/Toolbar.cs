using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public Transform player;

    public UnityEngine.UI.Image Offense;
    public UnityEngine.UI.Image Defense;
    public UnityEngine.UI.Image Utility;

    public static float OffenseCD = 15f;
    public static float DefenseCD = 15f;
    public static float UtilityCD = 15f;

    bool OffenseOnCooldown = false;
    bool DefenseOnCooldown = false;
    bool UtilityOnCooldown = false;
    RectTransform offenseTransform;
    RectTransform defenseTransform;
    RectTransform utilityTransform;

    int handItem = 0;
    int lastItem = -1;

    // Start color for Utility Image
    Color utilityColor;

    // Start is called before the first frame update
    void Start()
    {
        handItem = 0;
        offenseTransform = Offense.rectTransform;
        defenseTransform = Defense.rectTransform;
        utilityTransform = Utility.rectTransform;

        utilityColor = Utility.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (lastItem != handItem) {
            if (lastItem == 0) {
                offenseTransform.anchoredPosition = new Vector2(offenseTransform.anchoredPosition.x, offenseTransform.anchoredPosition.y - 50f);
                Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);
                offenseTransform.localScale = newScale;
            }
            else if (lastItem == 1) {
                defenseTransform.anchoredPosition = new Vector2(defenseTransform.anchoredPosition.x, defenseTransform.anchoredPosition.y - 50f);
                Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);
                defenseTransform.localScale = newScale;
            }
            else if (lastItem == 2) {
                utilityTransform.anchoredPosition = new Vector2(utilityTransform.anchoredPosition.x, utilityTransform.anchoredPosition.y - 50f);
                Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);
                utilityTransform.localScale = newScale;
            }
            lastItem = handItem;
            if (handItem == 0) {
                offenseTransform.anchoredPosition = new Vector2(offenseTransform.anchoredPosition.x, offenseTransform.anchoredPosition.y + 50f);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                offenseTransform.localScale = newScale;
            }
            else if (handItem == 1) {
                defenseTransform.anchoredPosition = new Vector2(defenseTransform.anchoredPosition.x, defenseTransform.anchoredPosition.y + 50f);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                defenseTransform.localScale = newScale;
            }
            else if (handItem == 2) {
                utilityTransform.anchoredPosition = new Vector2(utilityTransform.anchoredPosition.x, utilityTransform.anchoredPosition.y + 50f);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                utilityTransform.localScale = newScale;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (handItem != 0) { handItem = 0; }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (handItem != 1) { handItem = 1; }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (handItem != 2) { handItem = 2; }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ActivateUtilAbility();
        }
    }


    public void ActivateUtilAbility()
    {
        // Check if the ability is not on cooldown
        if (!UtilityOnCooldown)
        {
            // Change the scale of the GameObject
            StartCoroutine(ChangeScaleAndColorForDuration(Vector3.one * 0.5f, utilityColor, Color.white, 15f));

            // Start the cooldown
            StartCoroutine(StartUtilCooldown(UtilityCD));
        }
        else
        {
            // Ability is on cooldown
            Debug.Log("Ability is on cooldown!");
        }
    }

    // Coroutine to change the scale and color for a duration
    private IEnumerator ChangeScaleAndColorForDuration(Vector3 targetScale, Color startColor, Color endColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate the scale and color over time
            float t = elapsedTime / duration;
            player.transform.localScale = targetScale;
            Utility.color = Color.Lerp(startColor, endColor, t);

            // Wait for the next frame
            yield return null;

            // Update elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Return the scale and color to normal after the duration
        player.transform.localScale = Vector3.one;
        Utility.color = startColor;
    }

    // Coroutine to start the cooldown
    private IEnumerator StartUtilCooldown(float cooldownDuration)
    {
        UtilityOnCooldown = true;

        // Wait for a brief moment before starting the color change
        yield return new WaitForSeconds(0.1f);

        // Start the color change during cooldown
        StartCoroutine(ChangeColorDuringCooldown());

        // Wait for the cooldown duration
        yield return new WaitForSeconds(cooldownDuration);

        // Reset the cooldown
        UtilityOnCooldown = false;
    }

    // Coroutine to gradually change the color during cooldown
    private IEnumerator ChangeColorDuringCooldown()
    {
        float elapsedTime = 0f;

        // Darker color for the cooldown effect
        Color darkerColor = utilityColor * 0.7f;

        while (elapsedTime < UtilityCD)
        {
            // Interpolate the color over time
            float t = elapsedTime / UtilityCD;
            Utility.color = Color.Lerp(darkerColor, utilityColor, t);

            // Wait for the next frame
            yield return null;

            // Update elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the color is set to the start color at the end
        Utility.color = utilityColor;
    }

}
