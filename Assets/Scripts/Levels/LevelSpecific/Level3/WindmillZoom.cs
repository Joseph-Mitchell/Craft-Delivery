using UnityEngine;

public class WindmillZoom : MonoBehaviour
{
    public int xZoomStart, xZoomEnd, xZoomFullStart, xZoomFullEnd, farZoom, closeZoom, camY, camX;
    public GameObject truck, mainCam;

    private float zoomLerp, truckYAtStart, xOffset;

    void Start()
    {
        truckYAtStart = 0;

        if (PlayerPrefs.GetInt("multiplayer") == 1)
        {
            xOffset = 14.5f;
        }
        else
        {
            xOffset = 5f;
        }
    }

    void Update ()
    {
		if (truck.GetComponent<Transform>().position.x >= xZoomStart && truck.GetComponent<Transform>().position.x <= xZoomEnd)
        {
            if (truck.GetComponent<Transform>().position.x <= xZoomFullStart)
            {
                if (truckYAtStart == 0)
                    truckYAtStart = truck.GetComponent<Transform>().position.y + 2;

                zoomLerp = (truck.GetComponent<Transform>().position.x - xZoomStart) / (xZoomFullStart - xZoomStart);
                mainCam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(closeZoom, farZoom, zoomLerp);
                mainCam.GetComponent<Transform>().position = new Vector3(Mathf.Lerp(xZoomStart + xOffset, camX, zoomLerp), Mathf.Lerp(truckYAtStart, camY, zoomLerp), -10);
            }
            else if (truck.GetComponent<Transform>().position.x < xZoomFullEnd)
            {
                mainCam.GetComponent<Camera>().orthographicSize = farZoom;
                mainCam.GetComponent<Transform>().position = new Vector3(camX, camY, -10);
            }
            else
            {
                zoomLerp = (truck.GetComponent<Transform>().position.x - xZoomFullEnd) / (xZoomEnd - xZoomFullEnd);
                mainCam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(farZoom, closeZoom, zoomLerp);
                mainCam.GetComponent<Transform>().position = new Vector3(Mathf.Lerp(camX, xZoomEnd + xOffset, zoomLerp), Mathf.Lerp(camY, truck.GetComponent<Transform>().position.y + 2, zoomLerp), -10);
            }

            mainCam.GetComponent<CameraScript>().lockCamera = false;
        }
        else
        {
            mainCam.GetComponent<CameraScript>().lockCamera = true;
            mainCam.GetComponent<Camera>().orthographicSize = 5;
        }
    }
}
