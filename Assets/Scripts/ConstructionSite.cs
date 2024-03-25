using System;
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
        tower = null;
    }

    public void SetTower(GameObject newTower, Enums.SiteLevel level, Enums.TowerType type)
    {
        if (tower != null)
        {
            GameObject.Destroy(tower);
        }

        tower = newTower;
        Level = level;
        TowerType = type;

        tower.transform.position = WorldPosition;
    }
    public void RemoveTower()
    {
        if (tower != null)
        {
            GameObject.Destroy(tower);
            tower = null; 
        }

        Level = Enums.SiteLevel.Onbebouwd;
        TowerType = default; 
    }
}

