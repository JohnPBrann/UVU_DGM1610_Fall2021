using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject startScreen;
    [Header ("HUD")]
    public Transform healthBarFill;
    public GameObject hud;
    public TextMeshProUGUI timer;
        private float startTime;
     [Header ("Pause Menu")]
    public GameObject pauseMenu;

     [Header ("Death Screen")]
    public GameObject deathScreen;
     [Header ("Win Screen")]
    public GameObject winScreen;
    public TextMeshProUGUI finalTime;



     private string sceneName;


    public static GameUI instance;
    // Start is called before the first frame update
    void Start()
    {
        sceneName =   SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        instance = this;
        gameManager = GetComponent<GameManager>();
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        Death();
        
        ClickToStart();
        if (gameManager.isPaused == true || gameManager.playerIsDead == true || gameManager.hasWon == true)
        {
            hud.SetActive(false);
        }
        else
         {
            hud.SetActive(true);
        }
        Win();
    }

     public void UpdateHealthBar(int curHp, int maxHp)
    {
        Slider slider = healthBarFill.GetComponent<Slider>();
        slider.maxValue = maxHp;
        slider.value = curHp;
    }

       public void TogglePauseMenu (bool paused)
   {
       pauseMenu.SetActive(paused);
   }

    public void OnResumeButton()
   {
        GameManager.instance.TogglePauseGame();
   }

    public void  OnRestartButton()
    {
        SceneManager.LoadScene(sceneName);
    }
     public void  OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }


    void ClickToStart()
    {
        if(gameManager.gameStart == false)
        {
            startScreen.SetActive(true);
            hud.SetActive(false);
        }
        if(gameManager.gameStart == true)
        {
            startScreen.SetActive(false);
        }
    }
    void Death()
    {
        if(gameManager.playerIsDead == true)
        {
            deathScreen.SetActive(true);
        }
        else
        {
            deathScreen.SetActive(false);
        }
    }
    void Win()
    {
        if(gameManager.hasWon == true)
        {
            winScreen.SetActive(true);
            float t = Time.time - startTime;
            string minutes = ((int) t /60).ToString();
            string seconds = (t % 60).ToString("f2");
            
            finalTime.text = "Time:" + minutes + ":" + seconds;
        }
        else
        {
            winScreen.SetActive(false);
        }
        
    }
    void Timer()
        {
            float t = Time.time - startTime;
            string minutes = ((int) t /60).ToString();
            string seconds = (t % 60).ToString("f2");
            
            timer.text = minutes + ":" + seconds;
        }
        
}
