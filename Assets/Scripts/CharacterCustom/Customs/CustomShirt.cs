using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShirt : CustomPart
{
    [field: SerializeField]
    public Character.EShirt Shirt {get; private set;}
    
    public CustomShirt() {}
    public CustomShirt(Character.EShirt shirt)
    {
        Shirt = shirt;
    }
    
    protected override void Start()
    {
        toggle.isOn = Shirt == Customization.GetShirt();
    }

    protected override void SetPart()
    {
        Customization.SetShirt(Shirt);
    }
}