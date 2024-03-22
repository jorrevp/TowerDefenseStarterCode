using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> Archers = new List<GameObject>(); // Lijst van Archer tower prefabs
    public List<GameObject> Swords = new List<GameObject>(); // Lijst van Sword tower prefabs
    public List<GameObject> Wizards = new List<GameObject>(); // Lijst van Wizard tower prefabs

    public GameObject TowerMenu; // Referentie naar het TowerMenu GameObject
    private TowerMenu towerMenu; // Referentie naar het TowerMenu script

    private ConstructionSite selectedSite; // Variabele om geselecteerde bouwplaats te onthouden

    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
    }
    void Awake()
    {
        // Singleton instantiëren
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;

        if (towerMenu != null)
        {
            towerMenu.SetSite(site);
        }
        else
        {
            Debug.LogError("TowerMenu component is null in GameManager.");
        }
    }
    public void Build(Enums.TowerType type, Enums.SiteLevel level)
    {
        if (selectedSite == null)
        {
            Debug.LogError("Er is geen bouwplaats geselecteerd. Kan de toren niet bouwen.");
            return; 
        }

        GameObject towerPrefab = null;

        int prefabIndex = (int)level - 1;

        switch (type)
        {
            case Enums.TowerType.Archer:
                towerPrefab = Archers[prefabIndex];
                break;
            case Enums.TowerType.Sword:
                towerPrefab = Swords[prefabIndex];
                break;
            case Enums.TowerType.Wizard:
                towerPrefab = Wizards[prefabIndex];
                break;
        }

        if (towerPrefab == null)
        {
            Debug.LogError("Geen tower prefab gevonden voor het geselecteerde type en niveau.");
            return;
        }

        GameObject tower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);

        selectedSite.SetTower(tower, level, type);

        if (towerMenu != null)
        {
            towerMenu.SetSite(null); 
        }
    }

    public void DestroyTower()
    {
        if (selectedSite == null)
        {
            Debug.LogError("Er is geen bouwplaats geselecteerd. Kan de toren niet verwijderen.");
            return;
        }

        selectedSite.RemoveTower();

        if (towerMenu != null)
        {
            towerMenu.SetSite(null);
        }
    }
    public ConstructionSite GetSelectedSite()
    {
        return selectedSite;
    }
}

