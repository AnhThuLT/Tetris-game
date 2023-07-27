using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private float fallTime = 0.8f;
    private int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("fallTime", fallTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
