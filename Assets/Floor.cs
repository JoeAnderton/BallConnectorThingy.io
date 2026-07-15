using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public float growSize;
    public Rigidbody2D rigidBody;
    float posX;
    public bool canMove = false;
    public float speed = 0.001f;
    bool movingLeft = true;
    public float maxDistance = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posX = transform.position.x;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingLeft && canMove)
        {
            posX -= speed;
            rigidBody.MovePosition(new Vector3(posX, transform.position.y, transform.position.z));
            leftWall.GetComponent<Rigidbody2D>().MovePosition(new Vector3(posX - transform.localScale.x/2 + leftWall.transform.localScale.x/2, leftWall.transform.position.y, leftWall.transform.position.z));
            rightWall.GetComponent<Rigidbody2D>().MovePosition(new Vector3(posX + transform.localScale.x / 2 - rightWall.transform.localScale.x / 2, rightWall.transform.position.y, rightWall.transform.position.z));
            if (rigidBody.position.x <= -maxDistance)
            {
                movingLeft = false;
            }
        }
        else if (!movingLeft && canMove)
        {
            posX += speed;
            rigidBody.MovePosition(new Vector3(posX, transform.position.y, transform.position.z));
            leftWall.GetComponent<Rigidbody2D>().MovePosition(new Vector3(posX - transform.localScale.x / 2 + leftWall.transform.localScale.x/2, leftWall.transform.position.y, leftWall.transform.position.z));
            rightWall.GetComponent<Rigidbody2D>().MovePosition(new Vector3(posX + transform.localScale.x / 2 - rightWall.transform.localScale.x / 2, rightWall.transform.position.y, rightWall.transform.position.z));
            if (rigidBody.position.x >= maxDistance)
            {
                movingLeft = true;
            }
        }
    }
    public void floorGrow()
    {
        transform.localScale = new Vector3(transform.localScale.x + growSize, transform.localScale.y, transform.localScale.z);
        leftWall.gameObject.transform.localPosition = new Vector3(leftWall.gameObject.transform.localPosition.x - growSize/2, leftWall.gameObject.transform.localPosition.y, leftWall.gameObject.transform.localPosition.z);
        rightWall.gameObject.transform.localPosition = new Vector3(rightWall.gameObject.transform.localPosition.x + growSize/2, rightWall.gameObject.transform.localPosition.y, rightWall.gameObject.transform.localPosition.z);
    }
}
