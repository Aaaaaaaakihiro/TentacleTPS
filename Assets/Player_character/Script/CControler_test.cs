using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CControler_test : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 6f; //移動スピード

    Vector3 forward, right; //縦横移動のベクトル
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    Plane plane = new Plane();
    float distance = 0;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void FixedUpdate()
    {
        //入力されたaxisを保持
        float h = Input.GetAxisRaw("HorizontalKey");
        float v = Input.GetAxisRaw("VerticalKey");

        //シーン上でプレイヤーを動かす
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        //Vector3 direction = new Vector3(h, 0f, v);
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * h;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * v;
        movement = rightMovement + upMovement;
        /*Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;*/

        playerRigidbody.MovePosition(transform.position + movement);

    }

    void Turning()
    {
        // カメラとマウスの位置を元にRayを準備
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {
            // 距離を元に交点を算出して、交点の方を向く
            var lookPoint = ray.GetPoint(distance);
            lookPoint.y = 0;
            transform.LookAt(lookPoint);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

}
