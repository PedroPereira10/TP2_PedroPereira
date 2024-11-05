using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _victoryScreen;

    [SerializeField] private Text _playerNameText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _totalScoreText;
    [SerializeField] private Text _itemValueText;
    [SerializeField] private Text _itemCounterText;

    private int score;
    private int totalScore;
    private int itemCounter;
    private int lastItemValue;

    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        score = 0;
        itemCounter = 0;
        lastItemValue = 0;

        _playerNameText.text = "" + PlayerPrefs.GetString("PlayerName", "Jogador");
        UpdateScore();
        UpdateTotalScore();
        UpdateItemCounter();
        UpdateItemValue(lastItemValue);
    }

    private void UpdateScore()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + score.ToString();
        }
    }

    private void UpdateTotalScore()
    {
        if (_totalScoreText != null)
        {
            _totalScoreText.text = "Total Score: " + totalScore.ToString();
        }
    }

    private void UpdateItemCounter()
    {
        if (_itemCounterText != null)
        {
            _itemCounterText.text = "Counter:" + itemCounter.ToString();
        }
    }

    public void UpdateItemValue(int value)
    {
        lastItemValue = value;

        if (_itemValueText != null)
        {
            _itemValueText.text = "Value: " + lastItemValue.ToString();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        totalScore += points;
        itemCounter += points;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("TotalScore", totalScore);

        UpdateScore();
        UpdateTotalScore();
        UpdateItemCounter();
    }

    private void SavePlayerScore()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Jogador");
        string scoresKey = "PlayerScores";

        List<PlayerScore> playerScores = LoadPlayerScores();
        playerScores.Add(new PlayerScore(playerName, score));

        PlayerPrefs.SetString(scoresKey, JsonUtility.ToJson(new PlayerScoreList(playerScores)));
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        SavePlayerScore();
        _gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PlayLoseSound();
    }

    public void Victory()
    {
        SavePlayerScore();
        _victoryScreen.SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PlayWinSound();
    }

    private List<PlayerScore> LoadPlayerScores()
    {
        string scoresJson = PlayerPrefs.GetString("PlayerScores", JsonUtility.ToJson(new PlayerScoreList()));
        PlayerScoreList scoresList = JsonUtility.FromJson<PlayerScoreList>(scoresJson);
        return scoresList.scores ?? new List<PlayerScore>();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void LoadResult()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Results", LoadSceneMode.Single);
    }
}

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int playerScore;

    public PlayerScore(string name, int score)
    {
        playerName = name;
        playerScore = score;
    }
}

[System.Serializable]
public class PlayerScoreList
{
    public List<PlayerScore> scores;

    public PlayerScoreList()
    {
        scores = new List<PlayerScore>();
    }

    public PlayerScoreList(List<PlayerScore> scores)
    {
        this.scores = scores;
    }
}