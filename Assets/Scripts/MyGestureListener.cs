using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MyGestureListener : MonoBehaviour,KinectGestures.GestureListenerInterface {

    public int playerIndex = 0;

    public Transform cubeTrans;

    public Text text;

    private static MyGestureListener instance = null;

    public static MyGestureListener Instance
    {
        get
        {
            return instance;
        }
    }

    #region GestureListenerInterface 实现

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        return false;
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {

        if (Test.Instance.isMid)
        {
            if (gesture == KinectGestures.Gestures.RaiseLeftHand)
            {
                text.text = "举起了左手！";
                isRaiseLeftHand = true;
            }
            else if (gesture == KinectGestures.Gestures.RaiseRightHand)
            {
                text.text = "举起了右手！";
                isRaiseRightHand = true;
            }
            else if (gesture == KinectGestures.Gestures.Psi)
            {
                text.text = "投降！";
                isPsi = true;
            }
            else if (gesture == KinectGestures.Gestures.Stop)
            {
                text.text = "停！";
                isStop = true;
            }
            else if (gesture == KinectGestures.Gestures.Tpose)
            {
                text.text = "T!";
                isTpose = true;
            }
            else if (gesture == KinectGestures.Gestures.Wave)
            {
                text.text = "Wave!";
                isWave = true;
            }
            else if (gesture == KinectGestures.Gestures.SwipeLeft)
            {
                isSwipeLeft = true;
                cubeTrans.DORotate(cubeTrans.position + new Vector3(0, 90, 0), 1).SetEase(Ease.Linear);

            }
            else if (gesture == KinectGestures.Gestures.SwipeRight)
            {
                isSwipeRight = true;
                cubeTrans.DORotate(cubeTrans.position + new Vector3(0, -90, 0), 1).SetEase(Ease.Linear);
            }
            else if (gesture == KinectGestures.Gestures.SwipeUp)
            {
                isSwipeUP = true;
                cubeTrans.DORotate(cubeTrans.position + new Vector3(90, 0, 0), 1).SetEase(Ease.Linear);
            }
            else if (gesture == KinectGestures.Gestures.SwipeDown)
            {
                isSwipeDown = true;
                cubeTrans.DORotate(cubeTrans.position - new Vector3(90, 0, 0), 1).SetEase(Ease.Linear);
            }
            else if (gesture == KinectGestures.Gestures.Jump)
            {
                text.text = "跳！";
            }
            else if (gesture == KinectGestures.Gestures.Squat)
            {
                text.text = "蹲！";
            }
            else if (gesture == KinectGestures.Gestures.Pull)
            {
                text.text = "拉！";
            }
            else if (gesture == KinectGestures.Gestures.Push)
            {
                text.text = "推！";
            }
            else if (gesture == KinectGestures.Gestures.RaisedLeftHorizontalRightHand)
            {
                text.text = "举左手右手水平！";
            }
            else if (gesture == KinectGestures.Gestures.RaisedRightHorizontalLeftHand)
            {
                text.text = "举右手左手水平！";
            }
        }
        else
        {
            text.text = "丢失玩家！";
        }

        return true;
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (Test.Instance.isMid)
        {
            if (gesture == KinectGestures.Gestures.ZoomIn)
            {
                if (progress > 0.5f && cubeTrans.localScale.x <= 5)
                {
                    cubeTrans.localScale += Vector3.one * 0.1f * screenPos.z;
                    print(screenPos);
                }
            }
            else if (gesture == KinectGestures.Gestures.ZoomOut)
            {
                if (progress > 0.5f && cubeTrans.localScale.x >= 0.5f)
                {
                    cubeTrans.localScale -= Vector3.one * 0.1f * screenPos.z;
                    print(screenPos);
                }
            }
            else if (gesture == KinectGestures.Gestures.Wheel)
            {
                if (progress > 0.5f)
                {
                    if (Mathf.Abs(screenPos.z) > 1)
                    {
                        text.text = "Wheel";
                        cubeTrans.Rotate(screenPos * 0.1f);
                    }
                }
            }
            else if (gesture == KinectGestures.Gestures.ShoulderLeftFront)
            {
                text.text = "左肩向前！";
            }
            else if (gesture == KinectGestures.Gestures.ShoulderRightFront)
            {
                text.text = "右肩向前！";
            }
            else if (gesture == KinectGestures.Gestures.LeanLeft)
            {
                text.text = "左倾！";
            }
            else if (gesture == KinectGestures.Gestures.LeanRight)
            {
                text.text = "右倾！";
            }
            else if (gesture == KinectGestures.Gestures.LeanForward)
            {
                text.text = "前倾！";
            }
            else if (gesture == KinectGestures.Gestures.LeanBack)
            {
                text.text = "后倾！";
            }
            else if (gesture == KinectGestures.Gestures.KickLeft)
            {
                text.text = "左脚前踢！";
            }
            else if (gesture == KinectGestures.Gestures.KickRight)
            {
                text.text = "右脚前踢！";
            }
        }
        else
        {
            text.text = "丢失玩家!";
        }
    }

    KinectManager manager;
    /// <summary>
    /// 当识别到用户时调用此方法
    /// </summary>
    public void UserDetected(long userId, int userIndex)
    {
            manager = KinectManager.Instance;

            manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);//举右手
            manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);//举左手
            manager.DetectGesture(userId, KinectGestures.Gestures.Psi);//Psi动作
            manager.DetectGesture(userId, KinectGestures.Gestures.Stop);//停止
            manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);//T字型
            manager.DetectGesture(userId, KinectGestures.Gestures.Wave);//挥动  不太灵敏
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);//左划
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);//右划
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);//上划
            manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);//下划
            manager.DetectGesture(userId, KinectGestures.Gestures.ZoomIn);//放大  效果一般
            manager.DetectGesture(userId, KinectGestures.Gestures.ZoomOut);//缩小  效果一般
            manager.DetectGesture(userId, KinectGestures.Gestures.Wheel);//旋转  不太灵敏
            manager.DetectGesture(userId, KinectGestures.Gestures.Jump);//跳
            manager.DetectGesture(userId, KinectGestures.Gestures.Squat);//蹲
            manager.DetectGesture(userId, KinectGestures.Gestures.Push);//推
            manager.DetectGesture(userId, KinectGestures.Gestures.Pull);//拉
            manager.DetectGesture(userId, KinectGestures.Gestures.ShoulderLeftFront);//左肩向前 不太灵敏
            manager.DetectGesture(userId, KinectGestures.Gestures.ShoulderRightFront);//右肩向前 不太灵敏
            manager.DetectGesture(userId, KinectGestures.Gestures.LeanLeft);//左倾  时好时坏
            manager.DetectGesture(userId, KinectGestures.Gestures.LeanRight);//右倾  时好时坏
            manager.DetectGesture(userId, KinectGestures.Gestures.LeanForward);//前倾  时好时坏
            manager.DetectGesture(userId, KinectGestures.Gestures.LeanBack);//后倾  时好时坏
            manager.DetectGesture(userId, KinectGestures.Gestures.KickLeft);//左脚前踢
            manager.DetectGesture(userId, KinectGestures.Gestures.KickRight);//右脚前踢
            manager.DetectGesture(userId, KinectGestures.Gestures.RaisedLeftHorizontalRightHand);//举左手右手水平 没反应
            manager.DetectGesture(userId, KinectGestures.Gestures.RaisedRightHorizontalLeftHand);//举右手左手水平 没反应
    }

    public void UserLost(long userId, int userIndex)
    {
    }

    #endregion

    #region 各种bool

    bool isRaiseRightHand = false;
    public bool IsRaiseRightHand()
    {
        if (isRaiseRightHand)
        {
            isRaiseRightHand = false;
            return true;
        }
        return false;
    }

    bool isRaiseLeftHand = false;
    public bool IsRaiseLeftHand()
    {
        if (isRaiseLeftHand)
        {
            isRaiseLeftHand = false;
            return true;
        }
        return false;
    }

    bool isPsi = false;
    public bool IsPsi()
    {
        if (isPsi)
        {
            isPsi = false;
            return true;
        }
        return false;
    }

    bool isStop = false;
    public bool IsStop()
    {
        if (isStop)
        {
            isStop = false;
            return true;
        }
        return false;
    }

    bool isTpose = false;
    public bool IsTpose()
    {
        if (isTpose)
        {
            isTpose = false;
            return true;
        }
        return false;
    }

    bool isWave = false;
    public bool IsWave()
    {
        if (isWave)
        {
            isWave = false;
            return true;
        }
        return false;
    }

    bool isSwipeLeft = false;
    public bool IsSwipeLeft()
    {
        if (isSwipeLeft)
        {
            isSwipeLeft = false;
            return true;
        }
        return false;
    }

    bool isSwipeRight = false;
    public bool IsSwipeRight()
    {
        if (isSwipeRight)
        {
            isSwipeRight = false;
            return true;
        }
        return false;
    }

    bool isSwipeUP = false;
    public bool IsSwipeUP()
    {
        if (isSwipeUP)
        {
            isSwipeUP = false;
            return true;
        }
        return false;
    }

    bool isSwipeDown = false;
    public bool IsSwipeDown()
    {
        if (isSwipeDown)
        {
            isSwipeDown = false;
            return true;
        }
        return false;
    }

    #endregion


    void Awake()
    {
        instance = this;
    }
}
