using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NpcAnimation : MonoBehaviour {

    #region NPC图集

    public Sprite IdleDown;
    public Sprite IdleUp;
    public Sprite IdleLeft;
    public Sprite IdleRight;

    public Sprite WalkDown1;
    public Sprite WalkDown2;

    public Sprite WalkUp1;
    public Sprite WalkUp2;

    public Sprite WalkLeft1;
    public Sprite WalkLeft2;

    public Sprite WalkRight1;
    public Sprite WalkRight2;

    private Sprite idleSprite;
    private Sprite moveSprite1;
    private Sprite moveSprite2;

    private SpriteRenderer image;

    #endregion

    //0.2f
    [SerializeField]
    private float playAnimSpeed = 0.15f;

    AIAttribute aiAttribute;

    PersonDirection dir;
    //是否初始化
    bool init = false;

    void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        aiAttribute = GetComponent<AIAttribute>();
    }

    /// <summary>
    /// 自动获取精灵图集，这个函数在物体一创建就调用
    /// </summary>
    public void GetAnimTexture()
    {
        init = true;

        Sprite[] sprites = Resources.LoadAll<Sprite>(Consts.TextureDir + name);

        for (int i = 0; i < sprites.Length; i++)
        {
            switch (sprites[i].name)
            {
                case "IdleDown":
                    IdleDown = sprites[i];
                    break;
                case "IdleUp":
                    IdleUp = sprites[i];
                    break;
                case "IdleLeft":
                    IdleLeft = sprites[i];
                    break;
                case "IdleRight":
                    IdleRight = sprites[i];
                    break;
                case "RunDown1":
                    WalkDown1 = sprites[i];
                    break;
                case "RunDown2":
                    WalkDown2 = sprites[i];
                    break;
                case "RunUp1":
                    WalkUp1 = sprites[i];
                    break;
                case "RunUp2":
                    WalkUp2 = sprites[i];
                    break;
                case "RunLeft1":
                    WalkLeft1 = sprites[i];
                    break;
                case "RunLeft2":
                    WalkLeft2 = sprites[i];
                    break;
                case "RunRight1":
                    WalkRight1 = sprites[i];
                    break;
                case "RunRight2":
                    WalkRight2 = sprites[i];
                    break;
            }
        }
    }

    public void SetIdle(PersonDirection dir)
    {
        switch (dir)
        {
            case PersonDirection.Left:
                image.sprite = IdleLeft;
                dir = PersonDirection.Left;
                break;
            case PersonDirection.Right:
                image.sprite = IdleRight;
                dir = PersonDirection.Right;
                break;
            case PersonDirection.Up:
                image.sprite = IdleUp;
                dir = PersonDirection.Up;
                break;
            case PersonDirection.Down:
                image.sprite = IdleDown;
                dir = PersonDirection.Down;
                break;
        }

        if (name == "Player")
        {
            aiAttribute.SetPlayerDir(dir);
        }
    }

    public void SetRunDir(PersonDirection dir)
    {
        this.dir = dir;

        switch (dir)
        {
            case PersonDirection.Down:
                idleSprite = IdleDown;
                moveSprite1 = WalkDown1;
                moveSprite2 = WalkDown2;
                break;
            case PersonDirection.Up:
                idleSprite = IdleUp;
                moveSprite1 = WalkUp1;
                moveSprite2 = WalkUp2;
                break;
            case PersonDirection.Left:
                idleSprite = IdleLeft;
                moveSprite1 = WalkLeft1;
                moveSprite2 = WalkLeft2;
                break;
            case PersonDirection.Right:
                idleSprite = IdleRight;
                moveSprite1 = WalkRight1;
                moveSprite2 = WalkRight2;
                break;
        }
    }

    public void StartRunAnim()
    {
        //StopCoroutine只能停止StartCoroutine（字符串）的协程
        StartCoroutine("PlayAnim");
    }

    public void StopRunAnim()
    {
        StopCoroutine("PlayAnim");
        //停止动画后恢复Idle动画
        SetIdle(dir);
    }

    IEnumerator PlayAnim()
    {
        while (true)
        {
            image.sprite = moveSprite1;

            yield return new WaitForSeconds(playAnimSpeed);

            image.sprite = idleSprite;

            yield return new WaitForSeconds(playAnimSpeed);

            image.sprite = moveSprite2;

            yield return new WaitForSeconds(playAnimSpeed);

            image.sprite = idleSprite;

            yield return new WaitForSeconds(playAnimSpeed);
        }
    }
}
