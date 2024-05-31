using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseActivity : BaseActivity
{
    [SerializeField]
    protected OriginObj BubbleOrigin;
    [SerializeField]
    protected float TimeTraslation = 0.33f;

    protected override void Awake()
    {
        base.Awake();
        Activity = EActivity.Choose;
    }

    protected override void Start()
    {
        BubbleOrigin.Load();
        base.Start();
    }

    protected override void LoadEmotionButtons()
    {
        if (BubbleOrigin.Enabled())
        {
            BubbleOrigin.Img.canvasRenderer.SetAlpha(0.0f);
            BubbleOrigin.Img.CrossFadeAlpha(1.0f, TimeTraslation, true);

            BubbleOrigin.GO.transform.position = BubbleOrigin.HidePosition;
            LeanTween.moveY(BubbleOrigin.GO, BubbleOrigin.AimPosition.y, TimeTraslation).setOnComplete(SpawnEmotionButtons);
        }
        else
        {
            SpawnEmotionButtons();
        }
    }

    protected void SpawnEmotionButtons()
    {
        base.LoadEmotionButtons();

        if (BubbleOrigin.Enabled())
        {
            foreach(GameObject button in InstantiateButtons)
            {
                if (button != null)
                    button.transform.position = BubbleOrigin.AimPositionOffset;
            }

            if (BubbleOrigin.Img != null)
                BubbleOrigin.Img.CrossFadeAlpha(0.0f, TimeTraslation, true);
            LeanTween.moveY(BubbleOrigin.GO, 0.0f, TimeTraslation);
        }
    }

    [System.Serializable]
    protected struct OriginObj
    {
        [field:SerializeField]
        public GameObject GO { get; private set; }
        public Image Img { get; private set; }

        [SerializeField]
        private Vector3 Offset;
        public Vector3 AimPosition { get; private set; }
        public Vector3 AimPositionOffset { get; private set; }
        public Vector3 HidePosition { get; private set; }
        bool Loaded;

        public void Load()
        {
            if (GO == null) return;

            Transform BubbleTrasform = GO.transform;
            HidePosition = new Vector3(BubbleTrasform.position.x, 0.0f);
            AimPosition = BubbleTrasform.position;
            AimPositionOffset = BubbleTrasform.position + Offset;
            Img = BubbleTrasform.GetComponent<Image>();
            Loaded = true;
        }

        public bool Enabled()
        {
            return Loaded && GO != null;
        }
    }
}
