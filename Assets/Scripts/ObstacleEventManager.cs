using UnityEngine;

public class ObstacleEventManager : MonoBehaviour
{
    public delegate void OnCollideWithObstacle(GameObject obstacle);
    public OnCollideWithObstacle onCollideWithObstacle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onCollideWithObstacle?.Invoke(collision.gameObject);
        }
    }
}
