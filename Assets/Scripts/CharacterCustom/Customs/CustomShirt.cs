using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShirt : MonoBehaviour
{
    [field: SerializeField]
    public Character.EShirt Shirt {get; private set;}
    
    public CustomShirt() {}
    public CustomShirt(Character.EShirt shirt)
    {
        Shirt = shirt;
    }
}