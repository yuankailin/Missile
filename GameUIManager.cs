using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    private GameObject m_GamePanel;
    private GameObject m_OverPanel;

    private UILabel label_Score;  //分数UI
    private UILabel label_Time;   //时间UI

    private GameObject m_ButtonControl;

    private GameObject button_Reset;

    private int time;
    public int Time
    {
        get { return time; }
        set { time = value;
            UpdateTimeLacel(time);
        }
    }

    //OverPanelInfo
    private UILabel scoreNum;   //最高分数
    private UILabel starNum;    //奖励数量
    private UILabel timeNum;    //时间长度

	void Start () {

        m_GamePanel = GameObject.Find("GamePanel");
        m_OverPanel = GameObject.Find("OverPanel");

        label_Score = GameObject.Find("StarNum1").GetComponent<UILabel>();
        label_Score.text = "0";
        label_Time = GameObject.Find("Time1").GetComponent<UILabel>();
        label_Time.text = "0:0";
        StartCoroutine("AddTime");

        scoreNum = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();
        starNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        timeNum = GameObject.Find("Time/TimeNum").GetComponent<UILabel>();

        m_ButtonControl = GameObject.Find("ButtonControl");

        button_Reset = GameObject.Find("Reset");
        UIEventListener.Get(button_Reset).onClick = ResetButtonClick;

        m_OverPanel.SetActive(false);
	}

    /// <summary>
    /// 更新分数UI显示
    /// </summary>
    public void UpdateScoreLabel(int score)
    {
        label_Score.text = score.ToString();
    }


    private void UpdateTimeLacel(int t)
    {
        if (t < 60) 
        {
            label_Time.text = "0:" + t;
        }
        else
        {
            label_Time.text = t / 60 + ":" + t % 60;
        }
    }

    /// <summary>
    /// 显示结束面板
    /// </summary>
    public void ShowOverPanel()
    {
        m_ButtonControl.SetActive(false);
        m_OverPanel.SetActive(true);
        StopAddTime();
        SetOverPanelInfo();
    }

    private void ResetButtonClick(GameObject go)
    {
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    /// 协程累加时间计时器
    /// </summary>
    /// <returns></returns>
    IEnumerator AddTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Time++;
        }
    }

    /// <summary>
    /// 停止时间累加协程
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }

    /// <summary>
    /// 给结束面板UI赋值
    /// </summary>
    private void SetOverPanelInfo()
    {
        int t = int.Parse(label_Score.text);
        starNum.text = "+" + (t * 10);
        timeNum.text = "+" + time.ToString();
        int tempHighestScore = t * 10 + time;
        scoreNum.text = tempHighestScore.ToString();

        //存储最高分
        PlayerPrefs.SetInt("HighestScore", tempHighestScore);
        //存储金币数
        PlayerPrefs.SetInt("Gold", t * 10);
    }
}
