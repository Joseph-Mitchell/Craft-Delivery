using UnityEngine;

public class WheelCollision : MonoBehaviour
{

    public bool colliding;

    void Start()
    {
        colliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }
}
