using UnityEngine;

public class CargoDrop : MonoBehaviour
{
    public GameObject timer;

    public bool animFinish;

	void Start ()
    {
        StopAnim();
        animFinish = false;
    }

    private void Update()
    {
        if (animFinish)
        {
            gameObject.GetComponent<Animator>().StopPlayback();
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !animFinish)
            StartAnim();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !animFinish)
            StopAnim();
    }

    private void StartAnim()
    {
        timer.SetActive(true);
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().Play("cargoDropAnim", -1, 0);
    }

    private void StopAnim()
    {
        timer.SetActive(false);
        gameObject.GetComponent<Animator>().StopPlayback();
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
