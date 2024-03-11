using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CustomBackground : MonoBehaviour
{
    RawImage image;
    [field:SerializeField]
    public Theme.EBackground Id { get; protected set; }

    protected virtual void Start()
    {
        image = GetComponent<RawImage>();
    }

    public void ChangeBackground(Theme.EBackground id)
    {
        Id = id;
        if (image != null)
            image.texture = GameManager.Instance.GetBackgrounds()[id].Texture;
    }
}
