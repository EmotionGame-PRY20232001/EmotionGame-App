using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionsSelected : MonoBehaviour
{
    [SerializeField]
    protected Transform Container;
    [SerializeField]
    protected EmotionObject EmotionPrefab;

    public void Start()
    {
        if (Container == null)
            Container = gameObject.GetComponent<Transform>();

        LoadEmotions();
    }

    protected void LoadEmotions()
    {
        if (GameManager.Instance == null || Container == null) return;
        
        foreach (Exercise.EEmotion emo in GameManager.Instance.SelectedEmotions)
        {
            var emotionObj = Instantiate(EmotionPrefab, Container);
            emotionObj.SetEmotion(emo, 0);
        }
    }
}