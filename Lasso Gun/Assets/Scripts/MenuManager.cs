using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
     public bool controlScreen;
     public GameObject controls;
    public GameObject mainMenu;
void Start()
{
     controlScreen = false;
}
void Update()
{
     if(controlScreen == true)
     {
          controls.SetActive(true);
         mainMenu.SetActive(false);
     }
     else
     {
         controls.SetActive(false);
         mainMenu.SetActive(true);
     }

}

 public void PlayButton()
     {
        SceneManager.LoadScene("Level1");
     } 
   public void ToggleControls()
     {
        controlScreen = !controlScreen;
     }
     public void QuitButton()
     {
          Application.Quit();
     }
   
}
