using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] private GameObject _scoreEntryPrefab;
    [SerializeField] private Transform _scoreContainer;    

    private void Start()
    {
        DisplayScores();
    }

    private void DisplayScores()
    {
        List<PlayerScore> playerScores = LoadPlayerScores();

        foreach (PlayerScore score in playerScores)
        {
            GameObject entry = Instantiate(_scoreEntryPrefab, _scoreContainer);  
            Text entryText = entry.GetComponentInChildren<Text>();
            entryText.text = $"{score.playerName}: {score.playerScore}";  
        }
    }

    private List<PlayerScore> LoadPlayerScores()
    {
        
        string scoresJson = PlayerPrefs.GetString("PlayerScores", JsonUtility.ToJson(new PlayerScoreList()));
        PlayerScoreList scoresList = JsonUtility.FromJson<PlayerScoreList>(scoresJson);
        return scoresList.scores ?? new List<PlayerScore>();
    }
}
