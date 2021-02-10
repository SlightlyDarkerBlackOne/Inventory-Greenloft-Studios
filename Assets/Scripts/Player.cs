using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Inventory inventory;
    [SerializeField]
    InventoryUI inventoryUI;

    #region Singleton
    public static Player Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start() {
        inventory = new Inventory(UseItem);
        inventoryUI.SetInventory(inventory);
        inventoryUI.SetPlayer(this);
    }
    private void UseItem(Item item) {
        switch (item.itemScriptableObject.itemType) {
            case Item.ItemType.Food:
                inventory.RemoveItem(new Item { itemScriptableObject = item.itemScriptableObject,amount = 1 });
                break;
            case Item.ItemType.Clothes:
                break;
            case Item.ItemType.Furniture:
                break;
        }
    }
    public Inventory GetInventory() {
        return inventory;
    }
}
