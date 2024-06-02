using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratsRibborn : MonoBehaviour
{
    CanvasGroup CanvasGr;
    public float TimeTransitionIn = 1.0f;
    public float TimeLoop = 1.0f;
    public float ScaleFrom = 0.5f;

    void Start()
    {
        TransitionIn();
    }

    protected void TransitionIn()
    {
        float posY = gameObject.transform.localPosition.y;
        float newPosY = gameObject.transform.localPosition.y;
        newPosY -= newPosY;

        LeanTween.moveLocalY(gameObject, posY, TimeTransitionIn).setFrom(newPosY).setOnComplete(Loop);
        LeanTween.scale(gameObject, Vector3.one, TimeTransitionIn).setFrom(ScaleFrom);
        LeanTween.alpha(gameObject, 1.0f, TimeTransitionIn).setFrom(0.0f);
    }

    protected void Loop()
    {
        const float start = 0.0f;
        const float end = start + 64.0f;
        LeanTween.moveLocalY(gameObject, transform.localPosition.y + end, TimeLoop)
            .setFrom(transform.localPosition.y + start)
            .setLoopPingPong()
            .setIgnoreTimeScale(true);
    }
}
