using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float time = 0;
    public int spawnRange = 1;
    public Vector2 direction;
    public float speed = 0.1f;

    private Renderer renderer;
    private bool isInvisible;

    // Start is called before the first frame update
    void Start()
    {
        //direction = new Vector2(1, 0);
        renderer = this.GetComponent<Renderer>();
        isInvisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        direction = Vector2.zero;
        //stop player
        bool blockMovingUp = false;
        bool blockMovingDown = false;
        bool blockMovingRight = false;
        bool blockMovingLeft = false;
        //Screen
        float halfHeight = 128;//  renderer.bounds.size.y ; 
        float halfWidth = 128;// renderer.bounds.size.x ;

        Vector2 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        if ((currentPosition.x + halfWidth - 70) > Screen.width)
        {
            blockMovingRight = true;
        }
        if ((currentPosition.x) <= 50)
        {
            blockMovingLeft = true;
        }
        if ((currentPosition.y + halfHeight - 70) > Screen.height)
        {
            blockMovingUp = true;
        }
        if ((currentPosition.y) <= 50)
        {
            blockMovingDown = true;
        }


        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !blockMovingUp)
        {
            direction.y = 1;
            direction.x = 0;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !blockMovingRight)
        {
            direction.x = 1;
            direction.y = 0;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && !blockMovingLeft)
        {
            direction.x = -1;
            direction.y = 0;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && !blockMovingDown)
        {
            direction.y = -1;
            direction.x = 0;
        }
        if (direction == Vector2.zero)
        {
            return;
        }
        direction.Normalize();

        /*if (direction.x == 1 || direction.x == -1)
        {
            direction.y = 0;
        }
        if (direction.y == 1 || direction.y == -1)
        {
            direction.x = 0;
        }*/

        Vector3 newPosition = new Vector3(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0);
        this.transform.position += newPosition;
        //transform.position *= WrapPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if(gameObject.tag == "Speed")
        {
            speed += 1;
            Debug.Log("Player speed is now: " + speed);
            Destroy(gameObject);
        }
        if(gameObject.tag == "Explosion")
        {
            Debug.Log("You Died");
            Destroy(this.gameObject);
        }
        Debug.Log(gameObject.tag + " " + gameObject.name);
    }

    private Vector2 WrapPlayer()
    {
        if (renderer.isVisible)
        {
            isInvisible = false;
            return new Vector2(1, 1);
        }

        if (isInvisible)
        {
            return new Vector2(1, 1);
        }

        float xFix = 1;
        float yFix = 1;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1)
            xFix = -1;
        if (pos.y < 0 || pos.y > 1)
            yFix = -1;
        isInvisible = true;
        return new Vector2(xFix, yFix);
    }
}
