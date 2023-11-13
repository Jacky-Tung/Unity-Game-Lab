using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballon : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D collider;
    [SerializeField] int speed = 6;
    [SerializeField] bool collideVerticalBorder = false;
    [SerializeField] bool collideHorizontalBorder = false;
    [SerializeField] int verticaldir = 1;
    [SerializeField] int horizontaldir = 1;
    [SerializeField] bool isFacingLeft = true;
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject controller;
    [SerializeField] Vector3 scaleChange;
    [SerializeField] int points;
    [SerializeField] int level;
    [SerializeField] float popTime; 
    [SerializeField] int decrementer;
    [SerializeField] float timeThresholdForThisLevel;
    [SerializeField] bool popped;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if(rigid == null)
            rigid = GetComponent<Rigidbody2D>();   
        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("GameController");
        }
        if (audio == null)
            audio = GetComponent<AudioSource>();
        if(spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(collider == null)
            collider = gameObject.GetComponent<Collider2D>();
        if(animator == null)
            animator = gameObject.GetComponent<Animator>();
            
        float spawnPos = Random.Range(-8.0f, 8.0f);
        rigid.transform.position += new Vector3(spawnPos, 0, 0);
        scaleChange = new Vector3(0.007f, 0.007f, 0.007f);
        points = 10;
        level = PersistentData.Instance.GetLevel();
        decrementer = level;
        speed *= level;
        timeThresholdForThisLevel = 12.0f - (level * level);
        popped = false;

        // Random balloon travel direction
        float dir = Random.Range(0f, 1f);
        if(dir > 0.5f){
            horizontaldir *= -1;    
        } else verticaldir *= -1;

        // random time frame, reduce time frame as level increases
        popTime = Random.Range(1.0f, timeThresholdForThisLevel);

        Invoke("PopAndRestartLevel", popTime);

        // decrement points on ballon size expansion
        InvokeRepeating("DecrementPoints", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(horizontaldir * speed, verticaldir * speed);
        rigid.transform.localScale += scaleChange;
        if(collideVerticalBorder){
            verticaldir *= -1;
            collideVerticalBorder = false;
        }
        else if(collideHorizontalBorder){
            horizontaldir *= -1;
            collideHorizontalBorder = false;
            Flip();
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VerticalBorder")
            collideVerticalBorder = true;
        else if(collision.gameObject.tag == "HorizontalBorder")
            collideHorizontalBorder = true;

        if(collision.gameObject.tag == "Pin"){
            Pop();
            controller.GetComponent<Scorekeeper>().UpdateScore(points);
            Invoke("CompleteLevel", 1f);
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingLeft = !isFacingLeft;
    }

    private void Pop()
    {
        if(!popped){
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            animator.SetTrigger("pop");
            Invoke("HideBalloon", 0.3f);
            rigid.simulated = false;
            collider.enabled = false;
            popped = true;
        }
    }

    private void DecrementPoints()
    {
        if(points > 0) points -= decrementer;
    }

    private void PopAndRestartLevel()
    {
        Pop();
        Invoke("RestartLevel", 1f);
    }

    private void CompleteLevel()
    {
        PersistentData.Instance.SetLevel(PersistentData.Instance.GetLevel() + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HideBalloon()
    {
        spriteRenderer.enabled = false;
    }
}
