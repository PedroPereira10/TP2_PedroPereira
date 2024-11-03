using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputField _nameInputField;
    [SerializeField] private Button _playButton;
    [SerializeField] private Text leaderboardText; 

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);
        DisplayLeaderboard();
    }

    public void StartGame()
    {
        string playerName = _nameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.SetInt("Score", 0);
            SceneManager.LoadScene("Easy");
        }
        else
        {
            Debug.Log("Please, enter a name to start.");
        }
    }

    private void DisplayLeaderboard()
    {
        leaderboardText.text = "Leaderboard:\n";

        List<PlayerScore> playerScores = LoadPlayerScores();
        foreach (PlayerScore score in playerScores)
        {
            leaderboardText.text += $"{score.playerName}: {score.playerScore}\n";
        }
    }

    private List<PlayerScore> LoadPlayerScores()
    {
        string scoresJson = PlayerPrefs.GetString("PlayerScores", JsonUtility.ToJson(new PlayerScoreList()));
        PlayerScoreList scoresList = JsonUtility.FromJson<PlayerScoreList>(scoresJson);
        return scoresList.scores ?? new List<PlayerScore>();
    }
}