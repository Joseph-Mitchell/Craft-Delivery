using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float xOffset = 5f;
    private float yOffset = 2f;

    public float maxCamera;

    public GameObject truck;

    public bool lockCamera;

    public int player;

    private void Start()
    {
        if(PlayerPrefs.GetInt("multiplayer") == 1)
        {
            if (player == 1)
                gameObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
            else if (player == 2)
                gameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);

            xOffset = 14.5f;
        }

        lockCamera = true;
    }

    void Update ()
    {
        if (lockCamera)
        {
            if (PlayerPrefs.GetInt("multiplayer") == 1)
            {
                if (truck.GetComponent<Transform>().position.x + xOffset < -20f)
                    gameObject.GetComponent<Transform>().position = new Vector3(-20f, truck.GetComponent<Transform>().position.y + yOffset, -10);
                else if (truck.GetComponent<Transform>().position.x > maxCamera - 18.3f)
                    gameObject.GetComponent<Transform>().position = new Vector3(maxCamera + xOffset - 18.3f, truck.GetComponent<Transform>().position.y + yOffset, -10);
                else
                    gameObject.GetComponent<Transform>().position = new Vector3(truck.GetComponent<Transform>().position.x + xOffset, truck.GetComponent<Transform>().position.y + yOffset, -10);
            }
            else
            {
                if (truck.GetComponent<Transform>().position.x + xOffset < 0.1f)
                    gameObject.GetComponent<Transform>().position = new Vector3(0.1f, truck.GetComponent<Transform>().position.y + yOffset, -10);
                else if (truck.GetComponent<Transform>().position.x > maxCamera)
                    gameObject.GetComponent<Transform>().position = new Vector3(maxCamera + xOffset, truck.GetComponent<Transform>().position.y + yOffset, -10);
                else
                    gameObject.GetComponent<Transform>().position = new Vector3(truck.GetComponent<Transform>().position.x + xOffset, truck.GetComponent<Transform>().position.y + yOffset, -10);
            }
        }
    }
}
