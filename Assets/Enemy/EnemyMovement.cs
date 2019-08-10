using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    Transform target;//プレイヤーキャラのトランスフォーム
    Slider hitPointBar;//hitPointBarのスライダー
    Transform cameraTarget;//カメラの位置 
    GameObject hpBarCanvas;//hpバーのカンバス


    [SerializeField]
    private float speed;//移動スピード

    [SerializeField]
    private float hitPoint;//敵のHP

    //targetと自分のVector
    Vector3 currentPosition;//現在の位置
    Vector3 targetPosition;//ターゲットの位置
    
    // Start is called before the first frame update
    void Start()
    {
        //ステータスの初期設定
        speed = 1.0f;
        hitPoint = 1000;

        //スクリプトで扱うゲームオブジェクトの定義
        hpBarCanvas = transform.Find("Canvas").gameObject;
        cameraTarget = GameObject.FindWithTag("MainCamera").transform;
        target = GameObject.FindWithTag("Player").transform;
        hitPointBar = transform.Find("Canvas/hitPointBar").gameObject.GetComponent<Slider>();
        hitPointBar.maxValue = hitPoint;//HPとHPバーの同期
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hitPoint -= 1.0f;//弾が当たったらHPを1減らす
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoint <= 0.0f)
        {
            Destroy(gameObject, 0.0f);//HPがゼロになったらオブジェクトを破壊
        }

        //HPバーに関する記述
        hpBarCanvas.transform.rotation = cameraTarget.rotation;//HPバーを常にカメラの方へ向ける
        hitPointBar.value = hitPoint;//HPバーとHPの値を連動

        //敵の歩行に関する記述
        float step = speed * Time.deltaTime;//歩幅・歩く速さ
        currentPosition = transform.position;//現在の位置を値として保持
        targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);//プレイヤーの位置を一旦保存

        //実際の動き
        transform.LookAt(targetPosition);//プレイヤーの方を向く
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, step);//プレイヤーの方へ歩かせる
        
    }
}
