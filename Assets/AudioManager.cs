using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource uiSound;
    public AudioSource uiSound2;
    public AudioSource uiSound3;
    public AudioSource uiSound4;
    public AudioSource uiSound5;

    #region Singleton
    public static AudioManager Instance { get; private set; }

    // Use this for initialization
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    public void PlaySound(AudioSource source) {
        source.Play();
    }
}
