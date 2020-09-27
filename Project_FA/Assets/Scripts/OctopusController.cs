using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusController : MonoBehaviour
{
    public float moveSpeed;
    public float lineOfSight;
    public float shootRange;
    public float fireRate;
    private float _nextFireTime;
    public GameObject enemyBulletPrefab;
    public GameObject enemyBulletSpawn;
    private Transform _player;
    public GameObject enemyDeathAnim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(_player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootRange)
        {
            transform.position = Vector2.MoveTowards
                (this.transform.position, _player.position, moveSpeed * Time.deltaTime);
        }
        
        else if (distanceFromPlayer <= shootRange && _nextFireTime < Time.time)
        {
            Instantiate(enemyBulletPrefab, enemyBulletSpawn.transform.position, enemyBulletSpawn.transform.rotation);
            _nextFireTime = Time.time + fireRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(gameObject);

            Instantiate(enemyDeathAnim, transform.position, transform.rotation);
        }
    }
}
