using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameUI gameUI;
    public static GameManager instance;
    public bool isPaused;
    public bool playerIsDead;
    public bool gameStart;
    private bool gameStarted;
    public bool hasWon = false;
    public PlayerController playerController;
    [Header ("First Stage")]
    public Transform firstWave;
    public GameObject grapplePoint1;
    private bool moveGrapple1 = true;
    private Vector3 oldPosition;
    [Header ("Second Stage")]
    public Transform secondWave;
    public GameObject door;
    [Header ("Stage 3")]
    public Transform enemies;
    public GameObject door1;

    


    void Awake()
    {
        instance = this;
    }
        void Start()
    {   
        gameUI = instance.transform.GetComponent<GameUI>();
        playerController = FindObjectOfType<PlayerController>();
        gameStart = false;
        gameStarted = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        isPaused = true;
        oldPosition = grapplePoint1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if(hasWon == false)
        {
        if(gameStart == true)
        {
            GameStart();
        }
        EventOne();
        if(Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }

        if(firstWave.childCount == 0)
        {
            moveGrapple1 = false;
            SecondWave();
        }
        if(gameStart == false)
        {
            ClickToStart();
        }
        GameUI.instance.UpdateHealthBar(playerController.curHp, playerController.maxHp);
        if(playerController.isDead == true)
        {
            playerIsDead =true;
            if (Input.anyKeyDown)
            {
                gameUI.OnRestartButton();
            }
        }
        else
        {
            playerIsDead = false;
        }
        }
    }

    void ClickToStart()
    {
        if(Input.anyKey)
        {
            gameStart = true;
        }
        
    }
    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused == true ? 0.0f : 1.0f;
        
        //Toggle the pause menu and cursor
        GameUI.instance.TogglePauseMenu(isPaused);
        Cursor.lockState = isPaused == true ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void GameStart()
    {
        if(gameStarted == false)
        {
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        gameStarted = true;
        }
    }

    void EventOne()
    { 
 
        if (moveGrapple1 == true)
        {
            Vector3 newPos = new Vector3(grapplePoint1.transform.position.x, grapplePoint1.transform.position.y +2, grapplePoint1.transform.position.z);
            grapplePoint1.transform.position =  Vector3.Lerp(grapplePoint1.transform.position, newPos, Time.deltaTime * 3);
        }
         if (moveGrapple1 == false)
        {
            grapplePoint1.transform.position =  Vector3.Lerp(grapplePoint1.transform.position, oldPosition, Time.deltaTime * 3);
        }
    }
    void SecondWave()
    {
        secondWave.gameObject.SetActive(true);
        if(secondWave.childCount == 0)
        {
            Door doorScript;
            doorScript = door.GetComponent<Door>();
            doorScript.enabled = true;
        }
    }
    public void ThirdWave()
    {
        enemies.gameObject.SetActive(true);
        if(enemies.childCount == 0)
        {
            Door doorScript;
            doorScript = door1.GetComponent<Door>();
            doorScript.enabled = true;
        }
    }

    public void Win()
    {
        hasWon = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }
}
