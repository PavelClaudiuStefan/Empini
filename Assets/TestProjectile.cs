using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour {
    public GameObject roundSendPoint;
    public Transform sendPoint;
    public Rigidbody2D projectile;

    public float projectileSpeed;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dir = (mousePos - transform.position).normalized;
            Rigidbody2D temp = Instantiate(projectile, sendPoint.position, Quaternion.Euler(Vector3.zero));
            temp.velocity = dir * projectileSpeed;
            Physics2D.IgnoreCollision(temp.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        }

        float rotationZ = Mathf.Atan2((mousePos - transform.position).y, (mousePos - transform.position).x) * Mathf.Rad2Deg;
        roundSendPoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
