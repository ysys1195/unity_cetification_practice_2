using UnityEngine;

public class RemovableObstacle : ObstacleAlternative
{
    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        Destroy(this.gameObject);
    }
}
