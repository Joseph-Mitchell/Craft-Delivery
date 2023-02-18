using UnityEngine;

public class PageMove : MonoBehaviour
{

    public bool playAnim;

    private void Start()
    {
        playAnim = false;
    }

    void Update ()
    {
        if (playAnim)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}
