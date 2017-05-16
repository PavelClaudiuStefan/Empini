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

    //For animation
    private Animator anim;
    bool entityMoving;
    Vector2 lastMove;

    Coroutine currentNotYet;

    void Start()
    {
        if (server)
        {
            StartCoroutine(GenerateRot());
        }
        anim = GetComponent<Animator>();
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
        if (server)
        {
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

            
            transform.position += toPos * Time.deltaTime * speed / 100;

            manager.SendMovement(this, transform.position, transform.rotation, toPos);

            AnimateEntity(toPos.x, toPos.y);
        }
        else
        {
            transform.position = posFromServer;
            transform.rotation = rotFromServer;

            AnimateEntity(toPosFromServer.x, toPosFromServer.y);
        }

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
        if (!isLocalPlayer)
            return;

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
    
    void AnimateEntity(float x, float y)
    {
        //if (!isLocalPlayer)
        //    return;

        entityMoving = false;

        if (x != 0 || y != 0)
        {
            entityMoving = true;
            lastMove = new Vector2(x, y);
        }
        anim.SetFloat("MoveX", x);
        anim.SetFloat("MoveY", y);
        anim.SetBool("PlayerMoving", entityMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
