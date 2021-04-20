using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform leftEye;

    [SerializeField] private Transform rightEye;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float timeBetweenShots = 0.4f;

    private float nextShotTime = 0.0f;

    public void Attack()
    {
        if (stats.IsDead) return;
        var rb = Instantiate(bulletPrefab, leftEye.position, leftEye.rotation).GetComponent<Rigidbody2D>();
        rb.AddForce((Vector2.up * 5f + Vector2.left * 2f), ForceMode2D.Impulse);
        rb = Instantiate(bulletPrefab, rightEye.position, rightEye.rotation).GetComponent<Rigidbody2D>();
        rb.AddForce((Vector2.up * 5f + Vector2.right * 2f), ForceMode2D.Impulse);
    }
    
/*
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + timeBetweenShots;
            var rb = Instantiate(bulletPrefab, leftEye.position, leftEye.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce((Vector2.up*5f+Vector2.left*2f), ForceMode2D.Impulse);
            rb = Instantiate(bulletPrefab, rightEye.position, rightEye.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce((Vector2.up*5f+Vector2.right*2f), ForceMode2D.Impulse);
            //Bullet bullet = Instantiate(bulletPrefab, leftEye.position, leftEye.rotation).GetComponent<Bullet>();
            //bullet.Initialize(1,1,0);
            //bullet = Instantiate(bulletPrefab, rightEye.position, rightEye.rotation).GetComponent<Bullet>();
            //bullet.Initialize(1,1,0);
        }
    }*/
}
