using UnityEngine;

public class TempPc : MonoBehaviour
{
    private PlayerMovement playerInput;

    private Rigidbody rb;

    [SerializeField]
    private float playerSpeed;

    private float angle;

    private Quaternion targetRotation;

    private Vector2 input;

    private Transform cam;

    void Awake()
    {
        playerInput = new PlayerMovement();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        GetInput();

        if (input.x == 0 && input.y == 0) return;

        CalculateDirection();
        Rotate();
        Move();
    }

    void GetInput()
    {
        //input.x = playerInput.Player.Move.ReadValue<Vector2>().x;
        //input.y = playerInput.Player.Move.ReadValue<Vector2>().y;
    }

    void CalculateDirection()
    {
        if (input.sqrMagnitude > 1.0f)
            input = input.normalized;

        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = targetRotation;
    }

    void Move()
    {
        //transform.position += transform.forward * 5 * Time.deltaTime;
        rb.velocity = transform.forward * 200 * Time.fixedDeltaTime;
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }
}