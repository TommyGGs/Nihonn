using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : Unity.Netcode.NetworkBehaviour
{
    // Start is called before the first frame update
    int mouseClicks = 0;
    private Vector3 moveInput;
    private Quaternion rotation;

    void Spin()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint
        (Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsOwner)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            transform.position += new Vector3(horizontal / 100, 0, vertical / 100);

            if (Input.GetMouseButtonDown(0))
            {
                mouseClicks++;
            }

            if (mouseClicks == 0)
            {
                Spin();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

            if (transform.position.y <= -4)
            {
                Debug.Log("gameOver!!");
                Destroy(gameObject);
            }
        }

    }

    [Unity.Netcode.ServerRpc]
    private void SetMoveInputServerRpc(float x, float z)
    {
        this.moveInput = new Vector3(x, 0, z);
    }

    [Unity.Netcode.ServerRpc]
    private void SetRotationServerRpc(Quaternion rotation)
    {
        this.rotation = rotation;
    }
}
