using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    private Board board;
    public TextMeshProUGUI scoreText;
    public int score;
    public Image scoreBar;
    public GameData gameData;
    private int numberStars;

    void Start()
    {
        board = FindAnyObjectByType<Board>();
        gameData = FindAnyObjectByType<GameData>();
        UpdateBar();
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;

        if (board != null && board.scoreGoals != null)
        {
            for (int i = 0; i < board.scoreGoals.Length; i++)
            {
                if (score > board.scoreGoals[i] && numberStars < i + 1)
                {
                    numberStars++;
                }
            }
        }

        if (gameData != null && gameData.saveData != null && board != null)
        {
            if (gameData.saveData.highScores != null && board.level < gameData.saveData.highScores.Length &&
                gameData.saveData.stars != null && board.level < gameData.saveData.stars.Length)
            {
                int highScore = gameData.saveData.highScores[board.level];
                if (score > highScore)
                {
                    gameData.saveData.highScores[board.level] = score;
                }

                int currentStars = gameData.saveData.stars[board.level];
                if (numberStars > currentStars)
                {
                    gameData.saveData.stars[board.level] = numberStars;
                }
                gameData.Save();
            }
        }

        UpdateBar();
    }

    private void UpdateBar()
    {
        if (board != null && scoreBar != null && board.scoreGoals != null && board.scoreGoals.Length > 0)
        {
            int length = board.scoreGoals.Length;
            scoreBar.fillAmount = (float)score / (float)board.scoreGoals[length - 1];
        }
    }
}
