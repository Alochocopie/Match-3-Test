using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "World", menuName = "Level")]
public class Level: ScriptableObject
{
    [Header("Board Dimensions")]
    public int width;
    public int height;

    [Header("Stating Tiles")]
    public TileType[] boardLayout;

    [Header("Available Dots")]
    public GameObject[] dots;

    [Header("Score Goals")]
    public int[] scoreGoals;

    [Header("End Game Requirements")]
    public EndGameRequiremets endGameRequirements;
    public BlankGoal[] levelGoals;

}
