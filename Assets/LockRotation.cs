using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        // 位置固定、回転の X と Y を固定。
        m_rigidbody.constraints =
        RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezePositionY;
    }
}
