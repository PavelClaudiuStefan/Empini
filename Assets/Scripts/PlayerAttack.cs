using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour {

    public GameObject roundSendPoint;
    public Transform sendPoint;
    public GameObject projectile;
    public GameObject[] projectiles;

    public AudioClip myAudio;

    private PlayerStats playerStats;


    private float unicCode;

    public void Init(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
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

        bullet.GetComponent<ProjectileStats>().Init(playerStats.BulletDamage, playerStats);

        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());

        AudioSource.PlayClipAtPoint(myAudio, transform.position);

        GameObject.Destroy(bullet, 2);
    }

    [Command]
    void CmdShot(Vector3 mousePos, Vector3 sendPoint , float unicCode)
    {
        Vector3 dir = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(projectile, sendPoint, Quaternion.Euler(Vector3.zero));

        bullet.GetComponent<Rigidbody2D>().velocity = dir * playerStats.BulletSpeed;
        bullet.GetComponent<ProjectileStats>().Init(playerStats.BulletDamage,playerStats);

        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        GameObject.Destroy(bullet, 2);

        RpcShot(mousePos, sendPoint , unicCode);

        AudioSource.PlayClipAtPoint(myAudio, transform.position);
    }

    [ClientRpc]
    void RpcShot(Vector3 mousePos, Vector3 sendPoint, float unicCode)
    {
        if (!isServer && unicCode != this.unicCode)
        {
            Vector3 dir = (mousePos - transform.position).normalized;
            GameObject bullet = Instantiate(projectile, sendPoint, Quaternion.Euler(Vector3.zero));

            bullet.GetComponent<Rigidbody2D>().velocity = dir * playerStats.BulletSpeed;
            bullet.GetComponent<ProjectileStats>().Init(playerStats.BulletDamage, playerStats);

            Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            GameObject.Destroy(bullet, 2);

            AudioSource.PlayClipAtPoint(myAudio, transform.position);
        }
    }

    public void updateProjectileSprite(int tier)
    {
        switch(tier)
        {
            case 5:
                projectile = projectiles[0];
                break;
            case 6:
                projectile = projectiles[1];
                break;
            case 7:
                projectile = projectiles[2];
                break;
            case 8:
                projectile = projectiles[3];
                break;
            case 9:
                projectile = projectiles[4];
                break;
            case 10:
                projectile = projectiles[5];
                break;
            case 11:
                projectile = projectiles[6];
                break;
            default:
                Debug.Log("BulletDamage out of bounds. Change the tiers in PlayerAttack.cs!");
                break;
        }
    } 
}
