using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public List<ItemScriptableObject> items;
    #region Singleton
    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

}
