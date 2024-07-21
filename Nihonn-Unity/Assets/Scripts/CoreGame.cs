using UnityEngine;

public class CoreGame : MonoBehaviour
{
    private GameObject chopstick1;
    private GameObject chopstick2;
    private bool isDropped = false;
    [SerializeField] private int speed = 5;
    private  SettingUpGame settingScript;

    private int mouseClicks = 0;
    private int spaceKeyClicked = 0;

    private void Start()
    {
        chopstick1 = GameObject.Find("Chopstick1(Clone)");
        chopstick2 = GameObject.Find("Chopstick2(Clone)");
        settingScript = GameObject.Find("Main Camera").GetComponent<SettingUpGame>();
    }

    private void Update()
    {
        // here if my turn
        if (settingScript.CurrentPlayerID == settingScript.PlayerID)
        {
            HandleMovement();
            HandleRotation();
        } 
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
        chopstick1 = GameObject.Find("Chopstick1(Clone)");
        chopstick2 = GameObject.Find("Chopstick2(Clone)");
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
        Debug.Log("telling chopsticks opened");
        //changed here
        if (!settingScript.chopsticksOpen) {
            settingScript.chopsticksOpen = true;
        } else {
            settingScript.chopsticksOpen = false;
        }

        chopstick1.transform.rotation = Quaternion.Euler(0f, settingScript.rightTurn ? 180f: 0f, -6.5f);
        chopstick2.transform.rotation = Quaternion.Euler(0f, settingScript.rightTurn ? 180f: 0f, 9.67f);

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

        if (Input.GetKeyUp(KeyCode.Space) && spaceKeyClicked == 0)
        {
            spaceKeyClicked ++;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isDropped = true;
            Debug.Log("yes space key");
            ChangeChopsticksRotation();
            
            // also changed here
            // settingScript.chopsticksOpen = false;

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
            // because switches turn when chopsticks opens 
            // so when its dropped and its your fault, it is is the other person's "isMyTurn"
            // so when objects fall out during isMyTurn == true, its your win
            // here isMyturn is fine but have to set it up in settingupgame
            settingScript.gameOver = true;
            settingScript.youWin = settingScript.isMyTurn ? true: false;
        }
    }
}
