using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Rename to Exercise?
public class EmotionPhoto : MonoBehaviour
{
    public Emotion.EEmotion PhotoEmotion { get; protected set; }
    [field:SerializeField]
    public Image Photo { get; protected set; } //or RawImage
    //[SerializeField]
    //protected Image Frame;

    public System.DateTime StartedAt;

    /// PHOTOS
    public void SetPhotoEmotion(Emotion.EEmotion emotion, Sprite photo = null)
    {
        PhotoEmotion = emotion;
        if (photo == null)
            SetRandomPhoto();
        else
            Photo.sprite = photo;
    }

    //deprecated
    public void SetRandomPhoto()
    {
        var gm = GameManager.Instance;
        var faceImages = gm.Emotions[PhotoEmotion].ExerciseContents.Faces;
        //ExerciseImage.texture = faceImages[Random.Range(0, faceImages.Count)].texture;
        if (Photo != null)
        {
            int randIndex = Random.Range(0, faceImages.Count - 1);
            //TODO: Check if needed:
            if (faceImages.Count > randIndex)
                Photo.sprite = faceImages[randIndex];
        }
    }

    public Emotion.EEmotion SetPhotoEmotion(List<Emotion.EEmotion> emotions)
    {
        if (emotions.Count == 0)
        {
            Debug.Log("[EmotionPhoto] Empty Emotion list");
            PhotoEmotion = Emotion.EEmotion.Neutral;
        }
        else
            SetPhotoEmotion(emotions[Random.Range(0, emotions.Count - 1)]);
        return PhotoEmotion;
    }

    public Emotion.EEmotion SetPhotoEmotionFromGMSelected()
    {
        var gm = GameManager.Instance;
        return SetPhotoEmotion(gm.SelectedEmotions);
    }


    /// CONTEXTS
    public virtual void SetContextEmotion(Emotion.EEmotion emotion, string text) {}

}
