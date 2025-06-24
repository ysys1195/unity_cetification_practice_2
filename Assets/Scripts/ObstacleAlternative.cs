using UnityEngine;

public class ObstacleAlternative : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HitPlayer(collision.gameObject);
        }
    }

    public virtual void HitPlayer(GameObject player)
    {
        print("I hit the player!");
    }

}
