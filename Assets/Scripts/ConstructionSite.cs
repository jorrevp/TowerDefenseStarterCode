using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite 
{      
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public Enums.SiteLevel Level { get; private set; }
    public Enums.TowerType TowerType { get; private set; }
    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        WorldPosition = worldPosition + new Vector3(0, 0.5f, 0);
        Level = Enums.SiteLevel.Onbebouwd;
        tower = null;
    }

    public void SetTower(GameObject newTower, Enums.SiteLevel newLevel, Enums.TowerType newType)
    {
        if (tower != null)
        {
            Object.Destroy(tower);
        }

        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
}

