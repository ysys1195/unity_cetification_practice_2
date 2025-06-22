using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleAlternative : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player collided with obstacle: " + gameObject.name);
        }
    }
}
