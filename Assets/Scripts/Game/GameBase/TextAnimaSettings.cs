using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using File = System.IO.File;

namespace ARPG
{
    public static class TextAnimaSettings
    {
        /// <summary>
        /// 获取附加到标记的文字动画文字
        /// </summary>
        /// <param name="animaType">动画类型</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetAnimaString(string content,TextAnimaType animaType)
        {
            string des = animaType switch
            {
                TextAnimaType.摆锤 => "<pend>" + content +"</pend>",
                TextAnimaType.悬垂 => "<dangle>" + content +"</dangle>",
                TextAnimaType.淡入淡出 => "<dangle>"+content+"</dangle>",
                TextAnimaType.彩虹 => "<rainb>"+content+"</rainb>",
                TextAnimaType.旋转 =>"<rot>"+content+"</rot>",
                TextAnimaType.弹跳 =>"<bounce>"+content+"</bounce>",
                TextAnimaType.幻灯片 => "<slide>"+content+"</slide>",
                TextAnimaType.秋千 => "<swing>"+content+"</swing>",
                TextAnimaType.波 => "<wave>"+content+"</wave>",
                TextAnimaType.增加大小 => "<incr>"+content+"</incr>",
                TextAnimaType.摇晃 => "<shake>"+content+"</shake>",
                TextAnimaType.摆动 => "<wiggle>"+content+"</wiggle>",
                _=> content,
            };
            return des;
        }

        /// <summary>
        /// 获取精灵图集String
        /// </summary>
        /// <param name="spriteName">文件名称</param>
        /// <param name="indexName">内容</param>
        /// <returns>返回规范好的内容字符串组</returns>
        /// <exception cref="Exception">没有没有该文件则报错</exception>
        public static string GetSpineText(string spriteName, string indexName)
        {
            string path = Application.dataPath+"/Plugins/TextMesh Pro/Resources/Sprite Assets/"+spriteName+".asset";
            if (File.Exists(path))
            {
                char[] arr = indexName.ToCharArray();
                string line ="";
                foreach (var c in arr)
                {
                    string item = "<sprite=\"" + spriteName + "\" name=\"" + c + "\">";
                    line += item;
                }
                return line;
            }

            throw new Exception("没有对应的Sprite文件");
        }

        public static string GetDamageText(DamageType type,bool isCirct,string Damage)
        {
            string sprite = "";
            sprite = isCirct ? "Cirtical" : type.ToString();
            return GetSpineText(sprite,Damage);
        }
    }

    public enum TextAnimaType
    {
        摆锤,
        悬垂,
        淡入淡出,
        彩虹,
        旋转,
        弹跳,
        幻灯片,
        秋千,
        波,
        增加大小,
        摇晃,
        摆动,
    }
}
