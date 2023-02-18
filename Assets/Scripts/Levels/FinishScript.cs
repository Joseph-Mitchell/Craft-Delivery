using UnityEngine;

public class FinishScript : MonoBehaviour {

    const float DRAGTIME = 0.3f;

    public GameObject[] cargo;
    public GameObject levelCompletePanel, timer, scoreController;
    public Sprite[] sprite;
    public static bool levelComplete;
    public bool startItemDrag;
    public static bool p1Finished, p2Finished;
    public int cargoWorth, maxTime, player;
    public string prefKeyUnlock;

    private int closest;
    private float closestDist = -1;
    private Vector3 centre;
    private bool[] cargoInRange;
    private bool noMoreCargo, timeCalc;
    private float dragTimer = DRAGTIME;

    private void Start()
    {
        levelCompletePanel.SetActive(false);

        p1Finished = false;
        p2Finished = false;
        startItemDrag = false;
        levelComplete = false;

        centre = new Vector3(gameObject.GetComponent<Transform>().position.x - 0.07f, gameObject.GetComponent<Transform>().position.y - 0.4f, 1);

        cargoInRange = new bool[cargo.Length];
        for (int i = 0; i < cargoInRange.Length; i++)
        {
            cargoInRange[i] = true;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("multiplayer") == 1)
        {
            if (p1Finished && p2Finished)
            {
                levelCompletePanel.SetActive(true);
            }
        }
        else
        {
            if (p1Finished)
            {
                levelCompletePanel.SetActive(true);
            }
        }

        if (Time.timeScale > 0)
        {
            if (startItemDrag)
            {
                if (!noMoreCargo)
                {
                    CollectClosest();

                    noMoreCargo = true;
                    for (int i = 0; i < cargo.Length; i++)
                    {
                        if (cargo[i].activeSelf && cargoInRange[i])
                        {
                            noMoreCargo = false;
                        }
                    }
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite[0];
                }
                else
                {
                    if (!timeCalc)
                    {
                        if (player == 1)
                            ScoreScript.timeCalc1 = true;
                        else if (player == 2)
                            ScoreScript.timeCalc2 = true;
                        timeCalc = true;
                    }
                    levelComplete = true;
                    if (player == 1)
                    {
                        p1Finished = true;
                    }
                    else if (player == 2)
                    {
                        p2Finished = true;
                    }
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite[1];
                    PlayerPrefs.SetInt(prefKeyUnlock, 1);
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite[0];
            }
        }
    }

    private void CollectClosest()
    {
        for (int i = 0; i < cargo.Length; i++)
        {
            if (cargo[i].activeSelf && cargoInRange[i])
            {
                float xdist = cargo[i].GetComponent<Transform>().position.x - centre.x;
                float ydist = cargo[i].GetComponent<Transform>().position.y - centre.y;
                float dist = Mathf.Sqrt((xdist * xdist) + (ydist * ydist));

                //Cargo that has fallen out too far from finish flag is not counted
                if (dist < 15)
                    cargoInRange[i] = true;
                else
                    cargoInRange[i] = false;

                if (cargoInRange[i])
                {
                    if (closestDist < 0 || dist < closestDist)
                    {
                        closest = i;
                        closestDist = dist;
                    }
                }
            }
        }

        cargo[closest].GetComponent<Rigidbody2D>().isKinematic = true;
        cargo[closest].GetComponent<PolygonCollider2D>().enabled = false;

        if (cargo[closest].GetComponent<Transform>().position != centre) //Prevent Sqrt(0)
        {
            float xdist = cargo[closest].GetComponent<Transform>().position.x - centre.x;
            float ydist = cargo[closest].GetComponent<Transform>().position.y - centre.y;
            float dist = Mathf.Sqrt((xdist * xdist) + (ydist * ydist));

            //Get a speed which slows on approach
            float moveSpeed = (dist / 8);

            if (moveSpeed > 0 && dist > moveSpeed && dragTimer > 0)
            {
                    cargo[closest].GetComponent<Transform>().position = new Vector3(cargo[closest].GetComponent<Transform>().position.x - (moveSpeed * xdist),
                                                                                    cargo[closest].GetComponent<Transform>().position.y - (moveSpeed * ydist),
                                                                                    cargo[closest].GetComponent<Transform>().position.z);
                    dragTimer -= Time.deltaTime;
            }
            else
            {
                DeleteCargo(closest);
            }
        }
        else
        {
            DeleteCargo(closest);
        }
    }

    void DeleteCargo(int i)
    {
        cargo[i].SetActive(false);
        closestDist = -1;
        Debug.Log("Cargo Deleted");
        if (player == 1)
            ScoreScript.p1CargoDelivered++;
        else if (player == 2)
            ScoreScript.p2CargoDelivered++;
        dragTimer = DRAGTIME;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(collision.name + " has entered the finish flag trigger box");
            timer.GetComponent<TimerScript>().tickDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(collision.name + " has exited the finish flag trigger box");
            timer.GetComponent<TimerScript>().tickDown = false;
        }
    }
}
