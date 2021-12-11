using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public bool gamePaused;
    
    public static GameManager instance;
    void Awake() 
    {
     instance = this;    
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
            TogglePauseGame();
    
    }
    public void TogglePauseGame()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                if(gamePaused == false)
                {
                GamePause();
                }
            }
                else
                {
                 GameUnPause();
                }
            
        } 
    }
    void GamePause()
        {
             gamePaused = true;
             Cursor.visible = true;
             Cursor.lockState = CursorLockMode.None;
             Time.timeScale = 0;
              GameUI.instance.TogglePauseMenu();
              Debug.Log(Time.timeScale);
              Debug.Log(Cursor.lockState);
              Debug.Log(Cursor.visible);
        }
    public void GameUnPause()
        {
             gamePaused = false;
             Cursor.visible = false;
             Cursor.lockState = CursorLockMode.Locked;
              Time.timeScale = 1;
             GameUI.instance.TogglePauseMenu();
              Debug.Log(Time.timeScale);
              Debug.Log(Cursor.lockState);
              Debug.Log(Cursor.visible);  
        }
}
