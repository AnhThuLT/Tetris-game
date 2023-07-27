using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LastGameOverScore : MonoBehaviour
{
    // Start is called before the first frame update
    private int score;
    private TMP_Text scoreText;
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GetTheScore();
    }
    void GetTheScore()
    {
        score = PlayerPrefs.GetInt("score");
        scoreText.text = "Score: " + score;
    }
}
