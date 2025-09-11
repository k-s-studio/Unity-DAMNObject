using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.KsCode.DAMNObject {
    [Serializable]
    public struct KeyCompPair {
        [SerializeReference] public IKey Key; // magic!
        public Component Comp;
        public KeyCompPair(IKey key, Component comp) {
            Key = key;
            Comp = comp;
        }
        public static KeyCompPair New(IKey key, Component comp) => new(key, comp);
        // operators
        public static implicit operator (IKey, Component)(KeyCompPair pair) => (pair.Key, pair.Comp);
        public static implicit operator KeyCompPair((IKey key, Component comp) t) => new(t.key, t.comp);
        public static explicit operator KeyValuePair<object, Component>(KeyCompPair pair) => KeyValuePair.Create<object, Component>(pair.Key, pair.Comp);

        public interface IKey {
            public string ToString();
            public int GetHashCode();
            public bool Equals(object obj);
            public Type GetType();
            protected static bool IsValidType(object o) => o is string || o is ValueType;
        }
    }
    [Serializable]
    public struct KeyString : KeyCompPair.IKey { // To differentiate from string
        [SerializeField] private string str;
        public KeyString(object o) { str = o.GetDescription(); }
        public override readonly string ToString() => str;
        public override readonly int GetHashCode() => str.GetHashCode();
        public override readonly bool Equals(object obj) => obj == null || str.GetHashCode() == obj.GetDescription().GetHashCode();
        // operators
        public static implicit operator string(KeyString ks) => ks.ToString();
    }

    [Serializable]
    public struct Key<T> : KeyCompPair.IKey {
        [SerializeField] private string m_Dscrption;
        [SerializeField] private int m_HashCode;
        public Key(object o) {
            m_Dscrption = o.GetDescription();
            m_HashCode = o.GetHashCode();
        }
        public static Key<TObject> New<TObject>(TObject obj) => new(obj);
        public override readonly string ToString() => m_Dscrption;
        public readonly override int GetHashCode() => m_HashCode;
        public readonly override bool Equals(object obj) => obj != null && obj.GetType() == GetType() && m_HashCode == obj.GetHashCode();
        public new readonly Type GetType() => typeof(T);
    }

    [Serializable]
    public struct Key : KeyCompPair.IKey {
        [SerializeField] private string m_Dscrption;
        [SerializeField] private int m_HashCode;
        [SerializeField] private string m_TypeFullName;
        public Key(object o) {
            if (o == null) throw new ArgumentException("null o in creating Key.");
            m_Dscrption = o.GetDescription();
            m_HashCode = o.GetHashCode();
            m_TypeFullName = o.GetType().FullName;
            if (!KeyCompPair.IKey.IsValidType(o))
                Debug.LogWarningFormat("'{0}' may have unstable hashcode through serialization.", m_Dscrption);
        }
        public override readonly string ToString() => m_Dscrption;
        public readonly override int GetHashCode() => m_HashCode;
        public readonly override bool Equals(object obj) => obj != null && obj.GetType().FullName == m_TypeFullName && obj.GetHashCode() == m_HashCode;
    }
}