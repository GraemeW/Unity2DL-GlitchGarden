using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarDisplay : MonoBehaviour
{
    TextMeshProUGUI starText = null;
    [SerializeField] int starCount = 100;
    [SerializeField] int maxStarCount = 99999;

    private void Start()
    {
        starText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        starText.text = starCount.ToString();
    }

    public void SpendStars(int starCost)
    {
        starCount -= starCost;
        UpdateDisplay();
    }

    public bool HaveEnoughtStars(int starCost)
    {
        if (starCount >= starCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddStars(int starIncrement)
    {
        starCount += starIncrement;
        starCount = Mathf.Clamp(starCount, starCount, maxStarCount);
        UpdateDisplay();
    }
}
