using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] diff1;
    public GameObject[] diff2;
    public GameObject[] diff3;
    public GameObject[] diff4;
    GameObject[] diff5;

    public GameObject enemy;

    public int round = 1;
    public int roundTime;

    bool playing = true;

    void Start()
    {
        StartCoroutine(DiffSelect());
        diff5 = GameObject.FindGameObjectsWithTag("SpawnLocations");
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

                case 10:
                    Spawn(diff2);
                    roundTime--;
                    break;

                case < 20:
                    Spawn(diff2);
                    break;

                case 20:
                    Spawn(diff3);
                    roundTime--; //8
                    break;

                case 30:
                    Spawn(diff3);
                    roundTime--; //7
                    break;

                case 40:
                    Spawn(diff3);
                    roundTime--;
                    break;

                case < 50:
                    Spawn(diff3);
                    break;

                case 50:
                    Spawn(diff4);
                    roundTime--;//5
                    break;

                case 60:
                    Spawn(diff4);
                    roundTime--; //4
                    break;

                case 70:
                    Spawn(diff4);
                    roundTime--; //3
                    break;

                case < 100:
                    Spawn(diff4);
                    break;

                default:
                    Spawn(diff5);
                    break;
            }
            round++;
        }
    }

    void Spawn(GameObject[] positions)
    {
        foreach (GameObject position in positions)
        {
            Instantiate(enemy, position.transform.position, Quaternion.identity);
        }
    }
}
