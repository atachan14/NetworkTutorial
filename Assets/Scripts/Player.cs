using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] float m_moveSpeed = 1;

    private Rigidbody rb;
    private Vector2 m_moveInput = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            SetMoveInputServerRpc(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));
        }

        if(IsServer)
        {
            ServerUpdate();
        }
    }

    [ServerRpc]
    private void SetMoveInputServerRpc(float x,float y)
    {
        m_moveInput = new Vector2(x,y);
    }

    private void ServerUpdate()
    {
        var velocity = Vector3.zero;
        velocity.x = m_moveSpeed * m_moveInput.normalized.x;
        velocity.z = m_moveSpeed * m_moveInput.normalized.y;

        rb.AddForce(velocity);
    }
}
