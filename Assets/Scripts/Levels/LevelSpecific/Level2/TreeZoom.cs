using UnityEngine;

public class TreeZoom : MonoBehaviour
{
    public int xZoomStart, xZoomEnd, xZoomFullStart, xZoomFullEnd, farZoom, closeZoom, truckOffsetYMin, truckOffsetYMax;
    public GameObject truck, mainCam;

    private float zoomLerp, truckOffsetXMin, truckOffsetXMax;

    private void Start()
    {
        if (PlayerPrefs.GetInt("multiplayer") == 1)
        {
            truckOffsetXMin = 14.5f;
            truckOffsetXMax = 14.5f;
        }
        else
        {
            truckOffsetXMin = 5f;
            truckOffsetXMax = 5f;
        }
    }

    void Update ()
    {
		if (truck.GetComponent<Transform>().position.x >= xZoomStart && truck.GetComponent<Transform>().position.x <= xZoomEnd)
        {
            if (truck.GetComponent<Transform>().position.x <= xZoomFullStart)
            {
                zoomLerp = (truck.GetComponent<Transform>().position.x - xZoomStart) / (xZoomFullStart - xZoomStart);
                mainCam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(closeZoom, farZoom, zoomLerp);
                mainCam.GetComponent<Transform>().position = new Vector3(truck.GetComponent<Transform>().position.x + Mathf.Lerp(truckOffsetXMin, truckOffsetXMax, zoomLerp), truck.GetComponent<Transform>().position.y + Mathf.Lerp(truckOffsetYMin, truckOffsetYMax, zoomLerp), -10);
            }
            else if (truck.GetComponent<Transform>().position.x < xZoomFullEnd)
            {
                mainCam.GetComponent<Camera>().orthographicSize = farZoom;
                mainCam.GetComponent<Transform>().position = new Vector3(truck.GetComponent<Transform>().position.x + truckOffsetXMax, truck.GetComponent<Transform>().position.y + truckOffsetYMax, -10);
            }
            else
            {
                zoomLerp = (truck.GetComponent<Transform>().position.x - xZoomFullEnd) / (xZoomEnd - xZoomFullEnd);
                mainCam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(farZoom, closeZoom, zoomLerp);
                mainCam.GetComponent<Transform>().position = new Vector3(truck.GetComponent<Transform>().position.x + Mathf.Lerp(truckOffsetXMax, truckOffsetXMin, zoomLerp), truck.GetComponent<Transform>().position.y + Mathf.Lerp(truckOffsetYMax, truckOffsetYMin, zoomLerp), -10);
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
