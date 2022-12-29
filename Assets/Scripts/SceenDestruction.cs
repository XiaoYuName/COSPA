using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteAdapter))]
[RequireComponent(typeof(Explodable))]
public class SceenDestruction : MonoSingleton<SceenDestruction>
{
    public Camera _Camera;
    private BoxCollider2D _boxCollider2D;
    private Explodable _explodable;
    private SpriteRenderer _renderer;
    private SpriteAdapter _adapter;

    protected override void Awake()
    {
        base.Awake();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _explodable = GetComponent<Explodable>();
        _boxCollider2D.size = new Vector2(_Camera.orthographicSize * 2 * _Camera.aspect, _Camera.orthographicSize * 2);
        _renderer = GetComponent<SpriteRenderer>();
        _adapter = GetComponent<SpriteAdapter>();
    }


    public void Destruction()
    {
        StartCoroutine(DestructionScene());
    }

    public void SetFollowCamare(Camera camera)
    {
        _Camera = camera;
    }

    public void Update()
    {
        if (gameObject.activeSelf && _Camera != null)
        {
            transform.localPosition = new Vector3(_Camera.transform.position.x,_Camera.transform.position.y,0);
        }
    }

    /// <summary>
    /// 破碎屏幕画面
    /// </summary>
    /// <returns></returns>
    IEnumerator DestructionScene()
    {
        yield return new WaitForEndOfFrame();  //等待当前渲染完毕
        Camera _MainCamera = _Camera;
        //初始化RendererTexture
        RenderTexture SceneTexture = new RenderTexture(Screen.width, Screen.height, 0);  //创建一个RenderTexture 高宽为屏幕高宽
        _MainCamera.targetTexture = SceneTexture; //渲染到到RenderTexture;
        _MainCamera.Render(); // 开始渲染一帧画面
        //激活渲染贴图并读取信息
        RenderTexture.active = SceneTexture;
        //创建纹理
        Texture2D mTexture2D = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,false);

        //读取纹理信息并存储为纹理数据
        Rect rect = new Rect(new Vector2(0, 0), new Vector2(Screen.width, Screen.height));
        mTexture2D.ReadPixels(rect, 0, 0);
        mTexture2D.Apply();

        //释放资源
        _MainCamera.depth = -10;
        _MainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(SceneTexture);
        
        //渲染到SpriteRenderer
        var Sp =  Sprite.Create(mTexture2D,rect,new Vector2(0.5f,0.5f),100);
        _renderer.sprite = Sp;
        _adapter.AdaptSpriteRender();
        _explodable.explode(false,true);
          _renderer.sprite = null;
         for (int i = 0; i < _explodable.fragments.Count; i++) 
         {
             Rigidbody2D rb2D = _explodable.fragments[i].GetComponent<Rigidbody2D>();
             rb2D.GetComponent<Collider2D>().isTrigger = true;
             rb2D.gravityScale = 0;
             rb2D.freezeRotation = true;
             // rb2D.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-10f, 10f));
             // rb2D.transform.localPosition += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
             StartCoroutine(Down(rb2D)) ;
             Destroy(rb2D.gameObject,5f);
         }
         

    }
    
    private IEnumerator Down(Rigidbody2D rb2D) 
    {
        yield return new WaitForSeconds(1);
        rb2D.gravityScale = 1;
        rb2D.AddForce(new Vector2(Random.Range(-3f, 3f), Random.Range(5f, 10f)), ForceMode2D.Impulse);
    }

}
