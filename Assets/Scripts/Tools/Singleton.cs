using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Singleton<T> //约束该类必须被继承
{
    private static T instance;
    
    public static T Instance => instance; //属性保护字段,让其只为Get,不能Set
    
    public static bool IsInitialized => instance != null; //用于判断是否为空
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T) this;
        }
    }
    
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; //销毁时,赋值为空
        }
        
    }
}
