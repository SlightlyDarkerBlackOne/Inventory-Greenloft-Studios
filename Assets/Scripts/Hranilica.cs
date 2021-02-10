using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Svakih nekoliko sekundi potroši jedan food
public class Hranilica : MonoBehaviour
{
    public int currentfoodAmount;
    private int maxFoodAmount = 50;

    private float eatTime = 3f;
    public float startEatTime = 2f;

    [SerializeField]
    private TextMeshPro foodText;
    [SerializeField]
    private SpriteRenderer lastFoodImageSR;
    #region Singleton
    public static Hranilica Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion
    private void Update() {
        eatTime -= Time.deltaTime;
        if(eatTime <= 0) {
            TakeOneFood();
            eatTime = startEatTime;
        }
        foodText.text = currentfoodAmount.ToString();
    }
    public void Feed(Item item, int foodAmount) {
        currentfoodAmount += foodAmount;
        if(currentfoodAmount >= maxFoodAmount) {
            currentfoodAmount = maxFoodAmount;
        }
        lastFoodImageSR.sprite = item.itemScriptableObject.itemSprite;

        for(int i = 0; i < foodAmount; i++){
            Player.Instance.GetInventory().RemoveItem(item);
        }
    }

    private void TakeOneFood() {
        currentfoodAmount--;
        if(currentfoodAmount < 0) {
            currentfoodAmount = 0;
        }
    }
}
