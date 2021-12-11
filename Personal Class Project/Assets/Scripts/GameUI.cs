using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public GameManager gameManager;
    private Scene scene;
    public GameObject pauseMenu;

   public void Awake()
   {
       scene = SceneManager.GetActiveScene();
       Debug.Log("Level Name: " + scene.name);
       instance = this;
       pauseMenu.SetActive(false);
   }
  
   public void TogglePauseMenu ()
   {
       if (gameManager.gamePaused==true) 
       {
           pauseMenu.SetActive(true);
       }
       else
       {
           pauseMenu.SetActive(false);
       }
       
   }

   public void OnResumeButton()
   {
         if (gameManager.gamePaused==true) 
       {
           gameManager.GameUnPause();
       }
   }

     public void  OnRestartButton()
     {
         SceneManager.LoadScene("Level_1");
     }
     public void  OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
