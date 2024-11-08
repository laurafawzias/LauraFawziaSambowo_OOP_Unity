using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    public void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
            Instance = this;
            
            LevelManager = GetComponentInChildren<LevelManager>();

            DontDestroyOnLoad(gameObject); 
            DontDestroyOnLoad(GameObject.Find("Camera"));
    }
}

