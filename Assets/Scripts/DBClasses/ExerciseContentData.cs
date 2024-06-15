using System.Collections;
using System.Collections.Generic;
using SQLite;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

//[System.Serializable]
public class ExerciseContentData : MonoBehaviour
{
    public DataLists DefaultDataList = new DataLists();

    [System.Serializable]
    public class Data
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public Emotion.EEmotion Emotion { get; set; }
        public ExerciseContent.EValueType Type { get; set; }
        public string Value { get; set; }

        public Data (Emotion.EEmotion emotion, ExerciseContent.EValueType valueType, string value)
        {
            Emotion = emotion;
            Type = valueType;
            Value = value;
        }

        public override string ToString()
        {
            return Emotion + "-" + Id + "\t" + Value;
        }
    }

    [System.Serializable]
    public struct DataLists
    {
        public static readonly string DEFAULT_PATH = "Defaults/";
        public static readonly string DEFAULT_PHOTOS_FILENAME = DEFAULT_PATH + "DefaultPhotos";
        public static readonly string DEFAULT_TEXTS_FILENAME = DEFAULT_PATH + "DefaultTexts";
        
        public ExerciseContent.EValueType Type;
        //public List<EmotionDataList> EmotionDatas;
        // Emotions: 0 Anger | 1 Disgust | 2 Fear | 3 Happy | 4 Neutral | 5 Sad | 6 Surprise
        public List<string> Anger, Disgust, Fear, Happy, Sad, Surprise; //, Neutral

        // Only when DB loads for first time
        public static List<Data> LoadDefaultResources(string path)
        {
            List<Data> datas = new List<Data>();
            var dataLists = Utils.LoadJsonFromResources<DataLists>(path);

            foreach(string value in dataLists.Anger)
                datas.Add( new Data(Emotion.EEmotion.Anger, dataLists.Type, value) );
            foreach(string value in dataLists.Disgust)
                datas.Add( new Data(Emotion.EEmotion.Disgust, dataLists.Type, value) );
            foreach(string value in dataLists.Fear)
                datas.Add( new Data(Emotion.EEmotion.Fear, dataLists.Type, value) );
            foreach(string value in dataLists.Happy)
                datas.Add( new Data(Emotion.EEmotion.Happy, dataLists.Type, value) );
            foreach(string value in dataLists.Sad)
                datas.Add( new Data(Emotion.EEmotion.Sad, dataLists.Type, value) );
            foreach(string value in dataLists.Surprise)
                datas.Add( new Data(Emotion.EEmotion.Surprise, dataLists.Type, value) );

            //foreach(Emotion.EEmotion emotion in System.Enum.GetValues(typeof(Emotion.EEmotion)))
                //var eds = dataLists.EmotionDatas.Find(x => x.Emotion == emotion);
                //if (eds != null)
                //foreach(string value in eds.Values)
                    //datas.Add( new Data(Emotion.EEmotion.Surprise, dataLists.Type, value) );

            return datas;
        }

        public void LoadPhotoResourcesByEmotion(Emotion.EEmotion emotion, string path)
        {
            //var eds = EmotionDatas.Find(x => x.Emotion == emotion);
            //if (eds == null)
            //{
            //    eds = new EmotionDataList { Emotion = emotion, Values = new List<string>() };
            //    EmotionDatas.Add(eds);
            //}
            //var lst = eds.Values;

            List<string> lst;
            switch (emotion)
            {
                case Emotion.EEmotion.Anger: lst = Anger; break;
                case Emotion.EEmotion.Disgust: lst = Disgust; break;
                case Emotion.EEmotion.Fear: lst = Fear; break;
                case Emotion.EEmotion.Happy: lst = Happy; break;
                case Emotion.EEmotion.Sad: lst = Sad; break;
                case Emotion.EEmotion.Surprise: lst = Surprise; break;
                //case Emotion.EEmotion.Neutral: break;
                default: return;
            }


            //Assets / Resources / Faces / Angry / Angry01.jpg
            //Sprite[] photos = Resources.LoadAll<Sprite>(path);
            string[] files = Utils.GetFilesFromResources(path, new[] { ".png", ".jpg" });

            //foreach (Sprite sprite in photos)
            foreach (string value in files)
            {
                //string value = path + '/' + sprite.name;
                lst.Add(value);
            }
        }

        public void Clean()
        {
            Anger = new List<string>();
            Disgust = new List<string>();
            Fear = new List<string>();
            Happy = new List<string>();
            Sad = new List<string>();
            Surprise = new List<string>();
        }
    }

    public void Clean()
    {
        DefaultDataList = new DataLists();
        DefaultDataList.Clean();
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
        Utils.SaveAsJsonToResources(DefaultDataList, DataLists.DEFAULT_PHOTOS_FILENAME);
    }
    public void SaveDefaultTextResources()
    {
        Utils.SaveAsJsonToResources(DefaultDataList, DataLists.DEFAULT_TEXTS_FILENAME);
    }
#endif

    protected List<Data> LoadResourcesForTest(GameObject TestLayout, string path)
    {
        if (TestLayout == null) return null;

        foreach (Transform child in TestLayout.transform)
            Destroy(child.gameObject);

        List<Data> datas = DataLists.LoadDefaultResources(path);
        datas = Utils.Shuffle(datas);

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
