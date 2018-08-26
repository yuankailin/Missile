using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour {

    private GameObject m_StartPanel;
    private GameObject m_SettingPanel;

    private GameObject button_Setting;
    private GameObject button_Close;

    private GameObject button_Play;

	void Start () {

        m_StartPanel = GameObject.Find("StartPanel");
        m_SettingPanel = GameObject.Find("SettingPanel");

        button_Setting = GameObject.Find("Setting");
        UIEventListener.Get(button_Setting).onClick = SettingButtonClick;
        button_Close = GameObject.Find("Close");
        UIEventListener.Get(button_Close).onClick = CloseButtonClick;

        button_Play = GameObject.Find("Play");
        UIEventListener.Get(button_Play).onClick = PlayButtonClick;

        m_SettingPanel.SetActive(false);  //默认隐藏设置面板
	}

    /// <summary>
    /// 设置按钮被点击
    /// </summary>
    private void SettingButtonClick(GameObject go)
    {
        //首先判断设置面板是否已经显示，如果已经显示则不执行逻辑
        if (m_SettingPanel.activeSelf == false)
        {
            m_SettingPanel.SetActive(true);
        }
    }

    private void CloseButtonClick(GameObject go)
    {
        m_SettingPanel.SetActive(false);
    }


    private void PlayButtonClick(GameObject go)
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 设置开始按钮的状态
    /// </summary>
    /// <param name="state"></param>
    public void SetPlayButtonState(int state)
    {
        if (state == 1)
        {
            button_Play.SetActive(true);
        }
        else
        {
            button_Play.SetActive(false);
        }
    }
}
