using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class tetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    public AudioClip clear;
    private AudioSource audioSource;

    public AudioClip rotate;

    public AudioClip Scorer;

    private TMP_Text scoreText;

    private TMP_Text levelText;

    public int scoreforovergame = 0;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fallTime = PlayerPrefs.GetFloat("fallTime");    
    }

    // Update is called once per frame


    void Update()
    {
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        levelText = GameObject.Find("Level").GetComponent<TMP_Text>();
        GameOverCheck();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            audioSource.PlayOneShot(rotate);
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            audioSource.PlayOneShot(rotate);
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        } 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rotate
            audioSource.PlayOneShot(rotate);
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),-90);
            }
                
        }


        
        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                CheckForLevelUp();
                
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            previousTime = Time.time;
        } 
    }

    void CheckForLines()
    {
        for(int i=height-1;i>=0;i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                audioSource.PlayOneShot(clear);
                UpdateScore();
            }
        }
    }

    public bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if(grid[j,y]!=null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY < 0  || roundedY >= height)
            {
                return false;
            }
            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
    void LevelUp()
    {
        int temp = PlayerPrefs.GetInt("Level");
        int temp2 = PlayerPrefs.GetInt("score") /10;
        if(temp != temp2)
        {
            SpeedUp();
        }
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("score") /10); 
        levelText.text = PlayerPrefs.GetInt("Level").ToString();
    }
    void SpeedUp()
    {
        PlayerPrefs.SetFloat("fallTime", PlayerPrefs.GetFloat("fallTime") - 0.02f); 
    }
    void CheckForLevelUp()
    {
        LevelUp();
    }

    void UpdateScore()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 5);
        audioSource.PlayOneShot(Scorer);
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        scoreforovergame = PlayerPrefs.GetInt("score");
    }
    void GameOverCheck()
    {
        for (int j = 0; j < width; j++)
        {
            if(grid[j,19]!=null)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
