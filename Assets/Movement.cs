using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float horizontalMovement;
    [SerializeField] float verticalMovement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 5;
    public bool isFacingLeft = true;
    // [SerializeField] bool jumpPressed = false;
    // [SerializeField] float jumpForce = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // if (Input.GetButtonDown("Jump"))
        //     jumpPressed = true;
    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(SPEED * horizontalMovement, SPEED * verticalMovement);
        if (horizontalMovement < 0 && !isFacingLeft || horizontalMovement > 0 && isFacingLeft)
            Flip();
        // if (jumpPressed && isGrounded)
        //     Jump();
        // else
        //     jumpPressed = false;
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingLeft = !isFacingLeft;
    }

    // private void Jump()
    // {
    //     rigid.velocity = new Vector2(rigid.velocity.x, 0);
    //     rigid.AddForce(new Vector2(0, jumpForce));
    //     Debug.Log("jumped");
    //     jumpPressed = false;
    //     isGrounded = false;
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log(collision.gameObject.tag);
    //     if (collision.gameObject.tag == "Border")
    //         collideBorder = true;
    //     else
    //         Debug.Log(collision.gameObject.tag);
    // }
}
