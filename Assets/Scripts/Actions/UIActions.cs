using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    // [SerializeField]
    // private Image guideImage;

    private void Awake()
    {
        // var player = GameManager.Instance.GetCurrentPlayer();
        //GameManager.Instance.GetCurrentPlayer().Info();
        // if (guideImage) guideImage.sprite = GameManager.Instance.GetGuideSprites()[player.GuideId];
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
        var player = GameManager.Instance.GetCurrentPlayer();
        if (player == null)
        {
            Debug.Log("Current player is null");
            return;
        }
        DBManager.Instance.UpdatePlayerToDb(player);
        ReturnToMainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("GameImitate");
    }
}
