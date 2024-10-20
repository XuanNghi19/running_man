using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if(pos.x <= -1.7f) {
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
