using UnityEngine;

public class CoreGame : MonoBehaviour
{
    private GameObject chopstick1;
    private GameObject chopstick2;
    private bool isDropped = false;
    [SerializeField] private int speed = 5;

    private int mouseClicks = 0;

    private void Start()
    {
        chopstick1 = GameObject.Find("Chopstick1(Clone)");
        chopstick2 = GameObject.Find("Chopstick2(Clone)");
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        CheckGameOver();
    }


    private void HandleMovement()
    {
        if (!isDropped && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            Vector3 direction = Input.GetKey(KeyCode.RightArrow) ? Vector3.right : Vector3.left;
            MoveObjects(direction);
        }
    }

    private void ChopsticksAppear()
    {
        chopstick1.GetComponent<SpriteRenderer>().enabled = true;
        chopstick2.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void MoveObjects(Vector3 direction)
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.position += movement;
        chopstick1.transform.position += movement;
        chopstick2.transform.position += movement;
    }

    private void ChangeChopsticksRotation()
    {
        bool isMyTurn = GameObject.Find("Main Camera").GetComponent<SettingUpGame>().isMyTurn;
        float rotateFloat = 0f;
        if (!isMyTurn)
        {
            rotateFloat = 180f;
        } 
        chopstick1.transform.rotation = Quaternion.Euler(0f, rotateFloat, -6.5f);
        chopstick2.transform.rotation = Quaternion.Euler(0f, rotateFloat, 9.67f);

    }

    private void HandleRotation()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ChopsticksAppear();
            mouseClicks++;
        }

        if (mouseClicks == 0)
        {
            Spin();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isDropped = true;
            ChangeChopsticksRotation();
        }
    }

    private void Spin()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void CheckGameOver()
    {
        if (transform.position.y <= -4)
        {
            Debug.Log("gameOver!!");
            Destroy(gameObject);
        }
    }
}
