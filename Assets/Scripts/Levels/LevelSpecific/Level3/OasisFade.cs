using UnityEngine;

public class OasisFade : MonoBehaviour
{

    private bool fadeOut;
    private float fadeLerp;

	void Start ()
    {
        fadeOut = false;
        fadeLerp = 1f;
	}
	
	void Update ()
    {
        if (fadeOut && fadeLerp > 0)
            fadeLerp -= 0.01f;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(0, 1, fadeLerp));
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeOut = true;
        }
    }
}
