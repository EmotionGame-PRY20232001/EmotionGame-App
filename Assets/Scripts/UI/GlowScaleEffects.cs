using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScaleEffects : MonoBehaviour
{
    public float TimeLoop = 1.0f;
    public float ScaleTo = 1.1f;
    public bool rotate;
    [SerializeField]
    protected float GlowLoopTime = 1.0f;
    [SerializeField] [Tooltip("Number of rotations per Scale")]
    protected float GlowLoopRotFactor = 4.0f;


    void Start()
    {
        Loop();
    }

    protected void Loop()
    {
        LeanTween.scale(gameObject, Vector3.one * ScaleTo, TimeLoop)
            .setFrom(Vector3.one)
            .setLoopPingPong()
            .setIgnoreTimeScale(true);

        if (rotate)
        {
            float rotationZ = Random.Range(0.0f, 360.0f);
            bool toRight = Random.Range(0.0f, 1.0f) >= 0.5f;
            gameObject.transform.rotation = Quaternion.AngleAxis(rotationZ, Vector3.forward);
            LeanTween.rotateAroundLocal(gameObject, toRight ? Vector3.forward : Vector3.back, 360f, GlowLoopRotFactor)
                .setRepeat(-1)
                .setIgnoreTimeScale(true);
        }
    }
}
