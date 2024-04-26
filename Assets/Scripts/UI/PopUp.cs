using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TMPTitle;

    public delegate void OnOpen();
    public OnOpen onOpen;
    public delegate void OnClose();
    public OnClose onClose;
    public delegate void OnAccept();
    public OnAccept onAccept;

    public string Title {
        get
        {
            return TMPTitle.text;
        }
        set
        {
            TMPTitle.text = value;
        }
    }
    

    public void Open()
    {
        gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
        onOpen?.Invoke();
        //play animation
    }

    public void Close()
    {
        onClose?.Invoke();
        GameManager.Instance.ResumeGame();
        gameObject.SetActive(false);
        //play animation
    }

    public void Accept()
    {
        onAccept?.Invoke();
        Close();
    }
}