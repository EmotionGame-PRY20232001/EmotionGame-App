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

    protected override bool IsSameCustomActive(Character.Custom custom)
    {
        // Debug.Log("Shirt " + Shirt + " | " + Customization.GetShirt() );
        // Customization.GetShirt()
        return Shirt == custom.Shirt;
    }

    protected override void ChangeSelection()
    {
        Customization?.SetShirt(Shirt);
    }
}