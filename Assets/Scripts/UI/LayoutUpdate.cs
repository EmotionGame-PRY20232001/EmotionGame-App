using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutUpdate : MonoBehaviour
{
    [SerializeField]
    protected RectTransform LayoutToForce;
    [SerializeField]
    protected HorizontalLayoutGroup horizLayoutGroup;

    void Start()
    {
        if(LayoutToForce != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(LayoutToForce);

        horizLayoutGroup.CalculateLayoutInputHorizontal();
    }
}
