using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//[Table("ExerciseContent")]
public class ExerciseContent
{
    //[AutoIncrement, PrimaryKey]
    //public int Id { get; set; }
    //[Indexed(Name = "Content", Order = 1, Unique = true)]
    //public string Face { get; set; }
    //[Indexed(Name = "Content", Order = 2, Unique = true)]
    //public string Text { get; set; }
    //Exercise.EEmotion CorrectEmotion { get; set; }

    public enum EValueType : uint { FacePhoto, Text };

    //[System.Serializable]
    //public struct Unit {
    //    // id is order on list
    //    public string Value;
    //    // Value can be path or text
    //    public EValueType Type;

    //    public Sprite LoadFacePhoto()
    //    {
    //        if (Type != EValueType.FacePhoto) return null;
    //        var sprite = Utils.LoadFromResources<Sprite>("Faces/" + Value);
    //        return sprite;
    //    }
    //}

    // For now keep using list
    public List<Sprite> Faces;
    public List<string> Contexts;

    public Sprite GetFacePhotoFromId(string id)
    {
        IdStruct idStruct = IdStruct.FromString(id);
        if (idStruct.type != EValueType.FacePhoto) return null;
        return Faces[idStruct.order];
    }

    public string GetContextsFromId(string id)
    {
        IdStruct idStruct = IdStruct.FromString(id);
        if (idStruct.type != EValueType.Text) return "";
        return Contexts[idStruct.order];
    }


    [System.Serializable]
    public struct IdStruct
    {
        public EValueType type { get; private set; }
        public int order { get; private set; }

        public static string ToString(EValueType type, int order)
        {
            const string separator = "_";
            string id = ((uint)type).ToString() + separator + order.ToString();
            return id;
        }

        public static IdStruct FromString(string id)
        {
            IdStruct idSt = new IdStruct();
            if (id.Length < 2) return idSt;

            string idStr = id.ToString();
            const string separator = "_";
            string[] parts = idStr.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return idSt;

            idSt.type = (EValueType)uint.Parse(parts[0]);
            idSt.order = int.Parse(parts[1]);
            return idSt;
        }
    }
}
