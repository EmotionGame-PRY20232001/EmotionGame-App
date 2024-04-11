using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionPhoto : MonoBehaviour
{
    [SerializeField]
    protected Image Photo; //or RawImage
    [SerializeField]
    protected Image Frame;
    protected Emotion.EEmotion m_Emotion;

    [SerializeField]
    public Emotion.EEmotion PhotoEmotion {
        get { return m_Emotion; }
        set { SetPhotoEmotion(value); }
    }

    public void SetPhotoEmotion(Emotion.EEmotion emotion)
    {
        m_Emotion = emotion;
        var gm = GameManager.Instance;
        var faceImages = gm.Emotions[m_Emotion].Faces;
        //ExerciseImage.texture = faceImages[Random.Range(0, faceImages.Count)].texture;
        Photo.sprite = faceImages[Random.Range(0, faceImages.Count)];
    }

    public Emotion.EEmotion SetPhotoEmotion(List<Emotion.EEmotion> emotions)
    {
        if (emotions.Count == 0)
        {
            Debug.Log("[EmotionPhoto] Empty Emotion list");
            m_Emotion = Emotion.EEmotion.Neutral;
        }
        else
            SetPhotoEmotion(emotions[Random.Range(0, emotions.Count)]);
        return m_Emotion;
    }
    public Emotion.EEmotion SetPhotoEmotionFromGMSelected()
    {
        var gm = GameManager.Instance;
        return SetPhotoEmotion(gm.SelectedEmotions);
    }
}
