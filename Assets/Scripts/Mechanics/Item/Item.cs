using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Lore { get; private set; }
    public Sprite sprite;

    public virtual void Use() {}
    public virtual void PickUp() {}
}
