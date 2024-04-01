using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ThemeColorFilled : MonoBehaviour
{
    public Theme.CustomPalette DefaultTheme;
    //PrimaryColor | 96D0FF 150, 208, 255, 255 | 4E5A60
    //AccentColor | FEE456 254, 228, 86, 255 | 7C4E39
    //PaperColor | FFF7EE 255, 247, 238, 255
    //DangerColor | FFC1BF 255, 193, 191, 255
    
    [SerializeField]
    protected Theme.ETypes FillType;

    protected Color ColorFill;
    protected Color ColorContrast;

    [SerializeField]
    protected List<Graphic> GraphicToFill;
    [SerializeField]
    protected List<Graphic> GraphicToContrast;

    protected void Awake()
    {
        SetColors();
        Fill();
    }
    
    protected void SetColors()
    {
        //TODO: Color contrast
        switch (FillType)
        {
            case Theme.ETypes.Primary:
                ColorFill = DefaultTheme.Primary.Background;
                ColorContrast = DefaultTheme.Primary.Text;
            break;
            case Theme.ETypes.Accent:
                ColorFill = DefaultTheme.Accent.Background;
                ColorContrast = DefaultTheme.Accent.Text;
            break;
            case Theme.ETypes.Danger:
                ColorFill = DefaultTheme.Danger.Background;
                ColorContrast = DefaultTheme.Danger.Text;
            break;
            default :
                ColorFill = DefaultTheme.Paper.Background;
                ColorContrast = DefaultTheme.Paper.Text;
            break;
        }
    }

    protected void Fill()
    {
        if (GraphicToFill.Count > 0)
        {
            foreach(Graphic gh in GraphicToFill)
            {
                if (gh != null)
                    gh.color = ColorFill;
            }
        }
        
        if (GraphicToContrast.Count > 0)
        {
            foreach(Graphic gh in GraphicToContrast)
            {
                if (gh != null)
                    gh.color = ColorContrast;
            }
        }
    }
}
