using UnityEngine;
using System.Collections;

/// <summary>
/// 导弹控制脚本
/// </summary>
public class Missile : MonoBehaviour {

    private Transform m_Transform;
    private Transform player_Transform;
    private GameObject Explode03;  //特效文件

    //导弹默认的前方
    private Vector3 normalForward = Vector3.forward;
	
	void Start () {

        m_Transform = gameObject.GetComponent<Transform>();
        player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Explode03 = Resources.Load<GameObject>("Explode03");
    }
	
	
	void Update () {

        m_Transform.Translate(Vector3.forward);

        //导弹追踪角色
        //导弹与角色的方向
        Vector3 dir = player_Transform.position - m_Transform.position;
        //插值计算当前导弹要旋转的角度
        normalForward = Vector3.Lerp(normalForward, dir, Time.deltaTime);
        //改变当前导弹的方向
        m_Transform.rotation = Quaternion.LookRotation(normalForward);
	}

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Missile")
        {
            //播放特效
            GameObject.Instantiate(Explode03, m_Transform.position, Quaternion.identity);

            GameObject.Destroy(gameObject);
        }
    }
}
