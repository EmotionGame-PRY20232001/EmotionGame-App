using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionButtonMovement : EmotionButton
{
    [SerializeField] 
    private RectTransform CanvasRectTransform;
    [SerializeField]
    protected int Velocity;
    protected RectTransform RectTransform;
    protected RectPos LatestPos;
    protected bool ToClamp = false;

    protected override void Awake()
    {
        base.Awake();
        RectTransform = GetComponent<RectTransform>();
        CanvasRectTransform = GameObject.Find("AreaOfButtons").GetComponent<RectTransform>();
        CheckTouchingBorders();
    }

    private void Update()
    {
        if (ToClamp)
        {
            ClampPosition();
            return;
        }

        RectTransform.anchoredPosition += Dir * Time.deltaTime * Velocity;
        CheckTouchingBorders();
    }

    protected void CheckTouchingBorders()
    {
        float topPos = RectTransform.anchoredPosition.y + RectTransform.rect.height / 2;
        float bottomPos = topPos - RectTransform.rect.height;
        float leftPos = RectTransform.anchoredPosition.x - RectTransform.rect.width / 2;
        float rightPos = leftPos + RectTransform.rect.width;

        if (topPos > 0 || bottomPos < -CanvasRectTransform.rect.height ||
            leftPos < 0 || rightPos > CanvasRectTransform.rect.width)
        {
            ToClamp = true;
            LatestPos.top = topPos;
            LatestPos.bottom = bottomPos;
            LatestPos.left = leftPos;
            LatestPos.right = rightPos;
        }

        if (topPos > 0 || bottomPos < -CanvasRectTransform.rect.height) Dir.y *= -1;
        if (leftPos < 0 || rightPos > CanvasRectTransform.rect.width) Dir.x *= -1;

}

    protected void ClampPosition()
    {
        if (!ToClamp) return;
        float radiusY = RectTransform.rect.height / 2;
        float radiusX = RectTransform.rect.width / 2;

        if (LatestPos.top > 0 || LatestPos.bottom < -CanvasRectTransform.rect.height)
        {
            if (LatestPos.top > 0)
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, -radiusY);
            if (LatestPos.bottom < -CanvasRectTransform.rect.height)
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, radiusY - CanvasRectTransform.rect.height);
        }

        if (LatestPos.left < 0 || LatestPos.right > CanvasRectTransform.rect.width)
        {
            if (LatestPos.left < 0)
                RectTransform.anchoredPosition = new Vector2(radiusX, RectTransform.anchoredPosition.y);
            if (LatestPos.right > CanvasRectTransform.rect.width)
                RectTransform.anchoredPosition = new Vector2(CanvasRectTransform.rect.width - radiusX, RectTransform.anchoredPosition.y);
        }

        ToClamp = false;
    }

    public struct RectPos
    {
        public float top;
        public float bottom;
        public float left;
        public float right;
    }
}
