using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnableObject
{
    public GameObject rotatingPoint;
    public GameObject followPoint;

    public float speed;

    private GameObject tempFollow;

    bool onPlayer;
    bool pasChange;

    bool notYet;

    Coroutine currentNotYet;

    void Start()
    {
        StartCoroutine(GenerateRot());
    }
    IEnumerator GenerateRot()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if(!pasChange && !onPlayer)
                rotatingPoint.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            pasChange = false;
        }
    }
    IEnumerator NotYet()
    {
        notYet = true;
        yield return new WaitForSeconds(1f);
        notYet = false;
    }
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);

        Vector3 toPos;

        if (onPlayer)
        {
            toPos = (tempFollow.transform.position - transform.position).normalized;

            Vector3 difference = tempFollow.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            rotatingPoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
        else
            toPos = (followPoint.transform.position - transform.position).normalized;

        transform.position += toPos * Time.deltaTime * speed/100;

    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        manager.ObjectHitted(col, this);
        /*if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }*/
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!onPlayer && col.tag == "Player")
        {
            onPlayer = true;
            tempFollow = col.gameObject;

            Vector3 difference = tempFollow.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            rotatingPoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        }
        if(col.tag == "Zone")
        {
            rotatingPoint.transform.Rotate(0, 0, 180);
            pasChange = true;
            onPlayer = false;

            if (currentNotYet != null)
                StopCoroutine(currentNotYet);

            currentNotYet = StartCoroutine(NotYet());

        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(!onPlayer && col.tag == "Player" && !notYet)
        {
            onPlayer = true;
            tempFollow = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            onPlayer = false;
        }
    }
}
