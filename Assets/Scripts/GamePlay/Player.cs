using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigid;

    const float DEFAULT_SPEED = 5;
    Direction direction = Direction.Stay;

    public void SetDirection(Direction newDirection)
    {
        if (newDirection == direction)
        {
            return;
        }
        direction = newDirection;
        switch (direction)
        {
            case Direction.Left:
                playerRigid.velocity = DEFAULT_SPEED * Vector2.left;
                break;
            case Direction.Right:
                playerRigid.velocity = DEFAULT_SPEED * Vector2.right;
                break;
            case Direction.Stay:
                playerRigid.velocity = Vector2.zero;
                break;
        }
    }

    public Vector2 GetVelocity()
    {
        return playerRigid.velocity;
    }

    public enum Direction
    {
        Left = -1, Stay = 0, Right = 1
    }
}
