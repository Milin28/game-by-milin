
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // คำนวณทิศทางการเดินตามทิศที่ตัวละครหันอยู่
        Vector3 move = transform.right * x
                     + transform.forward * z;

        move = Vector3.ClampMagnitude(move, 1f);

        // กด Left Shift เพื่อวิ่ง
        float currentSpeed = Input.GetKey(KeyCode.LeftShift)
            ? runSpeed
            : walkSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // ป้องกันตัวละครลอยเมื่อยืนอยู่บนพื้น
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        // แรงโน้มถ่วง
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
