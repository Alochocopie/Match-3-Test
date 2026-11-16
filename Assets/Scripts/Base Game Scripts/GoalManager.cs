using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BlankGoal
{
    public int numberNeed;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;
}

public class GoalManager : MonoBehaviour
{
    public BlankGoal[] levelGoals;
    public List<GoalPanel> currentGoals = new List<GoalPanel>();
    public GameObject goalPrefab;
    public GameObject goalIntroParent;
    public GameObject goalGameParent;
    private Board board;
    private EndGameManager endGame;


    void Start()
    {
        board = FindAnyObjectByType<Board>();
        endGame = FindAnyObjectByType<EndGameManager>();
        GetGoals();
        SetupGoals();
    }

    void GetGoals()
    {
        if (board != null)
        {
            if (board.world != null)
            {
                if (board.level < board.world.levels.Length)
                {
                    if (board.world.levels[board.level] != null)
                    {
                        levelGoals = board.world.levels[board.level].levelGoals;
                        for (int i = 0; i < levelGoals.Length; i++)
                        {
                            levelGoals[i].numberCollected = 0;
                        }
                    }
                }
            }
        }
    }

    void SetupGoals()
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {
            //Create a new goal panel at the goalIntroParent position
            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity);
            goal.transform.SetParent(goalIntroParent.transform, false);
            //Set image and text of the goal panel
            GoalPanel panel = goal.GetComponent<GoalPanel>();
            panel.thisSprite = this.levelGoals[i].goalSprite;
            panel.thisString = "0/" + this.levelGoals[i].numberNeed;
            //Create a new Goalpanel at the goalGameParent position
            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity);
            gameGoal.transform.SetParent(goalGameParent.transform, false);
            panel = gameGoal.GetComponent<GoalPanel>();
            currentGoals.Add(panel);
            panel.thisSprite = this.levelGoals[i].goalSprite;
            panel.thisString = "0/" + this.levelGoals[i].numberNeed;
        }
    }

    public void UpdateGoals()
    {
        int goalsCompleted = 0;
        for (int i= 0; i < levelGoals.Length; i++)
        {
            currentGoals[i].thisText.text = levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeed;
            if (levelGoals[i].numberCollected >= levelGoals[i].numberNeed)
            {
                goalsCompleted++;
                currentGoals[i].thisText.text = levelGoals[i].numberNeed + "/" + levelGoals[i].numberNeed;
            }
        }
        if (goalsCompleted >= levelGoals.Length)
        {
            if (endGame != null)
            {
                endGame.WinGame();
            }
            Debug.Log("WIN");
        }
    }
    public void CompareGoal(string goalToCompare)
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {
            if (levelGoals[i].matchValue == goalToCompare)
            {
                levelGoals[i].numberCollected++;
                UpdateGoals();
            }
        }
    }

}

