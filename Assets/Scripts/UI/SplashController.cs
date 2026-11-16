using UnityEngine;

public class SplashController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject levelSelectPanel;
    void Start()
    {
        if (BackToSplash.openLevelSelect)
        {
            startPanel.SetActive(false);
            levelSelectPanel.SetActive(true);
        }
        else
        {
            startPanel.SetActive(true);
            levelSelectPanel.SetActive(false);
        }
    }
}