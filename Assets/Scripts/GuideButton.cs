using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideButton : MonoBehaviour
{
    [SerializeField]
    private int guideId;
    [SerializeField]
    private Image guideImage;

    public void LoadData(int id, Sprite sprite)
    {
        guideId = id;
        guideImage.sprite = sprite;
    }

    public void SelectGuide()
    {
        // var guideLayout = GetComponentInParent<GuideLayout>();
        // guideLayout.ChangeSelection(guideId, GameManager.Instance.GetGuideSprites()[guideId]);
    }
}
