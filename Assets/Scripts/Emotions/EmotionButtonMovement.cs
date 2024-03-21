using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionButtonMovement : MonoBehaviour
{
    [SerializeField]
    protected int Velocity;
    protected RectTransform RectTransform;
    protected Vector2 Dir;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Dir = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        //float randomNum = Random.Range(0f, 2 * Mathf.PI);
        //Vector2 randomVec = new Vector2(Mathf.Cos(randomNum), Mathf.Sin(randomNum));
        RectTransform.anchoredPosition += Dir * Time.deltaTime * Velocity;
        CheckTouchingBorders();
    }

    protected void CheckTouchingBorders()
    {
        float topPos = RectTransform.anchoredPosition.y + RectTransform.rect.height / 2;
        float bottomPos = topPos - RectTransform.rect.height;
        float leftPos = RectTransform.anchoredPosition.x - RectTransform.rect.width / 2;
        float rightPos = leftPos + RectTransform.rect.width;
        if (topPos > 0 || bottomPos < -Screen.height) Dir.y *= -1;
        if (leftPos < 0 || rightPos > Screen.width) Dir.x *= -1;
    }
}
