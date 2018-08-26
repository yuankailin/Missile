using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

    private Ship m_Ship;

    private GameObject left;
    private GameObject right;

	void Start () {

        left = GameObject.Find("Left");
        right = GameObject.Find("Right");

        m_Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        UIEventListener.Get(left).onPress = LeftPress;
        UIEventListener.Get(right).onPress = RightPress;
	}

    private void LeftPress(GameObject go, bool isPress)
    {
        if (isPress) 
        {
            m_Ship.IsLeft = true;
        }
        else
        {
            m_Ship.IsLeft = false;
        }
    }

    private void RightPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            m_Ship.IsRight = true;
        }
        else
        {
            m_Ship.IsRight = false;
        }
    }
}
