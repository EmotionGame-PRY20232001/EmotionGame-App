using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhotosCollection : MonoBehaviour
{
    public uint NumPhotos = 10;

    [SerializeField]
    protected bool UseRandom = true;
    [SerializeField]
    protected EmotionPhoto PhotoPrefab;

    public List<EmotionPhoto> Photos { get; protected set; }

    protected void Awake()
    {
        Photos = new List<EmotionPhoto>();
    }

    public void LoadPhotos()
    {
        FillPhotos(GetQuantityPerEmotion());
        if (UseRandom)
            Photos = Photos.OrderBy(x => Random.value).ToList();
    }

    protected Dictionary<Emotion.EEmotion, uint> GetQuantityPerEmotion()
    {
        Dictionary<Emotion.EEmotion, uint> emotionExercises = new Dictionary<Emotion.EEmotion, uint>();
        var gm = GameManager.Instance;
        uint numEmotions = (uint)(gm.SelectedEmotions.Count);
        uint numPerEmotion = NumPhotos / numEmotions;

        foreach (Emotion.EEmotion emotion in gm.SelectedEmotions)
        {
            emotionExercises[emotion] = numPerEmotion;
        }

        if (NumPhotos % numEmotions != 0)
        {
            uint aux = NumPhotos - (numEmotions * numPerEmotion);
            var emotions = gm.SelectedEmotions.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < aux; i++)
            {
                Emotion.EEmotion emotion = emotions.ElementAt(i);
                emotionExercises[emotion] += 1;
            }
        }

        return emotionExercises;
    }

    protected void FillPhotos(Dictionary<Emotion.EEmotion, uint> emotionExercises)
    {
        var gm = GameManager.Instance;
        foreach (Emotion.EEmotion emotion in gm.SelectedEmotions)
        {
            List<Sprite> faceImages = gm.Emotions[emotion].Faces;
            faceImages = faceImages.OrderBy(x => Random.value).ToList();
            faceImages = faceImages.GetRange(0, (int)emotionExercises[emotion]);
            foreach (Sprite sprite in faceImages)
                LoadPhoto(emotion, sprite);
        }
    }

    protected EmotionPhoto LoadPhoto(Emotion.EEmotion emotion, Sprite sprite)
    {
        if (PhotoPrefab != null)
        {
            var photo = Instantiate(PhotoPrefab, transform);
            if (photo != null)
            {
                photo.SetPhotoEmotion(emotion, sprite);
                Photos.Add(photo);
                return photo;
            }
        }
        return null;
    }
}
