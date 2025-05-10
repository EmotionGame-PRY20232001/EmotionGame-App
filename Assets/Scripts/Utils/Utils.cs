using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
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
    static public IEnumerable<T> Shuffle<G,T>(G list) where G : IEnumerable<T>
    {
        if (list.Count() < 2)
            return list;
        return list.OrderBy(x => Random.value).AsEnumerable();
    }
    static public List<T> Shuffle<T>(List<T> list)
    {
        return Shuffle<List<T>, T>(list).ToList();
    }

    protected static void CheckNull<T>(T val, string name)
    {
        if (val == null)
        {
            Debug.LogWarning("ExerciseContent:CheckNull\t" + name + "\tis null");
        }
    }

    //static public void TestingMethods()
    //{
    //    List<float> floats = new List<float>();
    //    Shuffle(floats).ToList();
    //}

    //https://stackoverflow.com/questions/163162/can-you-call-directory-getfiles-with-multiple-filters
    //extensions = (new[] { ".png", ".jpg" })
    public static string[] GetFilesFromResources(string folderPath, string[] extensions)
    {
        string RES_PATH = Application.dataPath + "/Resources/";
        string[] files = Directory.GetFiles(RES_PATH + folderPath, "*.*", SearchOption.AllDirectories)
                                    .Where(fn =>
                                        extensions.Any(x =>
                                            fn.EndsWith(x, System.StringComparison.OrdinalIgnoreCase)
                                        )
                                    ).ToArray();

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace(RES_PATH, "")
                                .Replace(Path.GetExtension(files[i]), "")
                                .Replace("\\", "/");
        }

        return files;
    }

    public static string[] GetFilesFromResources<T>(string folderPath, string[] extensions) where T : Object
    {
        string[] files = GetFilesFromResources(folderPath, extensions);

        List<string> filesLst = files.ToList();
        foreach (string file in filesLst)
        {
            T obj = Resources.Load<T>(file);
            if (obj == null)
                filesLst.Remove(file);
        }
        return filesLst.ToArray();
    }

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

    public static string SanitizeFileName(string name, string replacement = "_")
    {
        // Remove invalid characters
        string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        string invalidReStr = $"[{invalidChars}]+";
        string sanitized = Regex.Replace(name, invalidReStr, replacement);

        // Trim spaces and dots at start/end
        sanitized = sanitized.Trim().TrimEnd('.');

        // Avoid reserved names (Windows)
        string[] reservedNames = {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };

        foreach (string reserved in reservedNames)
        {
            if (sanitized.Equals(reserved, System.StringComparison.OrdinalIgnoreCase))
            {
                sanitized = $"_{sanitized}";
                break;
            }
        }

        return sanitized;
    }

    /// <summary>
    /// Returns [playerNameSanitized]/[playerNameSanitized][_name?][_date-hour].[extension]
    /// </summary>
    /// <param name="extension">whitout dot</param>
    /// <param name="detName">detailed name after player name</param>
    public static string GetDefaultFilePathName(string folder = "", string extension = "csv", string detName = "")
    {
        var gm = GameManager.Instance;
        string playerName = gm == null ? "Player" : gm.GetCurrentPlayer().Name;
        playerName.Trim();
        playerName.Replace(" ", "-");
        playerName = SanitizeFileName(playerName, "-");

        string filePath = Application.persistentDataPath;
        if (folder != "")
        {
            filePath = Path.Combine(Application.persistentDataPath, folder);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                Debug.Log($"Created folder: {filePath}");
            }
        }

        filePath = Path.Combine(filePath, playerName);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            Debug.Log($"Created folder: {filePath}");
        }

        System.DateTime dateExported = System.DateTime.Now;
        string customDate = "yyyymmdd-HHmmssff";
        customDate = dateExported.ToString(customDate);

        string fileName = playerName +
                          "_" + customDate +
                          (detName == "" ? "" : "_" + detName) +
                          "." + extension;
        filePath = Path.Combine(filePath, fileName);

        return filePath;
    }

}