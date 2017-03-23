using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject _followTarget;
    public float _moveSpeed;

    private Vector3 targetPos;

    public void InjectPlayer(GameObject target)
    {
        _followTarget = target;
    }

    void FixedUpdate()
    {
        if (_followTarget)
        {
            targetPos = new Vector3(_followTarget.transform.position.x, _followTarget.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, _moveSpeed * Time.deltaTime);
        }
    }
}
