using UnityEngine;

// プレイヤーをノックバックさせる
[RequireComponent(typeof(Rigidbody), typeof(ObstacleEventManager))]
public class PlayerKnockbackHandler : MonoBehaviour
{
    public float knockbackForce = 10f;
    private Rigidbody rb;
    private ObstacleEventManager obstacleEventManager;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        obstacleEventManager = GetComponent<ObstacleEventManager>();
        obstacleEventManager.onCollideWithObstacle += HandleKnockBack;
    }

    public void HandleKnockBack(GameObject obstacle)
    {
        Vector3 direction = (transform.position - obstacle.transform.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        if (obstacleEventManager != null)
        {
            obstacleEventManager.onCollideWithObstacle -= HandleKnockBack;
        }
    }
}
