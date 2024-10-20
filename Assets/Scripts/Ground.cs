using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    bool didGeneratedGround = false;
    BoxCollider2D collider2d;
    public Obstacle boxTemplate;

    public GameObject[] grounds;

    void Awake()
    {
        collider2d = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        groundHeight = transform.position.y + (collider2d.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }

    // Start and Update methods remain unchanged

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (collider2d.size.x / 2);

        if (groundRight < -1)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGeneratedGround)
        {
            if (groundRight < screenRight)
            {
                didGeneratedGround = true;
                GenerateGround();
            }
        }

        transform.position = pos;
    }

    void GenerateGround()
    {
        // GameObject go = Instantiate(gameObject);
        // BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        GameObject go = new GameObject();
        BoxCollider2D goCollider = new BoxCollider2D();
        Vector2 pos;
        int idxRandom = Random.Range(0, grounds.Length - 1);
        try
        {
            go = Instantiate(grounds[idxRandom].gameObject);
            goCollider = go.GetComponent<BoxCollider2D>();
        }
        catch (System.Exception ex)
        {
            Debug.Log($"Error occurred: {ex.Message}");
            Debug.Log($"Stack Trace: {ex.StackTrace}");  // In ra chi tiết stack trace
        }

        // Calculate the maximum jump height
        float h1 = player.jumpVelocity * player.maxHoldJumpTime;
        float t = player.jumpVelocity / Mathf.Abs(player.gravity);
        float h2 = player.jumpVelocity * t + (0.5f * (-player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;

        // Calculate the range for ground placement
        float minY = 1f;
        float maxY = maxJumpHeight * 0.7f + groundHeight;

        float actualY = Random.Range(minY, maxY);

        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 11.4f) pos.y = 11.4f;

        float t1 = t + player.maxHoldJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / Mathf.Abs(player.gravity));
        float totalTime = t1 + t2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.6f;
        maxX += groundRight;
        float minX = screenRight + 5;
        float actualX = Random.Range(minX, maxX);

        pos.x = actualX + (goCollider.size.x / 2);

        go.transform.position = pos;

        // Update the Ground script of the new ground
        Ground newGround = go.GetComponent<Ground>();
        newGround.groundHeight = pos.y + (goCollider.size.y / 2);
        newGround.didGeneratedGround = false;

        int obstacleNum = Random.Range(0, 3);
        for (int i = 0; i < obstacleNum; i++)
        {
            GameObject box = Instantiate(boxTemplate.gameObject);
            float y = newGround.groundHeight + 0.5f;
            float haftWidth = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - haftWidth;
            float right = go.transform.position.x + haftWidth;
            float x = Random.Range(left, right);
            Vector2 boxPos = new(x, y);
            box.transform.position = boxPos;
        }
    }
}
