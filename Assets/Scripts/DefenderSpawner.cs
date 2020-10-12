using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    // Tunables
    [SerializeField] Defender defaultDefender = null;
    [SerializeField] float placementVolume = 0.6f;

    // State
    Defender defender = null;
    GameObject defenderParent = null;
    const string DEFENDER_PARENT_NAME = "Defenders";

    // Cached references
    StarDisplay starDisplay = null;
    LevelController levelController = null;

    private void Start()
    {
        defender = defaultDefender;
        starDisplay = FindObjectOfType<StarDisplay>();
        levelController = FindObjectOfType<LevelController>();

        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (defenderParent == null)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    private Vector3 GetSquareClicked()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return SnapToGrid(worldPoint);
    }

    private Vector3 SnapToGrid(Vector3 worldPoint)
    {
        float yPos = Mathf.RoundToInt(worldPoint.y);
        float zPos = yPos*0.5f;
        return new Vector3(Mathf.RoundToInt(worldPoint.x), yPos, zPos);
    }

    private bool AttemptToPlaceDefenderAt(Vector3 gridPos)
    {
        if (!levelController.CheckLevelComplete())
        {
            int starCost = defender.GetStarCost();
            if (starDisplay.HaveEnoughtStars(starCost))
            {
                starDisplay.SpendStars(starCost);
                SpawnDefender(gridPos);
                return true;
            }
            else { return false; }
        }
        else { return false; }
    }

    private void SpawnDefender(Vector3 spawnPosition)
    {
        Defender newDefender = Instantiate(defender, spawnPosition, Quaternion.identity);
        newDefender.transform.parent = defenderParent.transform;
        AudioClip placementSFX = defender.GetPlacementSound();
        if (placementSFX != null)
        {
            AudioSource.PlayClipAtPoint(placementSFX, Camera.main.transform.position, placementVolume * PlayerPrefsController.GetMasterVolume());
        }
    }

    public void SetDefender(Defender defender)
    {
        this.defender = defender;
    }
}
