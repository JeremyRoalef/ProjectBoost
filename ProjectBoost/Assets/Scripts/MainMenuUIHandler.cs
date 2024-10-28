using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    public void onButtonPlayClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void onButtonShopClick()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void onButtonTutorialClick()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void obButtonQuitClick()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
