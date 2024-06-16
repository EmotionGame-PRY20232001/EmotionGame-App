using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TMPTitle;
    [SerializeField]
    protected RectTransform Panel;
    public float TransitionTime = 0.25f;

    public delegate void OnOpen();
    public OnOpen onOpen;
    public delegate void OnClose();
    public OnClose onClose;
    public delegate void OnAccept();
    public OnAccept onAccept;

    protected AudioPlaySwitch AudioSwitch;
    protected CanvasGroup PopUpCanvas;

    public string Title {
        get { return TMPTitle.text; }
        set { TMPTitle.text = value; }
    }

    private void Awake()
    {
        AudioSwitch = GetComponent<AudioPlaySwitch>();
        PopUpCanvas = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        //play animation
        AudioSwitch?.Play(true);
        if (PopUpCanvas != null)
            PopUpCanvas.LeanAlpha(1.0f, TransitionTime)
                .setFrom(0.0f)
                .setIgnoreTimeScale(true);
        if (Panel != null)
            Panel.LeanScale(Vector3.one, TransitionTime).setFrom(Vector3.one / 2.0f)
                .setIgnoreTimeScale(true);

        onOpen?.Invoke();
    }

    public void Close()
    {
        //play animation
        AudioSwitch?.Play(false);
        if (PopUpCanvas != null)
            PopUpCanvas.LeanAlpha(0.0f, TransitionTime)
                .setFrom(1.0f)
                .setIgnoreTimeScale(true);
        if (Panel != null)
            Panel.LeanScale(Vector3.one / 2, TransitionTime)
                .setFrom(Vector3.one)
                .setIgnoreTimeScale(true)
                .setOnComplete(OnClosed);
        else
            OnClosed();

        onClose?.Invoke();
    }

    protected void OnClosed()
    {
        LeanTween.cancel(gameObject);
        gameObject.SetActive(false);
    }

    public void Accept()
    {
        onAccept?.Invoke();
        Close();
    }
}