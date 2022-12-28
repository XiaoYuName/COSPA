
/// <summary>
/// 非继承自MonoBehaviour的单例类
/// </summary>
/// <typeparam name="T">该类必须可New()</typeparam>
public class Singleton<T> where T: new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}
