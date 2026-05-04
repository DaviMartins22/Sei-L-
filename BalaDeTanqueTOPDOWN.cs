using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Movimento do Corpo")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;

    [Header("Referências")]
    public Transform turret; // torre do tanque

    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        RotateTurret();
    }

    void FixedUpdate()
    {
        MoveTank();
    }

    void MoveTank()
    {
        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // Movimento para frente/trás
        Vector3 move = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Rotação do corpo
        float turn = turnInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void RotateTurret()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Detecta onde o mouse está no chão
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 direction = hit.point - turret.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                turret.rotation = Quaternion.Lerp(
                    turret.rotation,
                    lookRotation,
                    Time.deltaTime * 10f
                );
            }
        }
    }
}