using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class HighScoreManager : MonoBehaviour
{
    // Singleton instantie
    public static HighScoreManager Instance { get; private set; }

    // Public properties
    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }
    public class HighScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
    public List<HighScore> HighScores { get; set; } = new List<HighScore>();
    private void Awake()
    {
        // Controleer of er al een instantie bestaat
        if (Instance == null)
        {
            // Als er nog geen instantie is, maak deze dan de singleton instantie
            Instance = this;
            // Zorg ervoor dat dit GameObject niet wordt vernietigd wanneer een nieuwe scene wordt geladen
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Als er al een instantie bestaat, vernietig deze dan
            Destroy(gameObject);
        }
    }

    public void AddHighScore(int score)
    {
        // Kijk of deze score hoger is dan minstens 1 score in de List
        if (HighScores.Count < 5 || HighScores.Exists(hs => score > hs.Score))
        {
            // Voeg een nieuwe highscore toe
            HighScores.Add(new HighScore { Name = PlayerName, Score = score });

            // Sorteer de lijst volgens score, van hoog naar laag
            HighScores.Sort((x, y) => y.Score.CompareTo(x.Score));

            // Verwijder het laatste element als de lijst meer dan 5 elementen bevat
            if (HighScores.Count > 5)
            {
                HighScores.RemoveAt(5);
            }
        }
    }
}
