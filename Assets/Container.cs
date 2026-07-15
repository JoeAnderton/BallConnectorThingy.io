using UnityEngine;

public class Container : MonoBehaviour
{
    bool movingLeft = true;
    float posX;
    public bool canMove = false;
    public float speed = 0.5f;
    public float maxDistance = 10;
    public Rigidbody2D rigidBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posX = transform.position.x;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft && canMove)
        {
            posX -= speed;
            rigidBody.MovePosition(new Vector3(posX, transform.position.y, transform.position.z));
            if (rigidBody.position.x <= -maxDistance)
            {
                movingLeft = false;
            }
        }
        else if (!movingLeft && canMove)
        {
            posX += speed;
            rigidBody.MovePosition(new Vector3(posX, transform.position.y, transform.position.z));
            if (rigidBody.position.x >= maxDistance)
            {
                movingLeft = true;
            }
        }
    }
}
