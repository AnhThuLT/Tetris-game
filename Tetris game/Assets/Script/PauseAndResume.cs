using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAndResume : MonoBehaviour
{
    public Sprite RedPlaybutton;
    public Sprite GreenPlaybutton;
    public Button ButtonPlay;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClicktoButton()
    {
        if(isPaused==false)
        {
            PauseGame();
            isPaused = true;
            ChangeStopButton();
        }
        else
        {
            ResumeGame();
            isPaused = false;
            ChangePlayButton();
        }
    }
    
    void ChangeStopButton()
    {
        ButtonPlay.image.sprite = RedPlaybutton;
    }
    void ChangePlayButton()
    {
        ButtonPlay.image.sprite = GreenPlaybutton;
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
