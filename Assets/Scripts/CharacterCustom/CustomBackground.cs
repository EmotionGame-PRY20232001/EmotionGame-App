using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CustomBackground : MonoBehaviour
{
    RawImage playerBackgroundImage;
    // private void Awake()
    // {
    // }

    private void Start()
    {
        playerBackgroundImage = GetComponent<RawImage>();

        if (GameManager.Instance != null)
        {
            var player = GameManager.Instance.GetCurrentPlayer();
            playerBackgroundImage.texture = player == null ? GameManager.Instance.MainBackground :
                                                             GameManager.Instance.GetBackgrounds()[player.BackgroundId];
        }
    }
}
