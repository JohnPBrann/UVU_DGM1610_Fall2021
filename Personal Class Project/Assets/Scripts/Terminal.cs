using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Terminal : MonoBehaviour
{ 
    public GameObject terminal;
    public GameObject receiver;
    public GameObject glassCase;
    bool isUp;
    float speed = 3;
    [Header ("Password Settings")]
    public bool inputFieldVisible;
    public GameObject inputField;
    public bool passwordRight;
    public  string correctPassword;
    public TextMeshProUGUI messageText; 
    public GameObject message; 
  
   [Header ("SoundFX")]
    public AudioClip glassSound;
    private AudioSource audioSource;
    

    void Awake()
    {
        audioSource = terminal.GetComponent<AudioSource>();
        inputField.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            inputFieldVisible = false;
            message.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        inputFieldVisible = false;
        passwordRight = false;
    }

   
    void Update()
    { 
        if(isUp == true)
        {
            MoveUp();
            PasswordEntry();
        }
        else
        {
           MoveDown();
        }
        if(inputFieldVisible == true)
        {
            inputField.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            if(Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePassword();
        }
        }
        else
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmPassword();
        }
       if(passwordRight==true)
       {
            if(isUp == true) 
        {
           ClosePassword();
           isUp = false;
        }
       }
    }

    void ClosePassword()
    {
         inputField.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            inputFieldVisible = false;
    }
    void MoveUp()
    {
        Vector3 newPos = new Vector3(transform.position.x, -83.184f, transform.position.z);
        transform.position =  Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }
    void MoveDown()
    {
        Vector3 oldPos = new Vector3(transform.position.x, -84.01f, transform.position.z);
        transform.position =  Vector3.Lerp(transform.position, oldPos, Time.deltaTime * speed);
        
    }
    void PasswordEntry()
    {
        if(isUp == true)
        {
            if(Input.GetKeyDown("mouse 0"))
            {
                inputFieldVisible = true;
            }
        }
    }
    void ConfirmPassword()
    {
         string password = inputField.GetComponent<TMP_InputField>().text;
        
       if(password == correctPassword)
        {
            passwordRight = true;
            Debug.Log("Ya! That's it!");
            inputFieldVisible = false;
            Destroy(glassCase);
        }
        else
        {
            passwordRight = false;
            Debug.Log("Huh? You dumb?");
        }
        Message();
    }
    void Message()
    {
        message.SetActive(true);
        messageText.text = passwordRight == true ? "Ya! That's it!" : "Huh? You dumb?";
        StartCoroutine(Fade(messageText));
         
    }
   IEnumerator Fade(TextMeshProUGUI messageText)
    {
        Debug.Log("Coroutine started");
        {
            float fadeTime = 3.0f;
            float waitTime = 0f;
            while (waitTime < 1)
            {
                messageText.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.black, Color.clear, waitTime));
                yield return null;
                waitTime += Time.deltaTime / fadeTime;
            }
        Debug.Log("Coroutine ended");
        message.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other) //checks if player is within collider
    {
        if(other.gameObject.tag == "Player")
        {
            if(passwordRight==false)
            {
                isUp = true;
            }
        }
    }
    void OnTriggerExit (Collider other)
    {
         if(other.gameObject.tag == "Player")
        {
            isUp = false;
        }
    }
}
