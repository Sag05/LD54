using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int damage;

    void Update()
    {
        Rect rect = new Rect(-10,-10,20,20);

        if (!rect.Contains(gameObject.transform.position))
        {
            Debug.Log("destroyed shell");
            Destroy(gameObject);
        }    
    }
}
