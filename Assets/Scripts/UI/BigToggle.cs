using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class BigToggle : MonoBehaviour
{
    [SerializeField]
    protected RectTransform IconRect;
    [SerializeField]
    protected RectTransform PreviewRect;
    [SerializeField]
    protected CanvasGroup PreviewCanvas;
    
    public Toggle ToggleComp { get; protected set; }
    protected RectTransform Rect;
    
    [SerializeField]
    protected float TransitionTime = 0.5f;
    [SerializeField]
    protected float IconGrowFactor = 2.0f;
    protected bool wasOn = false;

    protected virtual void Awake()
    {
        ToggleComp = gameObject.GetComponent<Toggle>();
        Rect = gameObject.GetComponent<RectTransform>();
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
            LeanTween.size(Rect, PreviewRect.sizeDelta, TransitionTime);
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

    protected virtual void OnIconHidden()
    {
        IconRect.gameObject.SetActive(false);
    }

    protected virtual void OnPreviewHidden()
    {
        PreviewRect.gameObject.SetActive(false);
        //TODO: animation stop
    }
}
