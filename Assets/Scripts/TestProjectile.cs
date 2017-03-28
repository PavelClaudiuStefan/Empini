using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestProjectile : NetworkBehaviour
{
    public GameObject roundSendPoint;

    public Transform sendPoint;

    public GameObject projectile;

    public float projectileSpeed;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            CmdShot(mousePos, sendPoint.position);
        }

        float rotationZ = Mathf.Atan2((mousePos - transform.position).y, (mousePos - transform.position).x) * Mathf.Rad2Deg;
        roundSendPoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    [Command]
    void CmdShot(Vector3 mousePos, Vector3 sendPoint)
    {
        Vector3 dir = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(projectile, sendPoint, Quaternion.Euler(Vector3.zero));
        bullet.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;
        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());

        GameObject.Destroy(bullet, 2);

        NetworkServer.Spawn(bullet);
    }
}
