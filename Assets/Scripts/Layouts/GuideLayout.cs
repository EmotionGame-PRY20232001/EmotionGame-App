using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLayout : MonoBehaviour
{
    // Using CharacterCustomization instead
    // [SerializeField]
    // private Image selectedGuideImage;
    // [SerializeField]
    // private GameObject guideGrid;
    // [SerializeField]
    // private GameObject guideButtonPrefab;
    // [SerializeField]
    // private int selectedGuide = -1;

    // private void Awake()
    // {
    //     var guideSprites = GameManager.Instance.GetGuideSprites();
    //     var player = GameManager.Instance.GetCurrentPlayer();
    //     selectedGuide = player.GuideId;
    //     selectedGuideImage.sprite = guideSprites[selectedGuide];
    //     for (int i = 0; i < guideSprites.Count; i++)
    //     {
    //         var instance = Instantiate(guideButtonPrefab, guideGrid.transform);
    //         instance.GetComponent<GuideButton>().LoadData(i, guideSprites[i]);
    //     }
    // }

    public void ApplySelection()
    {
        // var player = GameManager.Instance.GetCurrentPlayer();
        // if (selectedGuide != -1) player.GuideId = selectedGuide;
        // GameManager.Instance.SetCurrentPlayer(player);
    }

    // public void ChangeSelection(int guideId, Sprite sprite)
    // {
    //     selectedGuide = guideId;
    //     selectedGuideImage.sprite = sprite;
    // }
}
