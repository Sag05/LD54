using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float shellVelocity;
    public float reload;
    public int damage;
    public int range;
    public float rotationTime;
    float currentAngleVelocity;
    CircleCollider2D rangeTrigger;
    public GameObject shell;

    bool hasTarget = false;
    GameObject target;

    void Start()
    {
        rangeTrigger = gameObject.AddComponent<CircleCollider2D>();
        rangeTrigger.radius = range;
        rangeTrigger.isTrigger = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!hasTarget)
            {
                Debug.Log(collision.gameObject.name + "In turret range");
                hasTarget = true;
                target = collision.gameObject;
                StartCoroutine(Fire());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            hasTarget = false;
            target = null;
        }
    }

    void Update()
    {
        
        if (hasTarget)
        {
            Vector3 diff = target.transform.position - transform.position;

            float targetAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180;
            float currentAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, targetAngle, ref currentAngleVelocity, rotationTime);

            transform.eulerAngles = new Vector3(0, 0, currentAngle);
        }


    }

    IEnumerator Fire()
    {
        while (hasTarget)
        {
            yield return new WaitForSeconds(reload);
            GameObject shellClone = Instantiate(shell, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));


            Rigidbody2D shellCloneRB = shellClone.AddComponent<Rigidbody2D>();
            shellCloneRB.gravityScale = 0;
            shellCloneRB.AddForce(shellClone.transform.up * shellVelocity);
            shellCloneRB.mass = 0.001f;

            Shell shellScript = shellClone.AddComponent<Shell>();
            shellScript.damage = damage;
        }
    }

}
