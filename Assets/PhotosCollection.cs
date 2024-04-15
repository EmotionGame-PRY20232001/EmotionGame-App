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

    public List<EmotionPhoto> Photos { get; protected set; }

    protected void Awake()
    {
        Photos = new List<EmotionPhoto>();
    }

    public void LoadPhotos(uint numPhotos)
    {
        FillPhotos(GetQuantityPerEmotion(numPhotos), numPhotos);

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

    protected void FillPhotos(Dictionary<Exercise.EEmotion, uint> emotionExercises, uint numPhotos)
    {
        var gm = GameManager.Instance;
        foreach (Exercise.EEmotion emotion in gm.SelectedEmotions)
        {
            List<Sprite> faceImages = gm.Emotions[emotion].Faces;
            int q = (int)emotionExercises[emotion];

            if (faceImages.Count < q)
            {
                Debug.LogWarning("[PhotosCollection] " + emotion + " has less face Images " + q + "!");
                numPhotos = (uint)(numPhotos - (q - faceImages.Count));
                q = faceImages.Count;

                if (q == 0)
                    continue;
            }

            faceImages = faceImages.OrderBy(x => Random.value).ToList();
            faceImages = faceImages.GetRange(0, q);
            foreach (Sprite sprite in faceImages)
                LoadPhoto(emotion, sprite);
        }
    }

    protected EmotionPhoto LoadPhoto(Exercise.EEmotion emotion, Sprite sprite)
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

    //protected void LoadFrame()
    //{
        //if (Frame == null) return;
        //var gm = GameManager.Instance;
        //Frame.sprite = GameManager.Instance.GetBackgrounds()[id].Texture;
    //}

}
