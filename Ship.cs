using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    private bool isLeft = false;
    private bool isRight = false;
    private bool isDeath = false;

    private MissileManager m_MissileManager;

    private GameObject Explode03;  //特效文件

    private int rewardNum = 0;  //本局吃掉的奖励物品数量

    private Transform m_Transform;

    private GameUIManager m_GameUIManager;

    private int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private int rotate;
    public int Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value; }
    }
	
	void Start () {

        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        Explode03 = Resources.Load<GameObject>("Explode03");

        m_GameUIManager = GameObject.Find("UI Root").GetComponent<GameUIManager>();
    }
	
	void Update () {

        if (isDeath == false)
        {
            m_Transform.Translate(Vector3.forward * speed);

            if (isLeft)
            {
                m_Transform.Rotate(Vector3.up * -1 * rotate);
            }

            if (isRight)
            {
                m_Transform.Rotate(Vector3.up * 1 * rotate);
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Border")
        {
            isDeath = true;
            m_GameUIManager.ShowOverPanel();
        }

        //飞机和导弹相撞
        if (coll.tag == "Missile") 
        {
            isDeath = true;
            m_GameUIManager.ShowOverPanel();
            //播放特效
            GameObject.Instantiate(Explode03, m_Transform.position, Quaternion.identity);

            m_MissileManager.StopCreate();
            GameObject.Destroy(coll.gameObject);
            gameObject.SetActive(false);
        }

        if (coll.tag == "Reward")
        {
            rewardNum++;
            m_GameUIManager.UpdateScoreLabel(rewardNum);  //更新UI分数显示
            GameObject.Destroy(coll.gameObject);
        }
    }
}
