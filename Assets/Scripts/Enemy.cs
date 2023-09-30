using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject gameManager;
    Economy economyScript;
    GameObject spawner;
    Spawner spawnerScript;

    public int defaultSpeed;
    public int defaultHitpoints;
    public int reward;
    Rigidbody2D rigidbody;

    public float speed;
    public float hitpoints;

    bool hasRun = false;
    int lastRound;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        economyScript = gameManager.GetComponent<Economy>();
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        spawnerScript = spawner.GetComponent<Spawner>();

        speed = defaultSpeed;
        hitpoints = defaultHitpoints;

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.right * speed;


        if (!hasRun)
        {
            hasRun = true;
            speed += spawnerScript.round / 10;
            hitpoints += spawnerScript.round / 10;
            Debug.Log("Speed: " + speed);
        }
        else if (lastRound < spawnerScript.round)
        {
            speed += spawnerScript.round / 10;
            hitpoints += spawnerScript.round / 10;
            Debug.Log("Speed: " + speed);
        }
        lastRound = spawnerScript.round;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shell")
        {
            hitpoints = hitpoints - collision.gameObject.GetComponent<Shell>().damage;
            //Debug.Log("Enemy HP: " + hitpoints);

            Destroy(collision.gameObject);
            if (hitpoints <= 0)
            {
                economyScript.currency += reward;
                Destroy(gameObject);
            }
        }
    }
}
