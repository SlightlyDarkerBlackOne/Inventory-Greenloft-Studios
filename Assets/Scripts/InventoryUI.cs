using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform slotsContainer;
    [SerializeField] private Transform slot;
    [SerializeField] private GameObject chooseFoodPanel;
    [SerializeField] private GameObject chooseEntitetPanel;
    private Player player;

    private Canvas canvas;
    public enum Category
    {
        All,
        Food,
        Clothes,
        Furniture,
    }

    private Category category;
    private Transform categoryButtons;

    void Start() {
        //Find the object you're looking for
        GameObject tempObject = GameObject.Find("Canvas");
        if (tempObject != null) {
            //If we found the object , get the Canvas component from it.
            canvas = tempObject.GetComponent<Canvas>();
            if (canvas == null) {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }
        category = Category.All;
        categoryButtons = transform.Find("Categories");
        categoryButtons.Find("All").transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
    }

    private void Awake() {
        slotsContainer = transform.Find("Slots");
    }
    public void SetPlayer(Player player) {
        this.player = player;
    }
    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItemsByCategory();
    }


    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItemsByCategory();
    }

    private void RefreshInventoryItemsByCategory() {
        foreach (Transform child in slotsContainer) {
            if (child == slot) continue;
            Destroy(child.gameObject);
        }
        if (category == Category.All) {
            ShowItemSlots(Item.ItemType.None);
        } else if (category == Category.Food) {
            ShowItemSlots(Item.ItemType.Food);
        } else if (category == Category.Clothes) {
            ShowItemSlots(Item.ItemType.Clothes);
        } else if (category == Category.Furniture) {
            ShowItemSlots(Item.ItemType.Furniture);
        }
        
    }

    //Instancira slotove ovisno o kreiranim ScriptableObject tipovima itema
    //Provjerava Lijevi klik za funkcionalnosti itema
    private void ShowItemSlots(Item.ItemType itemType) {
        foreach (Item item in inventory.GetItems()) {
            if (item.itemScriptableObject.itemType == itemType || category == Category.All) {
                GameObject slotGO = Instantiate(slot, slotsContainer).gameObject;
                slotGO.SetActive(true);

                slotGO.GetComponent<Button_UI>().ClickFunc = () => {
                    AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound2);

                    if (item.itemScriptableObject is Food) {
                        var chooseObject = Instantiate(chooseFoodPanel, slotGO.transform.position, Quaternion.identity);
                        chooseObject.transform.parent = canvas.transform;
                        chooseObject.GetComponent<ChooseFoodAmount>().SetItem(item);
                    } else if(item.itemScriptableObject is Clothes) {
                        chooseEntitetPanel.SetActive(true);
                        chooseEntitetPanel.GetComponent<OdabirEntiteta>().SetItem(item);
                    } else if(item.itemScriptableObject is Furniture) {
                        chooseEntitetPanel.SetActive(true);
                        chooseEntitetPanel.GetComponent<OdabirEntiteta>().SetItem(item);
                    }
                };

                Image image = slotGO.transform.Find("image").GetComponent<Image>();
                image.sprite = item.GetSprite();

                TextMeshProUGUI text = slotGO.transform.Find("text").GetComponent<TextMeshProUGUI>();
                if (item.amount > 1) {
                    text.SetText(item.amount.ToString());
                } else {
                    text.SetText("");
                }
            }
        }
    }

    public void ActiveCategoryAll() {
        category = Category.All;
        ScaleButtons();
        RefreshInventoryItemsByCategory();
    }
    public void ActiveCategoryFood() {
        category = Category.Food;
        ScaleButtons();
        RefreshInventoryItemsByCategory();
    }
    public void ActiveCategoryClothes() {
        category = Category.Clothes;
        ScaleButtons();
        RefreshInventoryItemsByCategory();
    }
    public void ActiveCategoryFurniture() {
        category = Category.Furniture;
        ScaleButtons();
        RefreshInventoryItemsByCategory();
    }
    //Povecava veličinu aktivnog gumba kategorije
    private void ScaleButtons() {
        foreach (Transform button in categoryButtons) {
            if(category.ToString() == button.name) {
                button.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
            } else {
                button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound5);
    }
    public List<Item> FilterByFood() {
        List<Item> filteredList = new List<Item>();
        foreach (Item item in inventory.GetItems()) {
            if(item.itemScriptableObject is Food) {
                filteredList.Add(item);
            }
        }
        return filteredList;
    }
    public List<Item> FilterByClothes() {
        List<Item> filteredList = new List<Item>();
        foreach (Item item in inventory.GetItems()) {
            if (item.itemScriptableObject is Clothes) {
                filteredList.Add(item);
            }
        }
        return filteredList;
    }
    public List<Item> FilterByFurniture() {
        List<Item> filteredList = new List<Item>();
        foreach (Item item in inventory.GetItems()) {
            if (item.itemScriptableObject is Furniture) {
                filteredList.Add(item);
            }
        }
        return filteredList;
    }
}
