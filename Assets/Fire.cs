using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject pin;
    [SerializeField] GameObject player;
    [SerializeField] bool fire = false;
    [SerializeField] Vector3 position;
    [SerializeField] int fireDir = 0;
    const float FireRate = 0.5F;
    private float nextFire = 0; 
    // Start is called before the first frame update
    void Start()
    {   
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(player.GetComponent<Movement>().isFacingLeft) 
            fireDir = -1;
        else fireDir = 1;
        position = player.transform.position + new Vector3(fireDir, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Movement>().isFacingLeft) 
            fireDir = -1;
        else fireDir = 1;
        position = player.transform.position + new Vector3(fireDir, 0, 0);
        if(Input.GetButton("Jump") && Time.time >= nextFire){
            nextFire = Time.time + FireRate;
            fire = !fire;
        }
    }

    private void FixedUpdate()
    {
        if(fire) FirePin();
    }

    void FirePin()
    {
        Instantiate(pin, position ,Quaternion.identity);
        fire = !fire;
    }
}
