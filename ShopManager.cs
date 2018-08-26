using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 商城模块总控制器
/// </summary>
public class ShopManager : MonoBehaviour {

    //商城数据对象
    private ShopData shopData;

    //xml路径
    private string xmlPath = "Assets/Datas/ShopData.xml";
    //xml存档路径
    private string savePath = "Assets/Datas/SaveData.xml";

    //商城商品模板
    private GameObject ui_ShopItem;

    private GameObject leftButton;
    private GameObject rightButton;
    //商城商品UI的集合
    private List<GameObject> shopUI = new List<GameObject>();

    //要展现的UI的索引
    private int index = 0;

    private UILabel starNum;
    private UILabel scoreNum;

    //引用
    private StartUIManager m_StartUIManager;

    void Start () {
        //实例化商城数据对象
        shopData = new ShopData();
        //加载xml
        shopData.ReadXMLByPath(xmlPath);
        shopData.ReadScoreAndGold(savePath);

        ui_ShopItem = Resources.Load<GameObject>("UI/ShopItem");

        m_StartUIManager = GameObject.Find("UI Root").GetComponent<StartUIManager>();

        //按钮事件绑定
        leftButton = GameObject.Find("LeftButton");
        UIEventListener.Get(leftButton).onClick = LeftButtonClick;
        rightButton = GameObject.Find("RightButton");
        UIEventListener.Get(rightButton).onClick = RightButtonClick;

        //同步UI与XML的数据
        starNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        scoreNum = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();
        //读取PlayerPrefs中的新的最高分
        int temHighestScore = PlayerPrefs.GetInt("HighestScore", 0);
        if (temHighestScore > shopData.highScore)
        {
            //更新UI
            UpdateUIHighestScore(temHighestScore);
            //更新XML，存储新的最高分
            shopData.UpdateXMLData(savePath, "HeightScore", temHighestScore.ToString());
            //清空PlayerPrefs
            PlayerPrefs.SetInt("HeightScore", 0);
        }
        else
        {
            //更新UI
            UpdateUIHighestScore(shopData.highScore);
        }

        //读取PlayerPrefs中的金币数
        int tempGold = PlayerPrefs.GetInt("GoldNum", 0);
        if (tempGold > 0)
        {
            int gold = shopData.goldCount + tempGold;
            //更新UI
            UpdateUIGold(gold);
            //更新XML中的存储
            shopData.UpdateXMLData(savePath, "GoldCount", gold.ToString());
            //清空PlayerPrefs
            PlayerPrefs.SetInt("GoldNum", 0);
        }
        else
        {
            //更新UI
            UpdateUIGold(shopData.goldCount);
        }

        SetPlayerInfo(shopData.shopList[0]);

        CreateAllShopUI();
	}

    /// <summary>
    /// 更新最高分UI
    /// </summary>
    private void UpdateUIHighestScore(int highestScore)
    {
        scoreNum.text = highestScore.ToString();
    }

    /// <summary>
    /// 更新金币数UI
    /// </summary>
    private void UpdateUIGold(int gold)
    {
        starNum.text = gold.ToString();
    }

    /// <summary>
    /// 更新UI数据显示
    /// </summary>
    private void UpdateUI()
    {
        starNum.text = shopData.goldCount.ToString();
        scoreNum.text = shopData.highScore.ToString();
    }

    /// <summary>
    /// 创建商城模块中所有的商品
    /// </summary>
    private void CreateAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            //实例化单个商品UI
            GameObject item = NGUITools.AddChild(gameObject, ui_ShopItem);
            
            //加载对应的飞机模型
            GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);
            //给商品UI元素赋值
            item.GetComponent<ShopItemUI>().SetUIValue(shopData.shopList[i].Id, shopData.shopList[i].Speed, shopData.shopList[i].Rotate, shopData.shopList[i].Price, ship, shopData.shopState[i]);

            //将生成的UI添加到集合中，进行管理
            shopUI.Add(item);
        }

        //隐藏UI
        ShopUIHideAndShow(index);
    }

    private void LeftButtonClick(GameObject go)
    {
        if (index > 0) 
        {
            index--;
            int temp = shopData.shopState[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
        }
    }

    private void RightButtonClick(GameObject go)
    {
        if (index < shopUI.Count - 1) 
        {
            index++;
            int temp = shopData.shopState[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
        }
    }

    /// <summary>
    /// 商城UI的显示与隐藏
    /// </summary>
    private void ShopUIHideAndShow(int index)
    {
        for (int i = 0; i < shopUI.Count; i++)
        {
            shopUI[i].SetActive(false);
        }

        shopUI[index].SetActive(true);
    }

    /// <summary>
    /// 计算商品价格
    /// </summary>
    /// <param name="item"></param>
    private void CalcItemPrice(ShopItemUI item)
    {
        if (shopData.goldCount >= item.itemPrice)
        {
            Debug.Log("购买成功");
            item.BuyEnd();                             //隐藏购买UI按钮
            shopData.goldCount -= item.itemPrice;      //减去已经消耗的金币
            UpdateUI();                                //更新UI  
            shopData.UpdateXMLData(savePath, "GoldCount", shopData.goldCount.ToString());
            shopData.UpdateXMLData(savePath, "ID" + item.itemId, "1");      //更新商品状态
        }
        else
        {
            Debug.Log("购买失败，金币不够");
        }
    }

    /// <summary>
    /// 存储当前角色信息到PlayerPrefs中去
    /// </summary>
    private void SetPlayerInfo(ShopItem item)
    {
        PlayerPrefs.SetString("PlayerName", item.Model);
        PlayerPrefs.SetInt("PlayerSpeed", int.Parse(item.Speed));
        PlayerPrefs.SetInt("PlayerRotate", int.Parse(item.Rotate));
    }
}
