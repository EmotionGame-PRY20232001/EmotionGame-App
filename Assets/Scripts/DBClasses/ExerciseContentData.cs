using System.Collections;
using System.Collections.Generic;
using SQLite;
using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
public class ExerciseContentData : MonoBehaviour
{
    public DataLists DefaultDataList = new DataLists();

    [System.Serializable]
    public struct Data
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public Emotion.EEmotion Emotion { get; set; }
        public ExerciseContent.EValueType Type { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Emotion + "-" + Id + "\t" + Value;
        }
    }

    [System.Serializable]
    public class EmotionDataList
    {
        public Emotion.EEmotion Emotion;
        public List<string> Values;
    }

    [System.Serializable]
    public struct DataLists
    {
        public static readonly string DEFAULT_PATH = "Defaults/";
        public static readonly string DEFAULT_PHOTOS_FILENAME = DEFAULT_PATH + "DefaultPhotos";
        public static readonly string DEFAULT_TEXTS_FILENAME = DEFAULT_PATH + "DefaultTexts";
        
        public ExerciseContent.EValueType Type;
        public List<EmotionDataList> EmotionDatas;

        // Emotions: 0 Anger | 1 Disgust | 2 Fear | 3 Happy | 4 Neutral | 5 Sad | 6 Surprise
        //public List<string> Anger, Disgust, Fear, Happy, Sad, Surprise; //, Neutral

        // Only when DB loads for first time
        public static List<Data> LoadDefaultResources(string path)
        {
            List<Data> datas = new List<Data>();

            var dataLists = Utils.LoadJsonFromResources<DataLists>(path);
            foreach(Emotion.EEmotion emotion in System.Enum.GetValues(typeof(Emotion.EEmotion)))
            {
                var eds = dataLists.EmotionDatas.Find(x => x.Emotion == emotion);
                if (eds != null)
                {
                    foreach(string value in eds.Values)
                    {
                        datas.Add(
                            new Data {
                                Type = dataLists.Type,
                                Emotion = emotion,
                                Value = value,
                            }
                        );
                    }
                }
            }

            return datas;
        }

        public void LoadPhotoResourcesByEmotion(Emotion.EEmotion emotion, string path)
        {
            var eds = EmotionDatas.Find(x => x.Emotion == emotion);
            if (eds == null)
            {
                eds = new EmotionDataList { Emotion = emotion, Values = new List<string>() };
                EmotionDatas.Add(eds);
            }

            //Assets / Resources / Faces / Angry / Angry01.jpg
            Sprite[] photos = Resources.LoadAll<Sprite>(path);

            foreach (Sprite sprite in photos)
            {
                string value = path + '/' + sprite.name;
                eds.Values.Add(value);
            }
        }
    }

    public void Clean()
    {
        DefaultDataList = new DataLists();
        DefaultDataList.EmotionDatas = new List<EmotionDataList>();
    }

    ///<summary>
    ///path: emotion|path "0|Path/filename.jpg".<br/>
    ///Emotions: 0 Anger | 1 Disgust | 2 Fear | 3 Happy | 4 Neutral | 5 Sad | 6 Surprise
    ///</summary>
    public void LoadPhotoResourcesByEmotion(string data)
    {
        var parts = data.Split('|');
        if (parts.Length < 2)
            Debug.Log("ExerciseContentData.LoadPhotoResourcesByEmotion " + data);

        Emotion.EEmotion emotion = (Emotion.EEmotion)uint.Parse(parts[0]);
        DefaultDataList.LoadPhotoResourcesByEmotion(emotion, parts[1]);
    }

#if UNITY_EDITOR
    public void SaveDefaultPhotoResources()
    {
        Utils.SaveAsJsonToResources(DefaultDataList, DataLists.DEFAULT_PHOTOS_FILENAME, true);
    }
    public void SaveDefaultTextResources()
    {
        Utils.SaveAsJsonToResources(DefaultDataList, DataLists.DEFAULT_TEXTS_FILENAME, true);
    }
#endif

    protected List<Data> LoadResourcesForTest(GameObject TestLayout, string path)
    {
        if (TestLayout == null) return null;

        foreach (Transform child in TestLayout.transform)
            Destroy(child.gameObject);

        List<Data> datas = DataLists.LoadDefaultResources(path);
        const int maxCount = 10;
        if (datas.Count > maxCount)
            datas.RemoveRange(maxCount, datas.Count - maxCount);
        return datas;
    }

    public void LoadPhotoResourcesTest(GameObject TestLayout)
    {
        string path = DataLists.DEFAULT_PHOTOS_FILENAME;
        var datas = LoadResourcesForTest(TestLayout, path);

        foreach (Data data in datas)
        {
            //Debug.Log("ExerciseContentData.LoadPhotoResourcesTest " + data);
            if (data.Type == ExerciseContent.EValueType.ResPhoto)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetParent(TestLayout.transform);
                Image imgComp = gObj.AddComponent<Image>();
                imgComp.sprite = Utils.LoadFromResources<Sprite>(data.Value);
                //Instantiate(gObj, TestLayout.transform);
            }
        }
    }

    public void LoadTextResourcesTest(GameObject TestLayout)
    {
        string path = DataLists.DEFAULT_TEXTS_FILENAME;
        var datas = LoadResourcesForTest(TestLayout, path);

        foreach (Data data in datas)
        {
            GameObject gObj = new GameObject();
            gObj.transform.SetParent(TestLayout.transform);
            Text txtComp = gObj.AddComponent<Text>();
            txtComp.text = data.Value;
            txtComp.font = Font.CreateDynamicFontFromOSFont("Arial", 32);
            txtComp.fontSize = 32;
            //TestLayout.text += data.Value + "\n";
        }
    }
}
