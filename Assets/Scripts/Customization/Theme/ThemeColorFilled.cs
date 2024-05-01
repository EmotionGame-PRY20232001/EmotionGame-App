using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class ThemeColorFilled : MonoBehaviour
{
    protected Theme.ETheme CurrentTheme = Theme.ETheme.Neutral;
    public Theme.CustomPalette DefaultTheme;
    //PrimaryColor | 96D0FF 150, 208, 255, 255 | 4E5A60
    //AccentColor | FEE456 254, 228, 86, 255 | 7C4E39
    //PaperColor | FFF7EE 255, 247, 238, 255
    //DangerColor | FFC1BF 255, 193, 191, 255

    // Settings
    [SerializeField]
    protected Theme.ETypes FillType = Theme.ETypes.Primary;
    [SerializeField]
    protected Theme.ELightness Lightness = Theme.ELightness.Main;

    [SerializeField]
    [Tooltip("Only used if FillType is Paper")]
    protected float PaperAlphaFill = 0.5f;

    [SerializeField]
    protected Color ColorFill;
    [SerializeField]
    protected Color ColorContrast;

    [SerializeField]
    protected List<Graphic> GraphicsToFill;
    [SerializeField]
    protected List<Graphic> GraphicsToContrast;

    protected void Start()
    {
        LoadDefault();
        UpdateColors();
        UpdateFillType();
    }

    public void LoadPlayerTheme(Theme.EBackground bgId)
    {
        LoadByBgId(bgId);
        UpdateColors();
        UpdateFillType();
    }

    protected void LoadByBgId(Theme.EBackground bgId)
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        CurrentTheme = gm.GetBackgrounds()[bgId].Theme;
        DefaultTheme = gm.ThemeCustom.Themes[CurrentTheme];
    }

    protected void LoadDefault()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        if (gm.IsPlayerActive())
        {
            var player = gm.GetCurrentPlayer();
            Theme.EBackground currentBg = (Theme.EBackground)player.BackgroundId;
            LoadByBgId(currentBg);
        }
        else
        {
            DefaultTheme = gm.ThemeCustom.Themes[CurrentTheme];
        }
    }

    protected void UpdateColors()
    {
        switch (FillType)
        {
            case Theme.ETypes.Primary:
                switch (Lightness)
                {
                    case Theme.ELightness.Main:
                        ColorFill = DefaultTheme.Primary.Main.Background;
                        ColorContrast = DefaultTheme.Primary.Main.Shape;
                        break;
                    case Theme.ELightness.Dark:
                        ColorFill = DefaultTheme.Primary.Dark.Background;
                        ColorContrast = DefaultTheme.Primary.Dark.Shape;
                        break;
                    case Theme.ELightness.Light:
                        ColorFill = DefaultTheme.Primary.Light.Background;
                        ColorContrast = DefaultTheme.Primary.Light.Shape;
                        break;
                    case Theme.ELightness.Disabled:
                        ColorFill = DefaultTheme.Primary.Disabled.Background;
                        ColorContrast = DefaultTheme.Primary.Disabled.Shape;
                        break;
                }
            break;
            case Theme.ETypes.Accent:
                switch (Lightness)
                {
                    case Theme.ELightness.Main:
                        ColorFill = DefaultTheme.Accent.Main.Background;
                        ColorContrast = DefaultTheme.Accent.Main.Shape;
                        break;
                    case Theme.ELightness.Dark:
                        ColorFill = DefaultTheme.Accent.Dark.Background;
                        ColorContrast = DefaultTheme.Accent.Dark.Shape;
                        break;
                    case Theme.ELightness.Light:
                        ColorFill = DefaultTheme.Accent.Light.Background;
                        ColorContrast = DefaultTheme.Accent.Light.Shape;
                        break;
                    case Theme.ELightness.Disabled:
                        ColorFill = DefaultTheme.Accent.Disabled.Background;
                        ColorContrast = DefaultTheme.Accent.Disabled.Shape;
                        break;
                }
            break;
            case Theme.ETypes.Danger:
                switch (Lightness)
                {
                    case Theme.ELightness.Main:
                        ColorFill = DefaultTheme.Danger.Main.Background;
                        ColorContrast = DefaultTheme.Danger.Main.Shape;
                        break;
                    case Theme.ELightness.Dark:
                        ColorFill = DefaultTheme.Danger.Dark.Background;
                        ColorContrast = DefaultTheme.Danger.Dark.Shape;
                        break;
                    case Theme.ELightness.Light:
                        ColorFill = DefaultTheme.Danger.Light.Background;
                        ColorContrast = DefaultTheme.Danger.Light.Shape;
                        break;
                    case Theme.ELightness.Disabled:
                        ColorFill = DefaultTheme.Danger.Disabled.Background;
                        ColorContrast = DefaultTheme.Danger.Disabled.Shape;
                        break;
                }
            break;
            case Theme.ETypes.Paper:
                switch (Lightness)
                {
                    case Theme.ELightness.Main:
                        ColorFill = DefaultTheme.Paper.Main.Background;
                        ColorContrast = DefaultTheme.Paper.Main.Shape;
                        break;
                    case Theme.ELightness.Dark:
                        ColorFill = DefaultTheme.Paper.Dark.Background;
                        ColorContrast = DefaultTheme.Paper.Dark.Shape;
                        break;
                    case Theme.ELightness.Light:
                        ColorFill = DefaultTheme.Paper.Light.Background;
                        ColorContrast = DefaultTheme.Paper.Light.Shape;
                        break;
                    case Theme.ELightness.Disabled:
                        ColorFill = DefaultTheme.Paper.Disabled.Background;
                        ColorContrast = DefaultTheme.Paper.Disabled.Shape;
                        break;
                }
                ColorFill.a = PaperAlphaFill;
            break;
        }
    }

    protected void UpdateFillGraphic(Color color)
    {
        if (GraphicsToFill.Count >= 0)
        {
            foreach (Graphic graphic in GraphicsToFill)
            {
                if (graphic != null)
                    graphic.color = color;
            }
        }
    }

    protected void UpdateContrastGraphic(Color color)
    {
        if (GraphicsToContrast.Count >= 0)
        {
            foreach (Graphic graphic in GraphicsToContrast)
            {
                if (graphic != null)
                    graphic.color = color;
            }
        }
    }

    protected void UpdateFillType()
    {
        UpdateFillGraphic(ColorFill);
        UpdateContrastGraphic(ColorContrast);
    }

    public void OnLightnessChange(Theme.ELightness newLightness, float time = 1f)
    {
        Color LastColorFill = ColorFill;
        Color LastColorContrast = ColorContrast;

        Lightness = newLightness;
        UpdateColors();

        if (time == 0.0f)
        {
            UpdateFillType();
        }
        else
        {
            LeanTween.value(gameObject, UpdateFillGraphic, LastColorFill, ColorFill, time);
            LeanTween.value(gameObject, UpdateContrastGraphic, LastColorContrast, ColorContrast, time);
        }
    }

    protected void Update()
    {
#if (UNITY_EDITOR)
        if (!Application.isPlaying)
        {
            UpdateColors();
            UpdateFillType();
        }
#endif
    }
}
