using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    GameObject soundSlider;
    Slider volumeSlider;
    AudioSource sound;

    List<Collider2D> enemies = new List<Collider2D>();


    void Start()
    {
        rangeTrigger = gameObject.AddComponent<CircleCollider2D>();
        rangeTrigger.radius = range;
        rangeTrigger.isTrigger = true;
        sound = gameObject.GetComponent<AudioSource>();
        soundSlider = GameObject.FindGameObjectWithTag("VolumeSlider");
        volumeSlider = soundSlider.GetComponent<Slider>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision);
            if (!hasTarget)
            {
                Debug.Log(collision.gameObject.name + "In turret range");
                hasTarget = true;
                GetClosestEnemy();
                StartCoroutine(Fire());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemies.Remove(collision);
        if(collision.gameObject == target)
        {
            StopCoroutine(Fire());
            CheckIfEnemiesStillExist();
        }
    }

    void Update()
    {
        sound.volume = volumeSlider.value;

        if (target is not null)
        {
            GetClosestEnemy();
            Vector3 diff = target.transform.position - transform.position;

            float targetAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180;
            float currentAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, targetAngle, ref currentAngleVelocity, rotationTime);

            transform.eulerAngles = new Vector3(0, 0, currentAngle);
        }

    }
    void CheckIfEnemiesStillExist()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] is null)
            {
                enemies.RemoveAt(i);
            }
        }

        if (enemies.Count > 0)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
            target = null;
        }
    }

    void GetClosestEnemy()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] is null)
                {
                    enemies.RemoveAt(i);
                }
            }
            
            float[] distances = new float[enemies.Count];

            for (int i = 0; i < enemies.Count; i++)
            {
                distances[i] = Vector3.Distance(enemies[i].transform.position, transform.position);
            }

            float minDistance = 0;
            int shortestDistanceID = 0;

            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < minDistance)
                {
                    minDistance = distances[i];
                    shortestDistanceID = i;
                }
            }

            target = enemies[shortestDistanceID].transform.gameObject;
        }
    }

    IEnumerator Fire()
    {
        while (hasTarget)
        {
            GameObject shellClone = Instantiate(shell, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));

            sound.Play();

            Rigidbody2D shellCloneRB = shellClone.AddComponent<Rigidbody2D>();
            shellCloneRB.gravityScale = 0;
            shellCloneRB.AddForce(shellClone.transform.up * shellVelocity);
            shellCloneRB.mass = 0.001f;

            Shell shellScript = shellClone.AddComponent<Shell>();
            shellScript.damage = damage;
            yield return new WaitForSeconds(reload);
        }
    }

}
