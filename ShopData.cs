﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 商城功能模块数据操作
/// </summary>
public class ShopData {

    //用于存储XML数据的实体集合
    public List<ShopItem> shopList = new List<ShopItem>();

    //商品的购买状态集合
    public List<int> shopState = new List<int>();

    public int goldCount = 0;  //金币数
    public int highScore = 0;  //最高分

    /// <summary>
    /// 通过指定的路径读取XML文档
    /// </summary>
    /// <param name="path">xml的路径</param>
    public void ReadXMLByPath(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("Shop");
        XmlNodeList nodeList = root.ChildNodes;
        foreach(XmlNode node in nodeList)
        {
            string speed = node.ChildNodes[0].InnerText;
            string rotate = node.ChildNodes[1].InnerText;
            string model = node.ChildNodes[2].InnerText;
            string price = node.ChildNodes[3].InnerText;
            string id = node.ChildNodes[4].InnerText;

            //Debug.Log(speed + "--" + rotate + "--" + model + "--" + price);

            //遍历完毕后，存储到List集合中
            ShopItem item = new ShopItem(id, speed, rotate, model, price);
            shopList.Add(item);
        }
    }

    /// <summary>
    /// 读取金币和最高分数
    /// </summary>
    public void ReadScoreAndGold(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        goldCount = int.Parse(nodeList[0].InnerText);
        highScore = int.Parse(nodeList[1].InnerText);

        //读取商品的购买状态
        for (int i = 2; i < 6; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// 更新XML文档内容
    /// </summary>
    public void UpdateXMLData(string path, string key, string value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key) 
            {
                node.InnerText = value;
                doc.Save(path);
            }
        }
    }

    /*
    /// <summary>
    /// 测试函数，测试List中的数据
    /// </summary>
    private void DebugListInfo()
    {
        for (int i = 0; i < shopList.Count; i++)
        {
            Debug.Log(shopList[i].ToString());
        }
    }
    */
}
