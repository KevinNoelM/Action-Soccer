using UnityEngine;
using System.Collections;

public class ColourSetter : MonoBehaviour
{
    [SerializeField]
    protected MeshRenderer[] hair, kit, skin, skinExtra;


    public void SetUp(Color hairColour, Color kitColour, Color skinColour, Color skinExtraColour)
    {
        foreach (var item in hair)
        {
            item.material.color = hairColour;
        }
        foreach (var item in kit)
        {
            item.material.color = kitColour;
        }
        foreach (var item in skin)
        {
            item.material.color = skinColour;
        }
        foreach (var item in skinExtra)
        {
            item.material.color = skinExtraColour;
        }
    }
}
