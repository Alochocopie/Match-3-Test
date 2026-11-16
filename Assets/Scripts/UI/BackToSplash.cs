using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSplash : MonoBehaviour
{
    public string sceneToLoad;
    private GameData gameData;
    public static bool openLevelSelect = false;
    private Board board;
    private ScoreManager scoreManager;


    void Start()
    {
        gameData = FindAnyObjectByType<GameData>();
        board = FindAnyObjectByType<Board>();
    }
    public void WinOK()
    {
      
        if (board == null) board = FindAnyObjectByType<Board>();
        if (scoreManager == null) scoreManager = FindAnyObjectByType<ScoreManager>();

        if (GameData.gameData != null && GameData.gameData.saveData != null && board != null)
        {
            int nextLevelIndex = board.level + 1;
            if (nextLevelIndex < GameData.gameData.saveData.isActive.Length)
            {
                GameData.gameData.saveData.isActive[nextLevelIndex] = true;
            }

            if (scoreManager != null && board.scoreGoals != null && board.scoreGoals.Length > 0)
            {
                int starsEarned = 0;
                for (int i = 0; i < board.scoreGoals.Length; i++)
                {
                    if (scoreManager.score >= board.scoreGoals[i])
                    {
                        starsEarned = i + 1;
                    }
                }

                if (board.level < GameData.gameData.saveData.stars.Length)
                {
                    if (starsEarned > GameData.gameData.saveData.stars[board.level])
                    {
                        GameData.gameData.saveData.stars[board.level] = starsEarned;
                    }
                }
            }

            GameData.gameData.Save();
        }
        openLevelSelect = true;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoseOK()
    {
        openLevelSelect = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}
