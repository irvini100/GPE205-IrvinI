using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    int maxPlatform = 0;

    public void GameOver()
    {
        gameOverScreen.Setup(maxPlatform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
