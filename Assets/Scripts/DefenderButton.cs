using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    // Tunables
    [Range(0,255)] [SerializeField] int disableGrayLevel = 91;
    [SerializeField] Defender defenderPrefab = null;
    DefenderSpawner defenderSpawner;

    // Cached References
    SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defenderSpawner = FindObjectOfType<DefenderSpawner>();
        LabelButtonsWithCost();
    }

    private void LabelButtonsWithCost()
    {
        TextMeshProUGUI costText = GetComponentInChildren<TextMeshProUGUI>();
        if (costText != null && defenderPrefab != null)
        {
            costText.text = defenderPrefab.GetStarCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        SelectDefender();
        HighlightSelection();
    }

    private void HighlightSelection()
    {
        // Graying out all the other buttons
        DefenderButton[] selectors = FindObjectsOfType<DefenderButton>();
        foreach (DefenderButton selector in selectors)
        {
            float grayLevelNormalized = (float)disableGrayLevel / (float)255.0;
            selector.GetComponent<SpriteRenderer>().color = new Color(grayLevelNormalized, grayLevelNormalized, grayLevelNormalized, 1);
        }

        // Enabling the selected button
        spriteRenderer.color = Color.white;
    }

    private void SelectDefender()
    {
        defenderSpawner.SetDefender(defenderPrefab);
    }
}
