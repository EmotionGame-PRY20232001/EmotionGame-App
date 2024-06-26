using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePopUp : MonoBehaviour
{
    //[SerializeField]
    //protected Button PauseButton;
    [SerializeField]
    protected Toggle TurnAudio;

    void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.Load();
        if (TurnAudio != null)
            TurnAudio.isOn = !AudioListener.pause;
        if (gameObject.activeSelf)
            Pause();
    }

    private void OnEnable()
    {
        Pause();
    }

    public void Pause()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResumeGame();
    }

    public void EnableAudio(Toggle toggle)
    {
        AudioManager.Instance?.EnableAudio(toggle);
    }
}
