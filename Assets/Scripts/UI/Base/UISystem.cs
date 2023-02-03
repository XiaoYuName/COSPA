using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;
using ARPG.Config;

namespace ARPG
{
    /// <summary>
    /// UISystem 底层委托: 用于打开界面后的回调
    /// </summary>
    /// <typeparam name="T">打开界面后的T类型对象</typeparam>
    public delegate void Call<in T>(T cb);
    
    /// <summary>
    /// UI管理器 :
    ///     UI工厂
    /// </summary>
    public class UISystem : MonoSingleton<UISystem>
    {
        /// <summary>
        /// Table配置表
        /// </summary>
        private UITable tableConfig;
        /// <summary>
        /// Prefab 配置表
        /// </summary>
        private UIPrefab prefabConfig;
        private Dictionary<string, GameObject> UiTableDic;
        /// <summary>
        /// UI根节点
        /// </summary>
        private List<RectTransform> UIRoot;
        /// <summary>
        /// 已宽度适配的UI画布根节点:该节点物体渲染高于UIRoot
        /// </summary>
        private List<RectTransform> AutoUIRootTop;
        /// <summary>
        /// 已宽度适配的UI画布根节点:该节点物体渲染低于UIRoot
        /// </summary>
        private List<RectTransform> AutoUIRootDonw;
        /// <summary>
        /// 已高度适配的UI画布根节点:该节点物体渲染处于最顶级
        /// </summary>
        private List<RectTransform> TopAutoUIRootTop;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        #region Init
        
        private void Init()
        {
            tableConfig = ConfigManager.LoadConfig<UITable>("UITable/UITable");
            prefabConfig = ConfigManager.LoadConfig<UIPrefab>("UIPrefab/UIPrefab");
            UiTableDic = new Dictionary<string, GameObject>();
            UIRoot = new List<RectTransform>();
            AutoUIRootTop = new List<RectTransform>();
            AutoUIRootDonw = new List<RectTransform>();
            TopAutoUIRootTop = new List<RectTransform>();
                InitParent();
            InitLoadPrefab();
        }
        
        /// <summary>
        /// 1. 加载初始父节点,如果没有则创建一个
        /// </summary>
        private void InitParent()
        {
            foreach (var ParentName in tableConfig.UIParents)
            {
                var Parent = transform.Find(ParentName) as RectTransform;
                if (Parent == null)
                {
                    GameObject obj = new GameObject(ParentName);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = Vector3.zero;
                    UIRoot.Add(obj.transform as RectTransform);
                }
                else
                {
                    UIRoot.Add(Parent);
                }

                var TopParent = transform.parent.Find("AutoUITop").Find(ParentName) as RectTransform;
                if (TopParent == null)
                {
                    GameObject obj = new GameObject(ParentName);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = Vector3.zero;
                    AutoUIRootTop.Add(obj.transform as RectTransform);
                }
                else
                {
                    AutoUIRootTop.Add(TopParent);
                }

                var DownParent = transform.parent.Find("AutoUIDown").Find(ParentName)as RectTransform;
                if (DownParent == null)
                {
                    GameObject obj = new GameObject(ParentName);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = Vector3.zero;
                    AutoUIRootDonw.Add(obj.transform as RectTransform);
                }
                else
                {
                    AutoUIRootDonw.Add(DownParent);
                }

                var TopRoot = transform.parent.Find("TopUICanvas").Find(ParentName) as RectTransform;
                if (TopRoot == null)
                {
                    GameObject obj = new GameObject(ParentName);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = Vector3.zero;
                    TopAutoUIRootTop.Add(obj.transform as RectTransform);
                }
                else
                {
                    TopAutoUIRootTop.Add(TopRoot);
                }

            }
        }
        
        /// <summary>
        /// 2.加载UI
        /// </summary>
        private void InitLoadPrefab()
        {
            List<UITableItem> Inits = tableConfig.GetLoadInitPrefab();
            foreach (var Table in Inits)
            {
                var Obj = Instantiate(Table.Prefab, String.IsNullOrEmpty(Table.parentName) ? 
                    transform : GetPanrent(Table.Type,Table.parentName));
                Obj.SetActive(true);
                if (Obj.GetComponent<UIBase>() == null)
                {
                    Obj.gameObject.SetActive(true);
                    UiTableDic.Add(Table.ID, Obj);
                    return;
                }

                if(Table.isInit)
                    Obj.GetComponent<UIBase>().Open();
                else 
                    Obj.GetComponent<UIBase>().Close();
                UiTableDic.Add(Table.ID, Obj);
            }
        }

        /// <summary>
        /// 获取UI父级: 内部调用
        /// </summary>
        /// <param name="type">适配层级</param>
        /// <param name="uiname">父级名称</param>
        /// <returns></returns>
        private RectTransform GetPanrent(UITableType type,string uiname)
        {
            return type switch
            {
                UITableType.UIRoot => UIRoot.Find(a => a.name == uiname),
                UITableType.UITop => AutoUIRootTop.Find(a => a.name == uiname),
                UITableType.UIDonw => AutoUIRootDonw.Find(a => a.name == uiname),
                UITableType.TopUIRoot => TopAutoUIRootTop.Find(a=>a.name == uiname),
                _=>  UIRoot.Find(a => a.name == uiname),
            };
        }

        #endregion

        #region 底层架构
        /// <summary>
        /// 获取UI的GameObject
        /// </summary>
        /// <param name="uiname">UI名称</param>
        /// <returns>GameObject对象</returns>
        public GameObject GetUI(string uiname)
        {
            if (!UiTableDic.ContainsKey(uiname)) return LaodInstance(uiname);
            GameObject Obj = UiTableDic[uiname];
            return Obj;
        }
        
        /// <summary>
        /// 获取UI的GameObject 并且设置他的isActive
        /// </summary>
        /// <param name="uiname">UI的名称</param>
        /// <param name="isActive">是否显示隐藏</param>
        /// <returns>UI的GameObject</returns>
        public GameObject GetUI(string uiname,bool isActive)
        {
            var Obj = GetUI(uiname);
            UIBase @base = Obj.GetComponent<UIBase>();
            if (@base == null) throw new Exception("获取UIBase失败,请确保该组件继承自UIBase");
            if(isActive)
                @base.Open();
            else
                @base.Close();
            return Obj;
        }

        /// <summary>
        /// 获取指定类型GameObje 的Conmponent对象
        /// </summary>
        /// <param name="uiname">对象名称</param>
        /// <param name="isActive">对象Active</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUI<T>(string uiname, bool isActive) where T : MonoBehaviour
        {
            GameObject Obj = GetUI(uiname, isActive);
            T t = Obj.GetComponent<T>();
            return t;
        }
        /// <summary>
        /// 获取指定类型GameObje 的Conmponent对象
        /// </summary>
        /// <param name="uiname">对象名称</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUI<T>(string uiname) where T : MonoBehaviour
        {
            GameObject Obj = GetUI(uiname);
            T t = Obj.GetComponent<T>();
            return t;
        }

        /// <summary>
        /// 获取指定类型GameObje 的对象，该对象不需要继承自UIBase 
        /// </summary>
        /// <param name="uiname"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetNotBaseUI<T>(string uiname) where T : Component
        {
            GameObject Obj = GetUI(uiname);
            return Obj.GetComponent<T>();
        }
        
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="uiname"></param>
        public void OpenUI(string uiname)
        {
            UIBase Obj = GetUI(uiname).GetComponent<UIBase>();
            Obj.transform.SetAsLastSibling();
            Obj.Open();
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="uiname">ui名称</param>
        /// <param name="func">打开后的回调处理</param>
        /// <typeparam name="T">任意类型</typeparam>
        public void OpenUI<T>(string uiname, Call<T> func) where T : UIBase
        {
            var Obj = GetUI(uiname);
            Obj.transform.SetAsLastSibling();
            var conmop = Obj.GetComponent<T>();
            UIBase @base = Obj.GetComponent<UIBase>();
            @base.Open();
            func?.Invoke(conmop);
        }


        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="uiname">ui名称</param>
        public void CloseUI(string uiname)
        {
            if(!UiTableDic.ContainsKey(uiname))return;
            UIBase Obj = GetUI(uiname).GetComponent<UIBase>();
            Obj.transform.SetAsFirstSibling();
            Obj.Close();
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="uiname">ui名称</param>
        /// <param name="func">委托事件</param>
        /// <typeparam name="T">类型</typeparam>
        public void CloseUI<T>(string uiname, Call<T> func) where T : UIBase
        {
            if(!UiTableDic.ContainsKey(uiname))return;
            var Obj = GetUI(uiname);
            Obj.transform.SetAsFirstSibling();
            var conmop = Obj.GetComponent<T>();
            UIBase @base = Obj.GetComponent<UIBase>();
            func?.Invoke(conmop);
            @base.Close();
        }



        private GameObject LaodInstance(string uiname)
        {
            UITableItem item = tableConfig.Get(uiname);
            if (item == null) throw new Exception("表中没有对应的UI组件");
            var Obj = Instantiate(item.Prefab, GetPanrent(item.Type,item.parentName));
            if(Obj.GetComponent<UIBase>() != null)
                Obj.GetComponent<UIBase>().Init();
            UiTableDic.Add(uiname,Obj);
            return Obj;
        } 
        
        //------------------------------Prefab -------------------------//
         /// <summary>
        /// 获取一个预制体GameObject 对象
        /// </summary>
        /// <param name="uiname">对象名称</param>
        /// <returns>返回GameObject对象</returns>
        /// <exception cref="Exception">如果找不到,则抛出异常</exception>
        public GameObject GetPrefab(string uiname)
        {
            GameObject Obj = prefabConfig.Get(uiname).Item;
            if (Obj == null)
            {
                throw new Exception("UIPrefab表中没有找到 :" + uiname);
            }
            return Obj;
        }

        /// <summary>
        /// 获取一个预制体GameObject对象上的组件
        /// </summary>
        /// <param name="uiname">对象名称</param>
        /// <typeparam name="T">对象类型,该类型必须继承自Component</typeparam>
        /// <returns>返回类型对象</returns>
        /// <exception cref="Exception">如果找不到,则抛出异常</exception>
        public T GetPrefab<T>(string uiname)
        {
            GameObject Obj = GetPrefab(uiname);
            T Tobj = Obj.GetComponent<T>();
            if (Tobj == null)
            {
                throw new Exception("UIPrefab表中没有对应脚本对象 :" + uiname);
            }
            return Tobj;
        }
        
        /// <summary>
        /// 生成一个UI对象
        /// </summary>
        /// <param name="uiname">名称</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T InstanceUI<T>(string uiname) where T : UIBase
        { 
            T Obj = GetPrefab<T>(uiname);
            var ui = Instantiate(Obj);
            ui.GetComponent<UIBase>().Init();
            return ui;
        }
        
        /// <summary>
        /// 生成一个UI对象
        /// </summary>
        /// <param name="uiname">名称</param>
        /// <param name="parent">父级对象</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>返回加载好的对象</returns>
        public T InstanceUI<T>(string uiname,Transform parent) where T : UIBase
        { 
            T Obj = GetPrefab<T>(uiname);
            var ui = Instantiate(Obj.gameObject, parent);
            ui.GetComponent<UIBase>().Init();
            return ui.GetComponent<T>();
        }
        
        /// <summary>
        /// 生成一个UI对象
        /// </summary>
        /// <param name="uiname">名称</param>
        /// <param name="pos">位置</param>
        /// <param name="rotation">旋转</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>返回加载好的对象</returns>
        public T InstanceUI<T>(string uiname,Vector3 pos,Quaternion rotation) where T : UIBase
        { 
            T Obj = GetPrefab<T>(uiname);
            var ui = Instantiate(Obj, pos, rotation);
            ui.GetComponent<UIBase>().Init();
            return ui;
        }

        #endregion
        

        #region ShowHelp

        /// <summary>
        /// 显示提示弹窗
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="des">内容</param>
        /// <param name="quit">退出</param>
        /// <param name="isMask">是否开启遮罩</param>
        public void ShowPopWindows(string title, string des, string quit,bool isMask = false)
        {
            PopSingleton ui = GetUI<PopSingleton>("PopSingleton");
            ui.transform.SetAsLastSibling();
            ui.ShowPopWindows(title,des,quit);
            ui.Open();
            UIMaskManager.Instance.SetMainScnenMask(isMask);
        }

        /// <summary>
        /// 显示提示弹窗
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="des">内容</param>
        /// <param name="quit">退出</param>
        /// <param name="func">点击确认后的回调</param>
        /// <param name="isMask">是否开启遮罩</param>
        public void ShowPopWindows(string title, string des, string quit,Action func,bool isMask = false)
        {
            PopSingleton ui = GetUI<PopSingleton>("PopSingleton");
            ui.transform.SetAsLastSibling();
            ui.ShowPopWindows(title,des,quit,func);
            UIMaskManager.Instance.SetMainScnenMask(isMask);
        }


        /// <summary>
        /// 显示对话弹窗
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="des">内容</param>
        /// <param name="btn1">按钮1 文字</param>
        /// <param name="btn2">按钮2 文字</param>
        /// <param name="func1">按钮1 委托</param>
        /// <param name="func2">按钮2 委托</param>
        /// <param name="isMask">是否开启遮罩</param>
        public void ShowPopDialogue(string title, string des, string btn1, string btn2, Action func1, Action func2,bool isMask = false)
        {
            PopDialogue ui = GetUI<PopDialogue>("PopDialogue");
            ui.transform.SetAsLastSibling();
            ui.ShowPopWindows(title,des,btn1,btn2,func1,func2);
            UIMaskManager.Instance.SetMainScnenMask(isMask);
        }

        /// <summary>
        /// 显示提示弹窗
        /// </summary>
        /// <param name="description">文本描述</param>
        /// <param name="waitTime">等待时长</param>
        /// <param name="tiem">动画时长</param>
        /// <param name="endAction">结束回调</param>
        /// <param name="waitAction">等待时回调</param>
        public void ShowTips(string description,float waitTime =0.35f,float tiem =0.25f,Action endAction = null,
            Action waitAction = null)
        {
            PopTips ui = GetUI<PopTips>("PopTips");
            ui.transform.SetAsLastSibling();
            ui.Show(description,waitTime,tiem);
        }

        /// <summary>
        /// 显示提示弹窗
        /// </summary>
        /// <param name="description">文本内容</param>
        /// <param name="waitAction">等待这个协程执行完毕后再执行下一个time</param>
        /// <param name="tiem">动画总过度时间</param>
        /// <param name="endAction">结束委托</param>
        public void ShowTips(string description,IEnumerator waitAction, float tiem = 0.25f, Action endAction = null)
        {
            PopTips ui = GetUI<PopTips>("PopTips");
            ui.transform.SetAsLastSibling();
            ui.Show(description,waitAction,endAction,tiem);
        }
        
        /// <summary>
        /// 显示倒计时UI
        /// </summary>
        /// <param name="time">倒计时时间</param>
        /// <param name="func">倒计时结束回调函数</param>
        public void DownTime(float time, Action func)
        {
            DownTime downTime = GetUI<DownTime>("DownTime");
            downTime.transform.SetAsLastSibling();
            downTime.DownTiem(time,func);
        }

        #endregion
    }
}

