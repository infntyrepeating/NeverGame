using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public GameObject Barrier;
    public GameObject playerLoc;

    public UnityEngine.UI.Image Offense;
    public UnityEngine.UI.Image Defense;
    public UnityEngine.UI.Image Utility;

    public static bool UtilityLocked = true;
    public static bool DefenseLocked = false;
    public static bool OffenseLocked = false;

    public static float OffenseCD = 5f;
    public static float DefenseCD = 10f;
    public static float UtilityCD = 30f;

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
    Color defenseColor;
    Color offenseColor;

    public Sprite utilOn;
    public Sprite utilOff;
    public Sprite defOn;
    public Sprite defOff;
    public Sprite offOn;
    public Sprite offOff;

    public Sprite lockOn;
    public Sprite lockOff;

    public PlayerMovement destroyer;

    // Start is called before the first frame update
    void Start()
    {
        handItem = 0;
        offenseTransform = Offense.rectTransform;
        defenseTransform = Defense.rectTransform;
        utilityTransform = Utility.rectTransform;

        utilityColor = Utility.color;
        defenseColor = Defense.color;
        offenseColor = Offense.color;

        Utility.color = Color.white;
        Defense.color = Color.white;
        Offense.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

        if (lastItem != handItem) {
            if (lastItem == 0) {
                offenseTransform.anchoredPosition = new Vector2(offenseTransform.anchoredPosition.x, offenseTransform.anchoredPosition.y - 20f);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                offenseTransform.localScale = newScale;
                if (OffenseLocked) { Offense.sprite = lockOff; } else
                {
                Offense.sprite = offOff;
                }
            }
            else if (lastItem == 1) {
                defenseTransform.anchoredPosition = new Vector2(defenseTransform.anchoredPosition.x, defenseTransform.anchoredPosition.y - 20f);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                defenseTransform.localScale = newScale;
                if (DefenseLocked) { Defense.sprite = lockOff; } else
                {
                Defense.sprite = defOff;
                }
            }
            else if (lastItem == 2) {
                utilityTransform.anchoredPosition = new Vector2(utilityTransform.anchoredPosition.x, utilityTransform.anchoredPosition.y - 20f);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                utilityTransform.localScale = newScale;
                if (UtilityLocked) { Utility.sprite = lockOff; } else
                {
                Utility.sprite = utilOff;
                }
            }
            lastItem = handItem;
            if (handItem == 0) {
                offenseTransform.anchoredPosition = new Vector2(offenseTransform.anchoredPosition.x, offenseTransform.anchoredPosition.y + 20f);
                Vector3 newScale = new Vector3(1.2f, 1.2f, 1.2f);
                offenseTransform.localScale = newScale;
                if (OffenseLocked) { Offense.sprite = lockOn; } else
                {
                Offense.sprite = offOn;
                }
            }
            else if (handItem == 1) {
                defenseTransform.anchoredPosition = new Vector2(defenseTransform.anchoredPosition.x, defenseTransform.anchoredPosition.y + 20f);
                Vector3 newScale = new Vector3(1.2f, 1.2f, 1.2f);
                defenseTransform.localScale = newScale;
                if (DefenseLocked) { Defense.sprite = lockOn; } else
                {
                Defense.sprite = defOn;
                }
            }
            else if (handItem == 2) {
                utilityTransform.anchoredPosition = new Vector2(utilityTransform.anchoredPosition.x, utilityTransform.anchoredPosition.y + 20f);
                Vector3 newScale = new Vector3(1.2f, 1.2f, 1.2f);
                utilityTransform.localScale = newScale;
                if (UtilityLocked) { Utility.sprite = lockOn; } else
                {
                Utility.sprite = utilOn;
                }
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
            if (handItem == 2 && !UtilityLocked)
            {
            ActivateUtilAbility();
            }
            else if (handItem == 1 && !DefenseLocked)
            {
            ActivateDefAbility();
            }
            else if (handItem == 0 && !OffenseLocked)
            {
            ActivateOffAbility();
            }
        }
    }


// DEFENSE

    public void ActivateDefAbility()
    {
        // Check if the ability is not on cooldown
        if (!DefenseOnCooldown)
        {
            // Change the scale of the GameObject
            Defense.color = defenseColor;

            // Start the cooldown
            StartCoroutine(StartDefCooldown(DefenseCD));
        }
        else
        {
            // Ability is on cooldown
            Debug.Log("Ability is on cooldown!");
        }
    }

    
    private IEnumerator StartDefCooldown(float cooldownDuration)
    {
        DefenseOnCooldown = true;
        Barrier.SetActive(true);

        // Wait for the cooldown duration
        yield return new WaitForSeconds(5f);

        // Reset the cooldown
        Barrier.SetActive(false);

        yield return new WaitForSeconds(5f);

        Defense.color = Color.white;
        DefenseOnCooldown = false;
    }


// UTIL
    public void ActivateUtilAbility()
    {
        // Check if the ability is not on cooldown
        if (!UtilityOnCooldown)
        {
            // Change the scale of the GameObject
            Utility.color = utilityColor;

            // Start the cooldown
            StartCoroutine(StartUtilCooldown(UtilityCD));
        }
        else
        {
            // Ability is on cooldown
            Debug.Log("Ability is on cooldown!");
        }
    }

    // Coroutine to start the cooldown
    private IEnumerator StartUtilCooldown(float cooldownDuration)
    {
        UtilityOnCooldown = true;
        PlayerMovement.playerSize = 2;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(cooldownDuration - 15f);

        // Reset the cooldown
        PlayerMovement.playerSize = 1;

        yield return new WaitForSeconds(15f);

        UtilityOnCooldown = false;
        Utility.color = Color.white;
    }

// UTIL
    public void ActivateOffAbility()
    {
        // Check if the ability is not on cooldown
        if (!OffenseOnCooldown)
        {
            // Change the scale of the GameObject
            Offense.color = offenseColor;

            // Start the cooldown
            StartCoroutine(StartOffCooldown(OffenseCD));
        }
        else
        {
            // Ability is on cooldown
            Debug.Log("Ability is on cooldown!");
        }
    }

    // Coroutine to start the cooldown
    private IEnumerator StartOffCooldown(float cooldownDuration)
    {
        OffenseOnCooldown = true;
        var mousePos = Input.mousePosition;
        mousePos.x -= Screen.width/2;
        mousePos.y -= Screen.height/2;

        // Calculate the direction vector from the center of the screen to the mouse position.
        Vector2 directionToMouse = (Vector2)mousePos;

        directionToMouse.Normalize();
        destroyer.DestroyObjectsInConeArea(playerLoc.transform.position, directionToMouse);

        // Wait for the cooldown duration
        yield return new WaitForSeconds(cooldownDuration);

        OffenseOnCooldown = false;
        Offense.color = Color.white;
    }

    // Coroutine to gradually change the color during cooldown

}
