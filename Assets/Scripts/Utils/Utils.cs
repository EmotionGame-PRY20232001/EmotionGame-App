using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    protected static void CheckNull<T>(T val, string name)
    {
        if (val == null)
        {
            Debug.LogError("ExerciseContent:CheckNull\t" + name + "\tis null");
        }
    }
}