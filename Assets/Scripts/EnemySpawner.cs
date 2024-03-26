using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Singleton instance
    public static EnemySpawner Instance;

    // Public lists for paths and enemies
    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

    // Private counter for UFOs
    private int ufoCounter = 0;

    // Awake wordt aangeroepen wanneer de scriptinstantie wordt geladen.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SpawnEnemy(int type, Enums.Path path)
    {
        List<GameObject> selectedPath = path == Enums.Path.Path1 ? Path1 : Path2;

        if (selectedPath.Count < 2)
        {
            Debug.LogError("Path doesn't have enough waypoints.");
            return;
        }

        var newEnemy = Instantiate(Enemies[type], selectedPath[0].transform.position, selectedPath[0].transform.rotation);
        var script = newEnemy.GetComponent<Enemy>();
        script.path = path;
        script.target = selectedPath[1];
    }

    // Functie om golven te starten
    public void StartWave(int number)
    {
        // Reset de teller
        ufoCounter = 0;

        // Start de gewenste golf op basis van het nummer
        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
            // Voeg hier meer gevallen toe voor andere golven indien nodig
            case 2:
                InvokeRepeating("StartWave2", 1f, 1.5f);
                break;
            case 3:
                InvokeRepeating("StartWave3", 1f, 1f);
                break;
        }
    }
    // Functie om golven van het type 1 te starten
    private void StartWave1()
    {
        SpawnEnemy(0, Enums.Path.Path1); // Veronderstel dat type 0 een UFO is, je kunt dit aanpassen aan je eigen logica
        ufoCounter++;

        // Stop de golf na een bepaald aantal UFO's
        if (ufoCounter >= 10) // Veronderstel dat je golf na 10 UFO's moet eindigen, pas dit aan aan je eigen behoeften
        {
            CancelInvoke("StartWave1");
        }
    }
    // Functie om golven van het type 2 te starten
    private void StartWave2()
    {
        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Enums.Path.Path1); // Veronderstel dat type 0 een moeilijkere vijand is, je kunt dit aanpassen aan je eigen logica
            ufoCounter++;
        }
        else if (ufoCounter < 40)
        {
            SpawnEnemy(1, Enums.Path.Path1); // Veronderstel dat type 1 een nog moeilijkere vijand is, je kunt dit aanpassen aan je eigen logica
            ufoCounter++;
        }
        else if (ufoCounter < 60)
        {
            SpawnEnemy(Random.Range(0, Enemies.Count), Enums.Path.Path1); // Random mix van vijanden
            ufoCounter++;
        }
        else
        {
            CancelInvoke("StartWave2");
        }
    }
    // Functie om golven van het type 3 te starten
    private void StartWave3()
    {
        if (ufoCounter < 50)
        {
            SpawnEnemy(1, Enums.Path.Path1); // Veronderstel dat type 1 een moeilijkere vijand is, je kunt dit aanpassen aan je eigen logica
            ufoCounter++;
        }
        else if (ufoCounter < 70)
        {
            SpawnEnemy(2, Enums.Path.Path1); // Veronderstel dat type 2 een nog moeilijkere vijand is, je kunt dit aanpassen aan je eigen logica
            ufoCounter++;
        }
        else if (ufoCounter < 90)
        {
            SpawnEnemy(Random.Range(0, Enemies.Count), Enums.Path.Path1); // Random mix van vijanden
            ufoCounter++;
        }
        else
        {
            CancelInvoke("StartWave3");
        }
    }

    public GameObject RequestTarget(Enums.Path path, int index)
    {
        List<GameObject> selectedPath = path == Enums.Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
            return selectedPath[index];
        else
            return null;
    }
}
   

