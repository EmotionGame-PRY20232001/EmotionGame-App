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
        playerBackgroundImage.texture = GameManager.Instance.GetBackgrounds()[id];
    }

    public void ReturnToSelecion()
    {
        SceneManager.LoadScene("SelectPlayer");
    }

    public void Play()
    {
        SceneManager.LoadScene("GameImitate");
    }
}
