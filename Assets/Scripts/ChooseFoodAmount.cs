using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Pomocu OnClick evenata dodaje i oduzima moguc broj hrane
//Submita u hranilicu amount hrane 
public class ChooseFoodAmount : MonoBehaviour
{
    private int amount;
    [SerializeField]
    private TextMeshProUGUI amountText;
    [SerializeField]
    private Image foodImage;

    private Item item;
    #region Singleton
    public static ChooseFoodAmount Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    void Start()
    {
        amount = 0;
    }
    private void Update() {
        amountText.text = amount.ToString();

        if (item.amount <= 0) {
            CloseWindow();
        }
    }
    public void SubmitAmount() {
        if(amount > 0) {
            Hranilica.Instance.Feed(item, amount);
            AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound3);
        }else
            AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound4);
    }
    public void AddAmount() {
        if (item.amount > amount) {
            amount++;
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound);
    }

    public void SubtractAmount() {
        amount--;
        if(amount <= 0) {
            amount = 0;
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.uiSound2);
    }

    public void CloseWindow() {
        Destroy(gameObject);
    }

    public void SetItem(Item item) {
        this.item = item;
        foodImage.sprite = item.itemScriptableObject.itemSprite;
    }
}
