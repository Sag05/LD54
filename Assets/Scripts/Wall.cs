using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    public int hipoints;
    public TextMeshProUGUI hpText;
    public GameObject gameManager;
    Economy econScript;

    GameObject existingScreen;
    public GameObject gameOverScreen;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        econScript = gameManager.GetComponent<Economy>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "Wall HP: " + hipoints.ToString();
        if(hipoints <= 0)
        {
            existingScreen =  Instantiate(gameOverScreen, canvas.transform);
            existingScreen.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + econScript.score;
            //Open GameOver Menu

            Destroy(gameObject);
        }
    }
}
