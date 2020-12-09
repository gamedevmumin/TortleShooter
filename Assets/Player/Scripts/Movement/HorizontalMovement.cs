using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement : MonoBehaviour, IMovement
{
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 input, float speed)
    {
        rb.velocity = new Vector2(input.x * speed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
