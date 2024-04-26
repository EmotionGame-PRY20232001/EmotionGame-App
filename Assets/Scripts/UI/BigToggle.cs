using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Toggle))]
public class BigToggle : MonoBehaviour
{
    [SerializeField]
    protected RectTransform IconRect;
    [SerializeField]
    protected RectTransform PreviewRect;
    [SerializeField]
    protected CanvasGroup PreviewCanvas;
    [SerializeField]
    protected RectTransform LayoutRect;

    //TODO: Inheritance
    [SerializeField]
    protected TMP_Text Text;
    [SerializeField]
    protected Exercise.EActivity Activity;

    public Toggle ToggleComp { get; protected set; }
    protected RectTransform Rect;
    
    public string Name;
    [SerializeField]
    protected float TransitionTime = 0.5f;
    [SerializeField]
    protected float IconGrowFactor = 2.0f;
    protected bool wasOn = false;

    protected virtual void Awake()
    {
        ToggleComp = gameObject.GetComponent<Toggle>();
        Rect = gameObject.GetComponent<RectTransform>();
        //wasOn = !ToggleComp.isOn;
    }

    void Start()
    {
        UpdateToggle(ToggleComp);

        ToggleComp.onValueChanged.AddListener(delegate {
            UpdateToggle(ToggleComp);
        });
    }

    protected virtual void UpdateToggle(Toggle toggle)
    {
        if (ToggleComp == null || wasOn == ToggleComp.isOn)
            return;

        // Debug.Log("Is being " + ToggleComp.isOn);

        if (ToggleComp.isOn)
        {
            OnToggleEnabled();
        }
        else
        {
            OnToggleDisabled();
        }
        wasOn = ToggleComp.isOn;
    }

    protected virtual void OnToggleEnabled()
    {
        // Change container size
        if (Rect != null && PreviewRect != null)
        {
            LeanTween.size(Rect, PreviewRect.sizeDelta, TransitionTime).setOnUpdate(OnRectTransform);
        }

        // Show preview
        if (PreviewCanvas != null)
        {
            PreviewCanvas.gameObject.SetActive(true);
            LeanTween.alphaCanvas(PreviewCanvas, 1.0f, TransitionTime);
        }
        
        // Hide icon
        if (IconRect != null)
        {
            // Grow icon
            LeanTween.size(IconRect, IconRect.sizeDelta*IconGrowFactor, TransitionTime/2);
            // Fade icon
            LeanTween.alpha(IconRect.gameObject, 0.0f, TransitionTime/2).setOnComplete(OnIconHidden);
        }

        OnActivitySelected();
    }

    protected virtual void OnToggleDisabled()
    {
        // Change container size
        if (Rect != null && IconRect != null)
        {
            LeanTween.size(Rect, IconRect.sizeDelta, TransitionTime);
        }

        // Hide preview
        if (PreviewCanvas != null)
        {
            LeanTween.alphaCanvas(PreviewCanvas, 0.0f, TransitionTime/2).setOnComplete(OnPreviewHidden);
        }

        // Show icon
        if (IconRect != null)
        {
            IconRect.gameObject.SetActive(true);
            // Grow icon
            LeanTween.size(IconRect, IconRect.sizeDelta/IconGrowFactor, TransitionTime);
            // Fade icon
            LeanTween.alpha(IconRect.gameObject, 1.0f, TransitionTime/2);
        }
    }

    protected virtual void OnRectTransform(float value) //IEnumerator
    {
        if (!ToggleComp.isOn || LayoutRect == null)
            return; // yield return null;

        // https://forum.unity.com/threads/force-immediate-layout-update.372630/
        LayoutRebuilder.ForceRebuildLayoutImmediate(LayoutRect);
    }

    protected virtual void OnIconHidden()
    {
        IconRect.gameObject.SetActive(false);
    }

    protected virtual void OnPreviewHidden()
    {
        PreviewRect.gameObject.SetActive(false);
        //TODO: animation stop
    }

    protected virtual void OnActivitySelected()
    {
        // Update Text
        if (Text != null)
            Text.text = "¡" + Name + "!";

        var gm = GameManager.Instance;
        if (gm != null)
            gm.LastPlayedGame = Activity;
    }
}
