using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    [SerializeField]
    private RawImage playerBackgroundImage;

    private void Awake()
    {
        int id = GameManager.Instance.GetCurrentPlayer().BackgroundId;
        //GameManager.Instance.GetCurrentPlayer().Info();
        playerBackgroundImage.texture = GameManager.Instance.GetBackgrounds()[id];
    }

    public void ReturnToSelecion()
    {
        SceneManager.LoadScene("SelectPlayer");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSelectTheme()
    {
        SceneManager.LoadScene("SelectTheme");
    }

    public void ApplySelectedTheme()
    {
        DBManager.Instance.UpdatePlayerToDb(GameManager.Instance.GetCurrentPlayer());
        ReturnToMainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("GameImitate");
    }
}
