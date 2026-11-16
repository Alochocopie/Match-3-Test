using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;
    private int starsActive;

    [Header("Level UI")]
    public Image[] stars;
    public TextMeshProUGUI levelText;
    public int level;
    public GameObject confirmPanel;

    private GameData gameData;
    void Start()
    {
        gameData = FindAnyObjectByType<GameData>();
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        LoadData();
        ActivateStars();
        DecideSprite();
        ShowLevel();
    }

    void LoadData()
    {
        //Is GameData present?
        /*if(gameData != null)
        {
            //Decide if the level is active
            if (gameData.saveData.isActive[level - 1])
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            //Decide how many stars to activate
            starsActive = gameData.saveData.stars[level - 1];
        }*/
        if (GameData.gameData != null && GameData.gameData.saveData != null)
        {
            int index = level - 1;

            if (GameData.gameData.saveData.isActive != null &&
                index >= 0 && index < GameData.gameData.saveData.isActive.Length)
            {
                isActive = GameData.gameData.saveData.isActive[index];
            }
            else
            {
                isActive = (level == 1); 
            }

            if (GameData.gameData.saveData.stars != null &&
                index >= 0 && index < GameData.gameData.saveData.stars.Length)
            {
                starsActive = GameData.gameData.saveData.stars[index];
            }
        }
    }

    void ActivateStars()
    {
        for ( int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }
    void DecideSprite()
    {
        if (isActive)
        {
            buttonImage.sprite = activeSprite;
            myButton.enabled = true;
            levelText.enabled = true;
        }
        else
        {
            buttonImage.sprite = lockedSprite;
            myButton.enabled = false;
            levelText.enabled = false;
        }
    }

    void ShowLevel()
    {
        levelText.text = "" + level;
    }



    public void ConfirmPanel(int level)
    {
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
