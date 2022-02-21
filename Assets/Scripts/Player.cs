using UnityEngine;

[RequireComponent(typeof(Behaviour2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Behaviour2D behaviour;
    private Vector2 movement;

    private void Start()
    {
        behaviour = GetComponent<Behaviour2D>();
    }

    private void Update()
    {
        Movement();
        behaviour.ApplyGravity(ref movement);
        behaviour.Translate(movement * Time.deltaTime);
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        movement.x = x * speed;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            movement.y = 10f;
        }
    }
}
