using System.Collections;
using System.Collections.Generic;
using SQLite;
using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
public class ExerciseContentData : MonoBehaviour
{
    public DataList DefaultDataList = new DataList();

    [System.Serializable]
    public struct Data
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        [field:SerializeField]
        public Emotion.EEmotion Emotion { get; set; }
        public ExerciseContent.EValueType Type { get; set; }
        [field:SerializeField]
        public string Value { get; set; }

        public override string ToString()
        {
            return Emotion + "-" + Id + "\t" + Value;
        }
    }

    [System.Serializable]
    public struct DataList
    {
        public static readonly string DEFAULT_PATH = "Defaults/";
        public static readonly string DEFAULT_PHOTOS_FILENAME = DEFAULT_PATH + "DefaultPhotos";
        public static readonly string DEFAULT_TEXTS_FILENAME = DEFAULT_PATH + "DefaultTexts";
        
        [System.NonSerialized]
        public ExerciseContent.EValueType Type;

        // TODO: Separate by emotion and when loadforDB return as List<Data>
        // TODO Build only using test
        public List<Data> DefaultList;

        // Only when DB loads for first time
        public static List<Data> LoadDefaultResources(string path)
        {
            //Data data = new Data();
            var datas = Utils.LoadJsonFromResources<DataList>(path);
            return datas.DefaultList;
        }

        public void LoadPhotoResourcesByEmotion(Emotion.EEmotion emotion, string path)
        {
            //Assets / Resources / Faces / Angry / Angry01.jpg
            Sprite[] photos = Resources.LoadAll<Sprite>(path);
            foreach (Sprite sprite in photos)
            {
                Data data = new Data();
                data.Emotion = emotion;
                data.Type = Type;
                data.Value = path + '/' + sprite.name;
                DefaultList.Add(data);
            }
        }
    }

    public void Clean()
    {
        DefaultDataList = new DataList();
        DefaultDataList.DefaultList = new List<Data>();
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
        Utils.SaveAsJsonToResources(DefaultDataList, DataList.DEFAULT_PHOTOS_FILENAME);
    }
    public void SaveDefaultTextResources()
    {
        Utils.SaveAsJsonToResources(DefaultDataList, DataList.DEFAULT_TEXTS_FILENAME);
    }
#endif

    protected List<Data> LoadResourcesForTest(GameObject TestLayout, string path)
    {
        if (TestLayout == null) return null;

        foreach (Transform child in TestLayout.transform)
            Destroy(child.gameObject);

        List<Data> datas = DataList.LoadDefaultResources(path);
        const int maxCount = 10;
        if (datas.Count > maxCount)
            datas.RemoveRange(maxCount, datas.Count - maxCount);
        return datas;
    }

    public void LoadPhotoResourcesTest(GameObject TestLayout)
    {
        string path = DataList.DEFAULT_PHOTOS_FILENAME;
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
        string path = DataList.DEFAULT_TEXTS_FILENAME;
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
