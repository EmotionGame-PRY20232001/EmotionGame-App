using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Rename to Exercise?
public class EmotionPhoto : MonoBehaviour
{
    public Exercise.EEmotion PhotoEmotion { get; protected set; }
    [field:SerializeField]
    public Image Photo { get; protected set; } //or RawImage
    //[SerializeField]
    //protected Image Frame;

    /// PHOTOS
    public void SetPhotoEmotion(Exercise.EEmotion emotion, Sprite photo = null)
    {
        PhotoEmotion = emotion;
        if (photo == null)
            SetRandomPhoto();
        else
            Photo.sprite = photo;
    }

    public void SetRandomPhoto()
    {
        var gm = GameManager.Instance;
        var faceImages = gm.Emotions[PhotoEmotion].Faces;
        //ExerciseImage.texture = faceImages[Random.Range(0, faceImages.Count)].texture;
        if (Photo != null)
        {
            int randIndex = Random.Range(0, faceImages.Count - 1);
            //TODO: Check if needed:
            if (faceImages.Count > randIndex)
                Photo.sprite = faceImages[randIndex];
        }
    }

    public Exercise.EEmotion SetPhotoEmotion(List<Exercise.EEmotion> emotions)
    {
        if (emotions.Count == 0)
        {
            Debug.Log("[EmotionPhoto] Empty Emotion list");
            PhotoEmotion = Exercise.EEmotion.Neutral;
        }
        else
            SetPhotoEmotion(emotions[Random.Range(0, emotions.Count - 1)]);
        return PhotoEmotion;
    }

    public Exercise.EEmotion SetPhotoEmotionFromGMSelected()
    {
        var gm = GameManager.Instance;
        return SetPhotoEmotion(gm.SelectedEmotions);
    }


    /// CONTEXTS
    public virtual void SetContextEmotion(Exercise.EEmotion emotion, string text) {}

}
