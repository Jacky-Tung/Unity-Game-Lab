using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Distractor : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D collider;
    [SerializeField] GameObject controller;
    private Animator animator;
    [SerializeField] int speed = 6;
    [SerializeField] int level;
    [SerializeField] int verticaldir = 1;
    [SerializeField] bool hit;
    [SerializeField] bool collideVerticalBorder = false;

    // Start is called before the first frame update
    void Start()
    {
        if(rigid == null)
            rigid = GetComponent<Rigidbody2D>();   
        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("GameController");
        }
        if(spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(collider == null)
            collider = gameObject.GetComponent<Collider2D>();
        if(animator == null)
            animator = gameObject.GetComponent<Animator>();

        level = PersistentData.Instance.GetLevel();
        speed  += level;
        hit = false;

        float spawnPos = Random.Range(-5.0f, 5.0f);
        rigid.transform.position += new Vector3(spawnPos, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(speed, verticaldir * speed);
        if(collideVerticalBorder){
            verticaldir *= -1;
            collideVerticalBorder = false;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VerticalBorder")
            collideVerticalBorder = true;

        if(collision.gameObject.tag == "Pin"){
            Hit();
            Destroy(collision.gameObject);
            Invoke("RestartLevel", 1f);
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HideDistractor()
    {
        spriteRenderer.enabled = false;
    }

    private void Hit()
    {
        if(!hit){
            animator.SetTrigger("hit");
            Invoke("HideDistractor", 0.3f);
            rigid.simulated = false;
            collider.enabled = false;
            hit = true;
        }
    }
}
