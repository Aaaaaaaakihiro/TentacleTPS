using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    [SerializeField]
    float speed = 6f; //移動スピードの定義

    Vector3 movement; //プレイヤーの動きを三次元ベクトルで定義
    Animator anim; //アニメーターコンポーネントを参照
    Rigidbody playerRigidbody; //PlayerのRigidbodyを参照
    Plane plane = new Plane();
    float distance = 0;

    void Awake()
    {
        //参照挙動をセットアップ
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //入力されたaxisを保持
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //シーン上でプレイヤーを動かす
        Move (h, v);
        Turning ();
        Animating (h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition (transform.position + movement);
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
        anim.SetBool ("IsWalking", walking);
    }
}