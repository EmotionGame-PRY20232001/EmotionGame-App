using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    [SerializeField]
    private Image guideImage;

    private void Awake()
    {
        var player = GameManager.Instance.GetCurrentPlayer();
        //GameManager.Instance.GetCurrentPlayer().Info();
        if (guideImage) guideImage.sprite = GameManager.Instance.GetGuideSprites()[player.GuideId];
    }

    public void ReturnToSelecion()
    {
        GameManager.Instance.SetCurrentPlayer(null);
        SceneManager.LoadScene("PlayerSelect");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSelectTheme()
    {
        SceneManager.LoadScene("SelectTheme");
    }

    public void ApplySelectedOptions()
    {
        DBManager.Instance.UpdatePlayerToDb(GameManager.Instance.GetCurrentPlayer());
        ReturnToMainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("GameImitate");
    }
}
