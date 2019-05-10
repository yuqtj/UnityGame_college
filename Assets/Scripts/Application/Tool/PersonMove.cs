using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PersonMove : MonoBehaviour {

    //移动到某一点后停留时间
    [SerializeField]
    private float stayHereTime;

    private AIAttribute aiAttribute;

    [SerializeField]
    private float moveSpeed = 0.3f;

    private NpcAnimation npcAnim;

    void Awake()
    {
        aiAttribute = GetComponent<AIAttribute>();
        npcAnim = GetComponent<NpcAnimation>();
    }

    

    #region 移动

    /// <summary>
    /// Npc移动
    /// </summary>
    public void Move(Vector2 endXorY)
    {

        if (endXorY.x != 0 && endXorY.y != 0)
        {
            Debug.Log("不符合规范");
            return;
        }
        else if (endXorY == Vector2.zero)
        {//玩家已经到目标点了或者异常，终止移动
            return;
        }
        else if (endXorY.x != 0)
        {
            NpcMoveAtX(endXorY.x);
        }
        else if (endXorY.y != 0)
        {
            NpcMoveAtY(endXorY.y);
        }
    }

    /// <summary>
    /// 存储巡逻点，x1和x2代表矩形上的对角线
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    //private void SavePatrolPoint(Vector2 x1, Vector2 x2)
    //{
    //    if (x1.x == x2.x && x1.y == x2.y)
    //    {//如果坐标一样，返回
    //        return;
    //    }
    //    else if (x1.x != x2.x && x1.y != x2.y)
    //    {//两个x/y都不一样，说明是矩形循环移动
    //        patrolPoint.Add(new Vector2(0, x2.y));
    //        patrolPoint.Add(new Vector2(x2.x, 0));
    //        patrolPoint.Add(new Vector2(0, x1.y));
    //        patrolPoint.Add(new Vector2(x1.x, 0));
    //    }
    //    else if (x1.x != x2.x || x1.y != x2.y)
    //    {//只有x/y一样，说明是垂直水平方向上的来回移动
    //        if (x1.x != x2.x)
    //        {
    //            patrolPoint.Add(new Vector2(x1.x, 0));
    //            patrolPoint.Add(new Vector2(x2.x, 0));
    //        }
    //        else if (x1.y != x2.y)
    //        {
    //            patrolPoint.Add(new Vector2(0, x1.y));
    //            patrolPoint.Add(new Vector2(0, x2.y));
    //        }
    //    }
    //}

    /// <summary>
    /// 获取下一个巡逻点
    /// </summary>
    /// <returns></returns>
    //private Vector2 GetNextPoint()
    //{
    //    if (patrolPoint.Count == 0)
    //    {
    //        Debug.Log("没有移动节点，请添加！");
    //        return Vector2.zero;
    //    }

    //    switch (moveMode)
    //    {//获取不同的移动方式采用不同的方法
    //        case MoveMode.Easy:
    //            moveAgain = null;
    //            break;

    //        case MoveMode.Nav:
    //            moveAgain = Move;

    //            if (indexInPatrolPoint == patrolPoint.Count)
    //            {//越界后终止

    //                return Vector2.zero;
    //            }
    //            break;

    //        case MoveMode.Patrol:
    //            moveAgain = Move;

    //            if (patrolPoint.Count == 1)
    //            {//巡逻模式下不能只设置一个节点，否则会栈溢出。
    //                moveAgain = null;
    //            }

    //            if (indexInPatrolPoint == patrolPoint.Count)
    //            {//越界后再重来
    //                indexInPatrolPoint = 0;
    //            }
    //            break;

    //    }
    //    return patrolPoint[indexInPatrolPoint++];
    //}

    /// <summary>
    /// 水平方向上的移动
    /// </summary>
    /// <param name="endX">X的终点坐标</param>
    private void NpcMoveAtX(float endX)
    {
        //目标点
        Vector3 desPos = new Vector3(endX, 0, 0);

        if (transform.localPosition.x == endX)
        {
            return;
        }
        else if (transform.localPosition.x < endX)
        {//向右移动
            StartCoroutine(NpcMove(PersonDirection.Right, desPos));
        }
        else if (transform.localPosition.x > endX)
        {//向左移动
            StartCoroutine(NpcMove(PersonDirection.Left, desPos));
        }
    }

    /// <summary>
    /// 垂直方向上的移动
    /// </summary>
    /// <param name="endY"></param>
    private void NpcMoveAtY(float endY)
    {
        //目标点
        Vector3 desPos = new Vector3(0, endY, 0);

        if (transform.localPosition.y == endY)
        {
            return;
        }
        else if (transform.localPosition.y < endY)
        {//向上移动
            StartCoroutine(NpcMove(PersonDirection.Up, desPos));
        }
        else if (transform.localPosition.y > endY)
        {//向下移动
            StartCoroutine(NpcMove(PersonDirection.Down, desPos));
        }
    }

    private Vector3 MoveDir(PersonDirection dir)
    {
        Vector3 dicVector3 = Vector3.zero;

        switch (dir)
        {
            case PersonDirection.Left:
                dicVector3 = new Vector3(-1, 0, 0);
                break;
            case PersonDirection.Right:
                dicVector3 = new Vector3(1, 0, 0);
                break;
            case PersonDirection.Up:
                dicVector3 = new Vector3(0, 1, 0);
                break;
            case PersonDirection.Down:
                dicVector3 = new Vector3(0, -1, 0);
                break;
        }

        return dicVector3;
    }

    /// <summary>
    /// 移动的函数
    /// </summary>
    /// <param name="dic">方向</param>
    /// <param name="desPos">目标点</param>
    /// <returns></returns>
    IEnumerator NpcMove(PersonDirection dir, Vector3 desPos)
    {
        Vector3 dicVector3 = MoveDir(dir);
        //设置动画
        npcAnim.SetRunDir(dir);

        npcAnim.StartRunAnim();

        bool arrive = false;

        //只要还没有到达
        while (!arrive)
        {
            //在FixedUpdate调用之后才执行接下来的代码
            yield return new WaitForFixedUpdate();

            dicVector3 = dicVector3.normalized * moveSpeed * Time.deltaTime;
            transform.Translate(dicVector3);

            if (desPos.x == 0)
            {//如果是y方向上的移动
                 arrive = Mathf.Abs(transform.localPosition.y - desPos.y) < 0.05f;
            }
            else if (desPos.y == 0)
            {//如果是x方向上的移动
                arrive = Mathf.Abs(transform.localPosition.x - desPos.x) < 0.05f;
            }
        }
        //停止动画
        npcAnim.StopRunAnim();

        //移动后的停留时间
        if (stayHereTime != 0f)
        {
            yield return new WaitForSeconds(stayHereTime);
        }
        //通知移动事件结束
        CommandManager.Instance.CommandOver();

        yield return null;
    }



    #endregion
}
