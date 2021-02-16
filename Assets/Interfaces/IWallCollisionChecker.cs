namespace Interfaces
{
    public enum CollisionDirection
    {
        Left, Right, None
    }
    public interface IWallCollisionChecker
    {
        bool IsTouchingWall();
        CollisionDirection GetCollisionDirection();
    }
}