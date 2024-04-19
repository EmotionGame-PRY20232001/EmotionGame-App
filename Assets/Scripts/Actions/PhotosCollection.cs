using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotosCollection : MonoBehaviour
{
    [SerializeField]
    protected bool UseRandom = true;
    [SerializeField]
    protected EmotionPhoto PhotoPrefab;
    [SerializeField]
    protected Image Frame;
    [SerializeField]
    [Tooltip("Else use text")]
    protected bool UseFaces = true;

    public List<EmotionPhoto> Photos { get; protected set; }

    protected void Awake()
    {
        Photos = new List<EmotionPhoto>();
    }

    public void LoadPhotos(uint numPhotos)
    {
        FillExercise(GetQuantityPerEmotion(numPhotos));

        if (Photos.Count > 0)
        {
            if (UseRandom)
                Photos = Photos.OrderBy(x => Random.value).ToList();

            if (Photos[0] != null)
                Photos[0].gameObject.SetActive(true);
        }

        if (Frame != null)
            Frame.transform.SetAsLastSibling();
    }

    protected Dictionary<Exercise.EEmotion, uint> GetQuantityPerEmotion(uint numPhotos)
    {
        Dictionary<Exercise.EEmotion, uint> emotionExercises = new Dictionary<Exercise.EEmotion, uint>();
        var gm = GameManager.Instance;
        uint numEmotions = (uint)(gm.SelectedEmotions.Count);
        if (numEmotions == 0)
        {
            Debug.LogError("[PhotosCollection] numEmotions selected = 0");
            return emotionExercises;
        }
        uint numPerEmotion = numPhotos / numEmotions;

        foreach (Exercise.EEmotion emotion in gm.SelectedEmotions)
        {
            emotionExercises[emotion] = numPerEmotion;
        }

        if (numPhotos % numEmotions != 0)
        {
            uint aux = numPhotos - (numEmotions * numPerEmotion);
            var emotions = gm.SelectedEmotions.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < aux; i++)
            {
                Exercise.EEmotion emotion = emotions.ElementAt(i);
                emotionExercises[emotion] += 1;
            }
        }

        return emotionExercises;
    }

    protected void FillExercise(Dictionary<Exercise.EEmotion, uint> emotionExercises)
    {
        var gm = GameManager.Instance;

        if (UseFaces)
        {
            foreach (Exercise.EEmotion emotion in gm.SelectedEmotions)
            {
                var faceImages = LoadByEmotionQ(emotion, (int)emotionExercises[emotion], gm.Emotions[emotion].Faces);
                if (faceImages != null)
                {
                    foreach (Sprite sprite in faceImages)
                        LoadPhoto(emotion, sprite);
                }
            }
        }
        else
        {
            foreach (Exercise.EEmotion emotion in gm.SelectedEmotions)
            {
                var contexts = LoadByEmotionQ(emotion, (int)emotionExercises[emotion], gm.Emotions[emotion].Contexts);
                if (contexts != null)
                {
                    foreach (string context in contexts)
                        LoadContext(emotion, context);
                }
            }
        }
    }

    protected List<T> LoadByEmotionQ<T>(Exercise.EEmotion emotion, int q, List<T> list)
    {
        if (list.Count < q)
        {
            Debug.LogWarning("[PhotosCollection] " + emotion + " has less Contexts texts than " + q + "!");
            //numPhotos = (uint)(numPhotos - (q - faceImages.Count));
            q = list.Count;

            if (q == 0)
                return null;
        }

        list = list.OrderBy(x => Random.value).ToList();
        list = list.GetRange(0, q);

        return list;
    }

    protected EmotionPhoto LoadPhoto(Exercise.EEmotion emotion, Sprite sprite)
    {
        if (PhotoPrefab == null) return null;
        var photo = Instantiate(PhotoPrefab, transform);
        photo.SetPhotoEmotion(emotion, sprite);
        Photos.Add(photo);
        return photo;
    }

    protected EmotionPhoto LoadContext(Exercise.EEmotion emotion, string text)
    {
        if (PhotoPrefab == null) return null;
        var photo = Instantiate(PhotoPrefab, transform);
        photo.SetContextEmotion(emotion, text);
        Photos.Add(photo);
        return photo;
    }

}
