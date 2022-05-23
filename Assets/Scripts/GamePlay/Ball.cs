using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] Transform player;
    const float DEFAULT_SPEED = 5;
    const float SPEED_UP = 3f;
    const float PLAYER_FRICTION = 0.3f;

    float ballSpeed;
    bool isShooted = false;
    Vector2 INIT_POSITION_OFFSET = new Vector2(0, 0.2f);

    private void Update()
    {
        if (!isShooted)
        {
            transform.localPosition = (Vector2)player.position + INIT_POSITION_OFFSET;
        }
        if (transform.position.y < -5.5f || transform.position.y > 3.6f)
        {
            Debug.Log("Ball over frame");
            GamePlayManager.Instance.OnBallFall();
        }
    }

    public void Shoot()
    {
        if (isShooted)
        {
            return;
        }
        isShooted = true;
        ballSpeed = DEFAULT_SPEED;
        //myRigidbody.velocity = new Vector2(-1f, 3f);

        //vx in range +- 0.25 to 0.5
        float vx = Random.Range(-0.25f, 0.25f);
        vx = vx + 0.25f * (vx > 0 ? 1 : -1);
        myRigidbody.velocity = DEFAULT_SPEED * (new Vector2(vx, 1f)).normalized;
    }

    public void Reset()
    {
        isShooted = false;
        myRigidbody.velocity = Vector2.zero;
        transform.localPosition = (Vector2)player.position + INIT_POSITION_OFFSET;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Brick":
                collision.gameObject.GetComponent<Brick>().HitByBall();
                break;

            case
                "Player":
                //fixed total speed
                myRigidbody.velocity =
                    ballSpeed * (myRigidbody.velocity +
                    (PLAYER_FRICTION * collision.gameObject.GetComponent<Player>().GetVelocity())).normalized;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Abyss")
        {
            GamePlayManager.Instance.OnBallFall();
        }
    }

    public void GetItem(Item.ItemType type)
    {
        switch (type)
        {
            case Item.ItemType.BallQuick:
                ballSpeed += SPEED_UP;
                myRigidbody.velocity =
                  ballSpeed * myRigidbody.velocity.normalized;
                break;
        }
    }
}
