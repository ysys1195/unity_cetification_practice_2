using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Object player;

    void FixedUpdate()
    {
        Move();
        Debug.Log(this.transform.eulerAngles);
    }

    private void Move()
    {
        float moveHorizontal = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.AddForce(movement *  100f, ForceMode.Force);
        rb.MoveRotation(Quaternion.Euler(0, moveHorizontal + this.transform.eulerAngles.y, 0));

        
    }
}
