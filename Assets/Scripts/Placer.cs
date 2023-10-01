using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{

    [System.Serializable]
    public struct PosArray
    {
        public GameObject position;
        public bool placed;
        public GameObject placedObject;
    }

    public struct Selected
    {
        public int cost;
        public GameObject obj;
        public GameObject img;
    }

    public GameObject gameManager;
    Economy economyScript;

    public GameObject lMGTurretImg;
    public GameObject lMGTurret;
    public int lMGCost;
    public GameObject hMGTurretImg;
    public GameObject hMGTurret;
    public int hMGCost;
    public GameObject sniperTurretImg;
    public GameObject sniperTurret;
    public int sniperCost;
    public GameObject tMMTurretImg;
    public GameObject tMMTurret;
    public int tMMCost;
    public GameObject fMMTurretImg;
    public GameObject fMMTurret;
    public int fMMCost;
    public GameObject removeTurretImg;


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
                    if (selected.img == removeTurretImg)
                    {
                        if (Mathf.Abs(positions[i].position.transform.position.x - newPos.x) < 0.5 && Mathf.Abs(positions[i].position.transform.position.y - newPos.y) < 0.5 && positions[i].placed == true)
                        {

                            Destroy(positions[i].placedObject);
                            dragging = false;
                            positions[i].placed = false;
                            Destroy(currentObj);
                            break;
                        }
                    }
                    else if (Mathf.Abs(positions[i].position.transform.position.x - newPos.x) < 0.5 && Mathf.Abs(positions[i].position.transform.position.y - newPos.y) < 0.5 && positions[i].placed == false && economyScript.currency >= selected.cost)
                    {
                        economyScript.currency -= selected.cost;
                        positions[i].placedObject = Instantiate(selected.obj, positions[i].position.transform.position, Quaternion.identity);
                        dragging = false;
                        Destroy(currentObj);
                        positions[i].placed = true;
                        break;
                    }
                }
            }
        }
    }

    public void RemoveTurretSelected()
    {
        if (!dragging)
        {
            selected.img = removeTurretImg;
            selected.cost = 0;
            dragging = true;
            currentObj = Instantiate(removeTurretImg);
        }
        else
        {
            Destroy(currentObj);
            dragging = false;
        }
    }

    public void LMGTurretSelected()
    {
        if (!dragging)
        {
            selected.cost = lMGCost;
            selected.obj = lMGTurret;
            selected.img = lMGTurretImg;
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
            selected.cost = hMGCost;
            selected.obj = hMGTurret;
            selected.img = hMGTurretImg;
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
            selected.cost = sniperCost;
            selected.obj = sniperTurret;
            selected.img = sniperTurretImg;
            dragging = true;
            currentObj = Instantiate(selected.img);
        }
        else
        {
            Destroy(currentObj);
            dragging = false;
        }
    }
    public void TMMTurretSelected()
    {
        if (!dragging)
        {
            selected.cost = tMMCost;
            selected.obj = tMMTurret;
            selected.img = tMMTurretImg;
            dragging = true;
            currentObj = Instantiate(selected.img);
        }
        else
        {
            Destroy(currentObj);
            dragging = false;
        }
    }
    public void FMMTurretSelected()
    {
        if (!dragging)
        {
            selected.cost = fMMCost;
            selected.obj = fMMTurret;
            selected.img = fMMTurretImg;
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
