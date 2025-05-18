using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Object player;
    [SerializeField] private Transform groundCheck; // 足元の位置
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    void FixedUpdate()
    {
        Move();
        // Debug.Log(this.transform.eulerAngles);
        Debug.Log(IsGrounded());
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

    private bool IsGrounded()
    {
        // 左右2点からレイを飛ばして接地をチェック
        float raySpacing = 0.3f;
        Vector3 originLeft = transform.position + transform.right * raySpacing;
        Vector3 originRight = transform.position - transform.right * raySpacing;
        Vector3 originCenter = transform.position;

        return
            Physics.Raycast(originLeft, Vector3.down, checkDistance, groundLayer) ||
            Physics.Raycast(originCenter, Vector3.down, checkDistance, groundLayer) ||
            Physics.Raycast(originRight, Vector3.down, checkDistance, groundLayer);
    }
}
