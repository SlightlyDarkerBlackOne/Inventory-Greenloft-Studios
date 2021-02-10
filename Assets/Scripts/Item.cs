using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
   public enum ItemType
    {
        None,
        Clothes,
        Food,
        Furniture,
    }

    public ItemScriptableObject itemScriptableObject;
    public int amount;

    public Sprite GetSprite() {
        return itemScriptableObject.itemSprite;
    }

    public bool IsStackable() {
        switch (itemScriptableObject.itemType) {
            default:
            case ItemType.Clothes:
            case ItemType.Food:
                return true;
            case ItemType.Furniture:
                return false;
        }
    }

    public override string ToString() {
        return itemScriptableObject.itemName;
    }
}
