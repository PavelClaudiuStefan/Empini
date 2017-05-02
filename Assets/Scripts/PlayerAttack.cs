using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour {

    public GameObject roundSendPoint;
    public Transform sendPoint;
    public GameObject projectile;

    private PlayerStats playerStats;


    private float unicCode;

    public void Init(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
        Debug.Log(playerStats);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Shot(mousePos, sendPoint.position);

            if (isServer)
                RpcShot(mousePos, sendPoint.position, -1);
            else
            {
                unicCode = Random.Range(0f, 1f);

                CmdShot(mousePos, sendPoint.position , unicCode);
            }
        }

        float rotationZ = Mathf.Atan2((mousePos - transform.position).y, (mousePos - transform.position).x) * Mathf.Rad2Deg;
        roundSendPoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    void Shot(Vector3 mousePos, Vector3 sendPoint)
    {
        Vector3 dir = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(projectile, sendPoint, Quaternion.Euler(Vector3.zero));

        bullet.GetComponent<Rigidbody2D>().velocity = dir * playerStats.BulletSpeed;
        bullet.GetComponent<ProjectileStats>().Init(playerStats.BulletDamage);

        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        GameObject.Destroy(bullet, 2);
    }

    [Command]
    void CmdShot(Vector3 mousePos, Vector3 sendPoint , float unicCode)
    {
        Vector3 dir = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(projectile, sendPoint, Quaternion.Euler(Vector3.zero));

        bullet.GetComponent<Rigidbody2D>().velocity = dir * playerStats.BulletSpeed;
        bullet.GetComponent<ProjectileStats>().Init(playerStats.BulletDamage);

        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        GameObject.Destroy(bullet, 2);

        RpcShot(mousePos, sendPoint , unicCode);
    }

    [ClientRpc]
    void RpcShot(Vector3 mousePos, Vector3 sendPoint, float unicCode)
    {
        if (!isServer && unicCode != this.unicCode)
        {
            Vector3 dir = (mousePos - transform.position).normalized;
            GameObject bullet = Instantiate(projectile, sendPoint, Quaternion.Euler(Vector3.zero));

            bullet.GetComponent<Rigidbody2D>().velocity = dir * playerStats.BulletSpeed;
            bullet.GetComponent<ProjectileStats>().Init(playerStats.BulletDamage);

            Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            GameObject.Destroy(bullet, 2);
        }
    }
}
