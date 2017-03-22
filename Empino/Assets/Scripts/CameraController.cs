using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject _followTarget;
    public float _moveSpeed;

    private Vector3 targetPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPos = new Vector3(_followTarget.transform.position.x, _followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, _moveSpeed * Time.deltaTime);
    }
}
