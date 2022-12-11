using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AtlasTool : Editor {

    [MenuItem("Tools/Slice Atlas")]
    public static void SliceAtlas()
    {
        if(!Directory.Exists("atlasout"))
        {
            Directory.CreateDirectory ("atlasout");
        }
        var dir = new DirectoryInfo ("Assets/AtlasTool");
        var imgs = dir.GetFiles ("*.png");
        var index = 0;
        for (; index < imgs.Length; index++)
        {
            var t = imgs[index];
            DealPng(t);
        }
    }

    public static void DealPng(FileInfo file)
    {
        var outdir = "atlasout/" + file.Name.Replace (".png", "");
        if(!Directory.Exists(outdir))
        {
            Directory.CreateDirectory (outdir);
        }
        string path = "Assets/AtlasTool/" + file.Name;
        var assets2 = AssetDatabase.LoadAllAssetsAtPath (path);
        foreach (var t in assets2)
        {
            Debug.LogError (t);
            if(t is Sprite)
            {
                var sp = t as Sprite;
                Texture2D t2d = new Texture2D ((int)sp.rect.width, (int)sp.rect.height, TextureFormat.RGBA32, false);
                var aslasTexture = sp.texture;
                t2d.SetPixels (aslasTexture.GetPixels((int)sp.rect.x, (int)sp.rect.y, (int)sp.rect.width, (int)sp.rect.height));
                t2d.Apply ();
                File.WriteAllBytes (outdir +"/" +sp.name +".png", t2d.EncodeToPNG());
            }
        }
    }
}

