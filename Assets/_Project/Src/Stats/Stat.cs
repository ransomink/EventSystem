using System.Collections.Generic;
using UnityEngine;

namespace Ransomink.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float value;
        [SerializeField] private float baseValue;
        [SerializeField] private List<Modifier> modifiers;

        public float Value 
        { 
                    get 
                    { 
                        if (_isDirty) 
                        {
                            value    = _value = FinalValue(); 
                            _isDirty = false;
                        }
                        return value;
                    }
            private set => this.value = value;
        }

        public IReadOnlyList<Modifier> Modifiers => modifiers.AsReadOnly();

        public float FinalValue()
        {
            return default;
        }

        public void AddModifier(Modifier mod)
        {
            
        }

        public void RemoveModifier(Modifier mod)
        {
            
        }

        private float _value;
        private bool  _isDirty;
    }
}
