using UnityEngine;

public class SimpleTruckMovement : MonoBehaviour
{
    public GameObject backWheel, frontWheel, finish;

    public float speed, maxSpeed, minSpeed, rotationDelta;

    public bool forward, backward;

    public int player;

    public string input;

    void Start()
    {
        if (player == 1)
            input = "Vertical";
        if (player == 2)
            input = "Vertical2";
    }

    void Update()
    {
        if (!finish.GetComponent<FinishScript>().startItemDrag)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            
            //Drive
            if (backWheel.GetComponent<WheelCollision>().colliding || frontWheel.GetComponent<WheelCollision>().colliding)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                    speed = -0.75f;
                else if (Input.GetAxisRaw("Vertical") > 0)
                    speed = 0.75f;
                else
                    speed = 0;
            }
            else
            {
                speed = 0;
            }

            gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(speed, 0);

            //Rotate
            if (Input.GetAxisRaw("Horizontal") > 0)
                gameObject.GetComponent<Rigidbody2D>().angularVelocity -= 20;
            else if (Input.GetAxisRaw("Horizontal") < 0)
                gameObject.GetComponent<Rigidbody2D>().angularVelocity += 20;

        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
