using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Utils : MonoBehaviour
{
    static public void SetImage(GameObject _object, Sprite sprite)
    {
        if (_object == null) return;
        Image image = _object.GetComponent<Image>();
        SetImage(image, sprite);
    }
    
    static public void SetImage(Image image, Sprite sprite)
    {
        if (image == null) return;
        
        if (sprite == null)
        {
            image.enabled = false;
        }
        else
        {
            if (!image.enabled)
                image.enabled = true;
            // image.overrideSprite = sprite;
            image.sprite = sprite;
        }
    }
    
    
    /// <summary>
    /// WITH ENUMERABLES
    /// </summary>
    
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    static public IEnumerable<T> Shuffle<T>(IEnumerable<T> list) 
    {
        if (list.Count() < 2)
            return list;
        return list.OrderBy(x => Random.value);
    }

    protected static void CheckNull<T>(T val, string name)
    {
        if (val == null)
        {
            Debug.LogError("ExerciseContent:CheckNull\t" + name + "\tis null");
        }
    }

    //static public void TestingMethods()
    //{
    //    List<float> floats = new List<float>();
    //    Shuffle(floats).ToList();
    //}


    //https://docs.unity3d.com/ScriptReference/Resources.Load.html
    public static T LoadFromResources<T>(string path) where T : Object
    {
        var val = Resources.Load<T>(path);
        CheckNull(val, path);
        return val;
    }


    /// JSON
    public static JsonSerializerSettings IgnoreSerializeTagSettings(bool prettyPrint = false)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new IgnoreSerializeTagContractResolver(),
            Formatting = prettyPrint ? Formatting.Indented : Formatting.None,
        };
        return settings;
    }


    public static T LoadJsonFromResources<T>(string path, bool IgnoreSerializeTag = true)
    {
        //Resources doesnt need extension
        if (path.EndsWith(".json"))
            path.Replace(".json","");

        var jsonTextFile = Resources.Load<TextAsset>(path);
        CheckNull(jsonTextFile, path);
        if (jsonTextFile == null) return default(T);

        T val;
        if (IgnoreSerializeTag)
            val = JsonConvert.DeserializeObject<T>(jsonTextFile.text, IgnoreSerializeTagSettings());
        else
            val = JsonUtility.FromJson<T>(jsonTextFile.text);

        CheckNull(val, jsonTextFile.text);
        return val;
    }

    /// <summary>
    /// path: Only after Assets/Resources/ path.json
    /// </summary>
    public static void SaveAsJsonToResources<T>(T val, string path, bool prettyPrint = false, bool IgnoreSerializeTag = true)
    {
//#if UNITY_EDITOR
        path = "Assets/Resources/" + path;
//#endif
//#if UNITY_STANDALONE
//        //You cannot add a subfolder, at least it does not work for them:
//        //(https://discussions.unity.com/t/saving-a-json-file-in-resource-folder/109463/2)
//        //Removing subfolders
//        string filename = path.Split("/").Last();
//        path = "MyGame_Data/Resources/" + filename;
//#endif
        if (!path.EndsWith(".json"))
            path += ".json";

        string data = "";
        if (IgnoreSerializeTag)
            data = JsonConvert.SerializeObject(val, IgnoreSerializeTagSettings(prettyPrint));
        else
            data = JsonUtility.ToJson(val, prettyPrint);

        File.WriteAllText(path, data);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
# endif
    }

}