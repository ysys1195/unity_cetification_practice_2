using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleAlternative : MonoBehaviour
{

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player collided with obstacle: " + gameObject.name);
        }
    }
}
