using UnityEngine;

public class TimerScript : MonoBehaviour {

    public Sprite[] sprites;
    public GameObject finish;

    public bool tickDown;

    private float timer;
    private bool visible;
    private int currentSprite;
    private Color color;

    private void Start()
    {
        tickDown = false;

        timer = 0.5f;
        visible = false;
        currentSprite = 0;
        color = new Color(1, 1, 1, 0);
    }

    void Update ()
    {
        if (tickDown)
        {
            if (currentSprite < 5)
            {
                visible = true;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0.5f;
                    currentSprite += 1;
                }
            }
            else
            {
                visible = false;
                finish.GetComponent<FinishScript>().startItemDrag = true;
            }
        }
        else
        {
            timer = 0.5f;
            currentSprite = 0;
            visible = false;
        }

        if (visible)
            color.a = 1;
        else
            color.a = 0;

        if (currentSprite < 5)
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        else
            visible = false;

        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
