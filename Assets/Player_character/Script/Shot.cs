using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    GameObject bullet;//弾丸のプレハブ
    GameObject tentacle;//触手弾のプレハブ

    Transform muzzle;//弾丸の発射点

    [SerializeField]
    private float speed = 1000;//弾丸の速度
    [SerializeField]
    private float rate = 1.0f;//弾丸のレート
    [SerializeField]
    private float tentacleSpeed = 2000;

    string bulletKind;//弾の種類

    private bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;//GenerateBulletが実行されていないことを表す

        bullet = Resources.Load("Prefabs/Bullet") as GameObject;//弾のプレハブ
        tentacle = Resources.Load("Prefabs/Tentacle") as GameObject;//触手のプレハブ

        muzzle = transform.Find("Gun/Muzzle");//マズルの位置
    }

    IEnumerator GenerateBullet(string bulletKind)
    {
        if (isRunning) yield break;//フレーム毎に呼び出されるコルーチンが実行中の場合はGenerateBulletを実行しない
        isRunning = true;//GenerateBulletを実行中…

        //Debug.Log("Wait Seconds...");

        if (bulletKind == "NormalBullet")
        {
            //弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;

            Vector3 force;//発射する強さ
            force = this.gameObject.transform.forward * speed;

            //Rigidobodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            //弾丸の位置を調整
            bullets.transform.position = muzzle.position;

            yield return new WaitForSeconds(rate);//弾丸のレート分コルーチンでインターバルを開ける
        }
        else if (bulletKind == "TentacleBullet")
        {
            GameObject tentacles = Instantiate(tentacle) as GameObject;

            Vector3 force;
            force = this.gameObject.transform.forward * speed;

            tentacles.GetComponent<Rigidbody>().AddForce(force);

            tentacles.transform.position = muzzle.position;
        }

        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bulletKind = "NormalBullet";

            StartCoroutine("GenerateBullet", bulletKind);//弾を発射する

            bulletKind = "";
        }else if (Input.GetMouseButton(1))
        {
            bulletKind = "TentacleBullet";

            StartCoroutine("GenerateBullet", bulletKind);//触手を発射

            bulletKind = "";
        }

        
    }
}
