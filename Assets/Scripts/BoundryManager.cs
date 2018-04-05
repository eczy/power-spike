using UnityEngine;

public class BoundryManager : MonoBehaviour
{
    private static BoundryManager instance;
    public Transform topBoundry;
    public Transform bottomBoundry;
    public Transform leftBoundry;
    public Transform rightBoundry;
    
    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public static bool OutOfBounds(Vector3 position)
    {
        return GetOutOfBoundsDirection(position) != Vector3.zero;
    }

    /// <summary>
    /// Returns the direction that the player is out of bounds in.
    /// A return value of Vector3.zero indicates that the player is within the boundries.
    /// </summary>
    /// <returns>The direction that the player is out of bounds in</returns>
    public static Vector3 GetOutOfBoundsDirection(Vector3 position)
    {
        Vector3 direction = Vector3.zero;
        
        if (OutOfBoundsTop(position))
        {
            direction = Vector3.up;
        } else if (OutOfBoundsBottom(position))
        {
            direction = Vector3.down;
        } else if (OutOfBoundsLeft(position))
        {
            direction = Vector3.left;
        } else if (OutOfBoundsRight(position))
        {
            direction = Vector3.right;
        }

        return direction;
    }

    private static bool OutOfBoundsTop(Vector3 position)
    {
        return position.y > instance.topBoundry.position.y;
    }

    private static bool OutOfBoundsBottom(Vector3 position)
    {
        return position.y < instance.bottomBoundry.position.y;
    }

    private static bool OutOfBoundsLeft(Vector3 position)
    {
        return position.x < instance.leftBoundry.position.x;
    }
    
    private static bool OutOfBoundsRight(Vector3 position)
    {
        return position.x > instance.rightBoundry.position.x;
    }
}
