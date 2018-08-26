using UnityEngine;
using System.Collections;
using System.Xml;  //引入XML操作相关的命名空间

/// <summary>
/// XML操作演示
/// </summary>
public class XMLDemo : MonoBehaviour {

    //定义字段存储xml的路径
    private string xmlPath = "Assets/Datas/web.xml";
	
	void Start () {

        ReadXMLByPath(xmlPath);
	}

    /// <summary>
    /// 通过路径读取XML中的数据进行显示
    /// </summary>
    /// <param name="path">XML的路径地址</param>
    private void ReadXMLByPath(string path)
    {
        //<1>实例化一个 XML 文档操作对象
        XmlDocument doc = new XmlDocument();
        //<2>使用 XML 对象加载 XML
        doc.Load(path);
        //<3>获取根节点
        XmlNode root = doc.SelectSingleNode("Web");
        //<4>获取根节点下所有子节点
        XmlNodeList nodeList = root.ChildNodes;
        //<5>遍历输出
        foreach(XmlNode node in nodeList)
        {
            //取属性
            int id = int.Parse(node.Attributes["id"].Value);
            //取文本
            string name = node.ChildNodes[0].InnerText;
            string url = node.ChildNodes[1].InnerText;

            Debug.Log(id + "--" + name + "--" + url);
        }
    }
}
