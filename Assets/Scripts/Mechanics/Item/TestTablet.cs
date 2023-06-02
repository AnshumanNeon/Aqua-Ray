using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TestTablet")]
public class TestTablet : Item
{
    public override void Use()
    {
        Debug.Log("yes");
    }
}
