using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAdapter : MonoBehaviour
{
    public enum EFillModel
    {
        /// <summary>
        /// 显示图片的所有内容
        /// </summary>
        ShowAll,
        /// <summary>
        /// 使图片内容填满屏幕
        /// </summary>
        Full,
        /// <summary>
        /// 根据图片高度填充屏幕
        /// </summary>
        WithHeight,
        /// <summary>
        /// 根据图片宽度填充屏幕
        /// </summary>
        WithWidth
    }

    public enum EUpdateType
    {
        /// <summary>
        /// 只在唤醒时更新一次
        /// </summary>
        UpdateOnAwake,
        /// <summary>
        /// 再每次视口发生变化的时候更新一次
        /// </summary>
        UpdateOnViewportChanged
    }

    public EFillModel eFillModel = EFillModel.Full;
    public EUpdateType TickType = EUpdateType.UpdateOnAwake;
    public Camera Viewport;
    float ScreenRatio;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScreenRatio = Viewport.aspect;
        AdaptSpriteRender();
    }

    private void LateUpdate()
    {
        if (TickType == EUpdateType.UpdateOnViewportChanged && spriteRenderer.sprite != null) 
        {
            if (ScreenRatio != Viewport.aspect)
            {
                AdaptSpriteRender();
            }
        }
    }

    /// <summary>
    /// 使sprite铺满整个屏幕
    /// </summary>
    public void AdaptSpriteRender()
    {
        if (spriteRenderer.sprite == null)
            return;
        Vector3 scale = spriteRenderer.transform.localScale;
        float cameraheight = Viewport.orthographicSize * 2;
        float camerawidth = cameraheight * Viewport.aspect;
        float texr = (float)spriteRenderer.sprite.texture.width / spriteRenderer.sprite.texture.height;
        float viewr = camerawidth / cameraheight;
        switch (eFillModel)
        {
            case EFillModel.WithHeight:
                //> 根据图片高度进行填充
                scale *= cameraheight / spriteRenderer.bounds.size.y;
                break;
            case EFillModel.WithWidth:
                //> 根据图片宽度进行填充
                scale *= camerawidth / spriteRenderer.bounds.size.x;
                break;
            case EFillModel.Full:
                //> 填满整个屏幕
                if (viewr >= texr)
                {
                    if (viewr >= 1 && texr >= 1 || texr < 1)
                        scale *= camerawidth / spriteRenderer.bounds.size.x;
                    else
                        scale *= cameraheight / spriteRenderer.bounds.size.y;
                }
                else
                {
                    if (viewr <= 1 || texr > 1)
                        scale *= cameraheight / spriteRenderer.bounds.size.y;
                    else
                        scale *= camerawidth / spriteRenderer.bounds.size.x;
                }
                break;
            default:
                //> 在屏幕上显示图片的全部内容
                if (viewr >= texr)
                {
                    scale *= cameraheight / spriteRenderer.bounds.size.y;
                }
                else
                {
                    scale *= camerawidth / spriteRenderer.bounds.size.x;
                }
                break;
        }
        spriteRenderer.transform.localScale = scale;
    }
}
