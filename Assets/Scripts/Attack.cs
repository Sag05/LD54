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
            if (!inWallRange)
            {
                Debug.Log("colliding with: " + collision.gameObject.name);
                wallScript = collision.gameObject.GetComponent<Wall>();
                inWallRange = true;

                StartCoroutine(AttackCoroutine());
            }

        }
    }

    IEnumerator AttackCoroutine()
    {
        while (inWallRange)
        {
            wallScript.hipoints -= damage;
            yield return new WaitForSeconds(reload);
            //Debug.Log("Wall HP: " + wallScript.hipoints);
        }
    }
}
