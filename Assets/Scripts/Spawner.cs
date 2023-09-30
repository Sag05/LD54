using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] diff1;
    public GameObject[] diff2;
    public GameObject[] diff3;
    public GameObject[] diff4;

    public GameObject enemy;

    public int round = 1;
    public int roundTime;

    bool playing = true;

    void Start()
    {
        StartCoroutine(DiffSelect());
    }

    void Update()
    {


    }

    IEnumerator DiffSelect()
    {
        while (playing)
        {
            yield return new WaitForSeconds(roundTime);

            Debug.Log("Round: " + round);
            switch (round)
            {
                case < 10:
                    Spawn(diff1);
                    break;

                case < 20:
                    Spawn(diff2);
                    break;

                case < 50:
                    Spawn(diff3);
                    break;

                default:
                    Spawn(diff4);
                    break;
            }
            round++;
        }
    }

    void Spawn(GameObject[] positions)
    {
        foreach(GameObject position in positions)
        {
            Instantiate(enemy, position.transform.position, Quaternion.identity);
        }
    }
}
