using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_camera : MonoBehaviour
{

    private GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用

    // Use this for initialization
    void Start()
    {

        //unitychanの情報を取得
        this.player = GameObject.Find("chr_sword_editted");

        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        //新しいトランスフォームの値を代入する
        //transform.position = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 2.0f * Time.deltaTime);

        //ユニティちゃんの向きと同じようにカメラの向きを変更する
        float x = 30;
        float y = 45;
        this.transform.rotation = Quaternion.Euler(x, y, 0.0f);

    }
}
