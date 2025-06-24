using UnityEngine;

public class RemovableObstacle : ObstacleAlternative
{
    public override void HitPlayer(GameObject player)
    {
        base.HitPlayer(player);

        Destroy(this.gameObject);
    }
}
