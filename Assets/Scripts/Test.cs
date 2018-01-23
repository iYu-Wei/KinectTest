using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 限定范围脚本
/// </summary>
public class Test : MonoBehaviour {

    private static Test instance = null;

    public static Test Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    public bool isMid = false;

    // Update is called once per frame
    void Update()
    {

        KinectManager manager = KinectManager.Instance;

        if (!manager)
        {
            return;
        }
        long userId = manager.GetUserIdByIndex(0);//获取玩家ID

        if (manager.IsJointTracked(userId, (int)KinectInterop.JointType.SpineMid))//追踪到玩家脊柱骨骼
        {
            Vector3 posJoint = manager.GetJointPosColorOverlay(userId, (int)KinectInterop.JointType.SpineMid, Camera.main, Camera.main.pixelRect);//获取位置

            if (posJoint != Vector3.zero)
            {
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(posJoint);//转换为视口坐标
                if (viewportPos.x > 0.4f && viewportPos.x < 0.6f)//限定范围（从左到右 0到1）
                {
                    isMid = true;
                }
                else
                {
                    isMid = false;
                }
            }
        }
    }
}
