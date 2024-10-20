using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prallax : MonoBehaviour
{

    public float depth = 1;
    Player player;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float realVelocity = player.velocity.x  / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if(pos.x <= -13) {
            pos.x = 64.4f;
        }

        transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
