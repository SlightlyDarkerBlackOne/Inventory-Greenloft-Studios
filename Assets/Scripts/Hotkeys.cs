using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pomocu Taba otvara i zatvara OdabirEntiteta prozor
public class Hotkeys : MonoBehaviour
{
    [SerializeField] private GameObject chooseEntitetPanel;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            chooseEntitetPanel.SetActive(!chooseEntitetPanel.activeSelf);
        }
    }
}
