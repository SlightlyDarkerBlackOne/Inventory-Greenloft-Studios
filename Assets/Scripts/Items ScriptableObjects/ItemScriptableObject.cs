using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public Item.ItemType itemType;
    public string itemName;
    public Sprite itemSprite;
}
