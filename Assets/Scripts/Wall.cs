using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int hipoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hipoints <= 0)
        {
            //Open GameOver Menu

            Destroy(gameObject);
        }
    }
}
