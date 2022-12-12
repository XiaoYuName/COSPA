using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;
 
public class Tools : EditorWindow
{
    [MenuItem("Tools/openWindow")]
    public static void createWindow()
    {
        Tools window = EditorWindow.GetWindow<Tools>("设置位置");
        window.Show();
        window.minSize = new Vector2(200, 300);
    }
 
    private void OnGUI()
    {
       
        if (GUILayout.Button("分割图集"))
        {
            ProcessToSprite();
        }
        
    }
    static SpriteMetaData[] spriteArr;
 
    static void getSprite()
    {
        Texture2D image = Selection.activeObject as Texture2D;//获取选择的对象
 
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//获取路径名称
 
        string path = rootPath + "/" + image.name + ".png";//图片路径名称
 
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口
 
        spriteArr = texImp.spritesheet;
        for (int i = 0; i < spriteArr.Length; i++)
        {
            MonoBehaviour.print(spriteArr[i].rect);
        }
    }
 
    static void setSprite()
    {
        Texture2D image = Selection.activeObject as Texture2D;//获取选择的对象
 
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//获取路径名称
 
        string path = rootPath + "/" + image.name + ".png";//图片路径名称
 
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口
        texImp.spritesheet = spriteArr;
    }
	
	[MenuItem("Tools/ProcessToSprite #&C")]
    static void ProcessToSprite()
    {
        Texture2D image = Selection.activeObject as Texture2D;//获取选择的对象
 
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//获取路径名称
 
        string path = rootPath + "/" + image.name + ".png";//图片路径名称
 
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口
 
        AssetDatabase.CreateFolder(rootPath, image.name);//创建文件夹
        MonoBehaviour.print(path);
                                                         // texImp.spritesheet = 
        
        foreach (SpriteMetaData metaData in texImp.spritesheet)//遍历小图集
        {
            Texture2D myimage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);
 
            for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)//Y轴像素
            {
                for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                    myimage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, image.GetPixel(x, y));
            }
 
            if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
            {
                Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
                newTexture.SetPixels(myimage.GetPixels(0), 0);
                myimage = newTexture;
            }
            var pngData = myimage.EncodeToPNG();
 
            File.WriteAllBytes(rootPath + "/" + image.name + "/" + metaData.name + ".png", pngData);
            // 刷新资源窗口界面
            AssetDatabase.Refresh();
        }
    }
    
}

