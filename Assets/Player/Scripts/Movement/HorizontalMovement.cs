using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement : MonoBehaviour, IMovement
{
    private Rigidbody2D rb;
    private IWallCollisionChecker wallCollisionChecker;
        
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallCollisionChecker = GetComponent<IWallCollisionChecker>();
    }
    public void Move(Vector2 input, float speed)
    {
        if (wallCollisionChecker?.GetCollisionDirection()==CollisionDirection.Left && input.x<0f ||
            wallCollisionChecker?.GetCollisionDirection()==CollisionDirection.Right && input.x>0f) return;
        rb.velocity = new Vector2(input.x * speed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
