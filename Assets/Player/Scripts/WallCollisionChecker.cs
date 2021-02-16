using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Player.Scripts
{
    public class WallCollisionChecker : MonoBehaviour, IWallCollisionChecker
    {
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform leftCheck;
        [SerializeField] private Transform rightCheck;
        [SerializeField] private float checkRadius = 0.2f;

        public bool IsTouchingWall()
        {
            return Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround) ||
                   Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround);
        }

        public CollisionDirection GetCollisionDirection()
        {
            if (Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround)) return CollisionDirection.Left;
            return Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround)
                ? CollisionDirection.Right
                : CollisionDirection.None;
        }
    }
}