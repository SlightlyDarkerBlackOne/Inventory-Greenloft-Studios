using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class OdabirEntiteta : MonoBehaviour
{
    private Item item;
    private List<GameObject> listaEntiteta = new List<GameObject>();
    [SerializeField]
    private GameObject buttonPrefab;
    private GameObject entiteti;

    private void Start() {
        entiteti = GameObject.Find("Entiteti");
        foreach (Transform child in entiteti.transform) {
            listaEntiteta.Add(child.gameObject);
        }
        InstantiateButtons();
        gameObject.SetActive(false);
    }
    //Instancira buttone
    //Provjerava lijevi klik za dodavanje oznacenog itema na entitet
    //Provjerava desni klik za uklanjanje itema s entiteta
    private void InstantiateButtons() {
        Transform parent = transform.Find("ButtonList");
        
        foreach (GameObject entitet in listaEntiteta) {
            GameObject button = Instantiate(buttonPrefab, parent);
            button.transform.Find("Text").GetComponent<Text>().text = entitet.name;

            button.GetComponent<Button_UI>().ClickFunc = () => {
                if (entitet.GetComponent<Entitet>().GetItem() == null && item != null) {
                    AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound3);
                    entitet.GetComponent<SpriteRenderer>().sprite = item.itemScriptableObject.itemSprite;
                    button.transform.Find("Text").GetComponent<Text>().text = item.itemScriptableObject.itemName;
                    entitet.GetComponent<Entitet>().SetItem(item);
                    Player.Instance.GetInventory().RemoveItem(item);
                    item = null;
                }
            };
            button.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                if(entitet.GetComponent<Entitet>().GetItem() != null) {
                    entitet.GetComponent<Entitet>().RemoveItem();
                    AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound4);

                }
                button.transform.Find("Text").GetComponent<Text>().text = entitet.name;
            };
        }
    }

    public void SetItem(Item item) {
        this.item = item;
    }

    public void CloseWindow() {
        gameObject.SetActive(false);
    }
}
