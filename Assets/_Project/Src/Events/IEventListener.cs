namespace Ransomink.Events
{
    public interface IEventListener<T>
    {
        //void OnEnable();
        //void OnDisable();
        //void OnValidate();
        void OnEventRaised(T value);
    }
}
