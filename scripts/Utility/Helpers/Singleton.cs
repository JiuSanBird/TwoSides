/// <summary>
/// 单例模式
/// 是否指向同一个单例,只与泛型T有关,单例模式限定着,同一个类型只能有一个
/// 所以说继承继承单例是没有意义的
/// 饱汉模式,线程不安全
/// </summary>
public abstract class Singleton<T> where T : class, new()
{
    protected Singleton() { }
    private static T instance;
    public static T Instance => instance ??= new T();
}