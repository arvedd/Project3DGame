using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMover : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float turnSpeed = 5f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("Player tidak ditemukan di scene!");
        }
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            // Rotasi smooth
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Gravity
            if (controller.isGrounded)
                verticalVelocity = -2f;
            else
                verticalVelocity -= gravity * Time.deltaTime;

            Vector3 move = transform.forward * speed + Vector3.up * verticalVelocity;
            controller.Move(move * Time.deltaTime);
        }
    }
}
