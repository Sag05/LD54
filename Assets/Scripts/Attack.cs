using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float reload;
    Wall wallScript;
    bool inWallRange = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("colliding with: " + collision.gameObject.name);
            wallScript = collision.gameObject.GetComponent<Wall>();
            inWallRange = true;

            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (inWallRange)
        {
            yield return new WaitForSeconds(reload);
            wallScript.hipoints -= damage;
            //Debug.Log("Wall HP: " + wallScript.hipoints);
        }
    }
}
