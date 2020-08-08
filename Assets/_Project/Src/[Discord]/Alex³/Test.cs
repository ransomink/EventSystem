using System;
using UnityEngine;

[System.Serializable]
public abstract class AbstractBar<T> : MonoBehaviour where T : AbstractFoo
{
    public T test;
    public T Test { get => test; private set => value = test; }
    public Component BaseType => GetComponent(this.GetType());

    public void Init(T test)
    {
        Test = test;
    }
}

[System.Serializable]
public class Test : AbstractBar<Foo>
{
    public Component Type => GetComponent(this.GetType());

    private void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var component = Type;
            Debug.Log($"Component Type: {component}");
            Debug.Log($"Component is AbstractBar<AbstractFoo>: {component is AbstractBar<AbstractFoo>}");
            var baseComponent = BaseType;
            Debug.Log($"BaseComponent Type: {baseComponent}");
            Debug.Log($"BaseComponent is AbstractBar<AbstractFoo>: {baseComponent is AbstractBar<AbstractFoo>}");
            // im trying to get `.test` of `component`
        }
    }
}

[System.Serializable]
public class Bar : AbstractBar<Foo> {}

[System.Serializable]
public abstract class AbstractFoo {}

[System.Serializable]
public class Foo : AbstractFoo {}
