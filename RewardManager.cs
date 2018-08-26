using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour {

    private Transform m_Transform;
    private GameObject prefab_reward;

    private int rewardCount = 0;  //当前场景中存在多少奖励物品
    private int rewardMaxCount = 3;  //当前场景中最多存在多少奖励物品

	void Start () {

        m_Transform = gameObject.GetComponent<Transform>();
        prefab_reward = Resources.Load<GameObject>("reward");

        //调用生成奖励物品
        InvokeRepeating("CreateReward", 5, 5);
	}
	
    private void CreateReward()
    {
        //存在的奖励物品不能超过最大值
        if (rewardCount < rewardMaxCount) 
        {
            Vector3 pos = new Vector3(Random.Range(-640, 490), 0, Random.Range(-465, 475));
            GameObject.Instantiate(prefab_reward, pos, Quaternion.identity, m_Transform);
            rewardCount++;
        }
    }

    public void StopCreate()
    {
        CancelInvoke();
    }
	
    /// <summary>
    /// 当前的奖励物品数量自减
    /// </summary>
	public void RewardCountDown()
    {
        rewardCount--;
    }
}
