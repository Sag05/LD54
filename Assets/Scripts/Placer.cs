using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

public class Placer : MonoBehaviour
{

    [System.Serializable]
    public struct PosArray
    {
        public GameObject position;
        public bool placed;
    }

    public struct Selected
    {
        public int cost;
        public GameObject obj;
        public GameObject img;
    }

    public GameObject gameManager;
    Economy economyScript;

    public GameObject LMGTurretImg;
    public GameObject LMGTurret;
    public int LMGCost;
    public GameObject HMGTurretImg;
    public GameObject HMGTurret;
    public int HMGCost;
    public GameObject SniperTurretImg;
    public GameObject SniperTurret;
    public int SniperCost;

    public PosArray[] positions;
    //int selectedTurretCost;

    Selected selected;

    Camera camera;

    bool dragging = false;
    GameObject currentObj;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        economyScript = gameManager.GetComponent<Economy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Destroy(currentObj);
            dragging = false;
        }

        if (dragging)
        {
            Vector3 newPos = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y);
            currentObj.transform.position = newPos;

            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    if (Mathf.Abs(positions[i].position.transform.position.x - newPos.x) < 0.5 && Mathf.Abs(positions[i].position.transform.position.y - newPos.y) < 0.5 && positions[i].placed == false && economyScript.currency >= selected.cost) 
                    {
                        economyScript.currency -= selected.cost;
                        Instantiate(selected.obj, positions[i].position.transform.position, Quaternion.identity);
                        positions[i].placed = true;
                        dragging = false;
                        Destroy(currentObj);
                        break;
                    }
                }
            }
        }
    }

    public void LMGTurretSelected()
    {
        if (!dragging)
        {
            selected.cost = LMGCost;
            selected.obj = LMGTurret;
            selected.img = LMGTurretImg;
            dragging = true;
            currentObj = Instantiate(selected.img);
        }
        else
        {
            Destroy(currentObj);
            dragging = false;
        }


    }
    public void HMGTurretSelected()
    {
        if (!dragging)
        {
            selected.cost = HMGCost;
            selected.obj = HMGTurret;
            selected.img = HMGTurretImg;
            dragging = true;
            currentObj = Instantiate(selected.img);
        }
        else
        {
            Destroy(currentObj);
            dragging = false;
        }
    }
    public void SniperTurretSelected()
    {
        if (!dragging)
        {
            selected.cost = SniperCost;
            selected.obj = SniperTurret;
            selected.img = SniperTurretImg;
            dragging = true;
            currentObj = Instantiate(selected.img);
        }
        else
        {
            Destroy(currentObj);
            dragging = false;
        }
    }
}
