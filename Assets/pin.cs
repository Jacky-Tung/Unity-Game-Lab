using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] GameObject player; 
    [SerializeField] const int SPEED = 7;
    [SerializeField] int verticaldir = 1;
    [SerializeField] int horizontaldir = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(player.GetComponent<Movement>().isFacingLeft) 
            horizontaldir *= -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(horizontaldir * SPEED, verticaldir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VerticalBorder" || collision.gameObject.tag == "HorizontalBorder")
            Destroy(gameObject);
    }
}
