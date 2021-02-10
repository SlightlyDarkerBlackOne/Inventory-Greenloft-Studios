using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class Entitet : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultSprite;

    private Item item;
    private SpriteRenderer renderer;
    private void Start() {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void RemoveItem() {
        renderer.sprite = defaultSprite;
        if (item != null) {
            Player.Instance.GetInventory().AddItem(item);
        }
        item = null;
    }
    public void SetItem(Item item) {
        this.item = item;
    }
    public Item GetItem() {
        return item;
    }
}
