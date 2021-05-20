using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New FoodType")]
public class FoodType : ScriptableObject
{
    public Sprite Sprite;
    public Color Tint = new Color(1,1,1,1);
    public Diet Diet;
    [TextArea(3,20)]
    public string Info;
}
