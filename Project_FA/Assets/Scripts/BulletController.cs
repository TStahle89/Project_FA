using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D _myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        
        _myRigidbody.velocity = transform.right * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime, 0f,0f);
        
        // Destroy bullets when exiting camera view
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width || screenPosition.x < 0 || screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
