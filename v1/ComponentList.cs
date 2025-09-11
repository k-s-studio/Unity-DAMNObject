using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.KsCode.DAMNObject.v1 {
    //[Serializable] //Type要可序列化，(Type type, object id)要可序列化
    [Obsolete]
    public class ComponentLst : IEnumerable<(Type type, object id)> {
        public Type type;
        public object id; //string, int, enum, ...whatever recognizable for user
        private readonly List<(Type type, object id)> items = new(); //最後都是加進List => GetEnumerator()，一定得初始化這個List
        public IEnumerator<(Type type, object id)> GetEnumerator() {
            if (type != null) yield return (type, id);
            foreach (var i in items) yield return i;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Add(Type t, object id) {
            // if (!(id == null || id.GetType().IsSerializable)) { 
            //     Debug.LogWarning($"{id.GetDescription()}) is not serializable. Please use serializable key instead e.g. string, Enum,...");
            //     id = null;
            // }
            items.Add((t, id)); //IKey saves everything
        }
        // public void Add(Type t, Enum id) => items.Add((t, id));
        // public void Add(Type t, string id) => items.Add((t, id));
        public void Add(Type t) => items.Add((t, null));
    }

    public abstract class CompCollector : IEnumerable<(Type type, object id)> {
        public Type type;
        public object id;
        public abstract IEnumerator<(Type type, object id)> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract void Add(Type t, object id);
        public abstract void Add(Type t);

        public class CompList : CompCollector {
            private readonly List<(Type type, object id)> items = new();
            public override IEnumerator<(Type type, object id)> GetEnumerator() => items.GetEnumerator();
            public override void Add(Type t, object id) => items.Add((t, id));
            public override void Add(Type t) => items.Add((t, null));
            public void Add(CompCollector c) => items.Add((c.type, c.id));
        }
    }
    public class CompCollectorIntg : IEnumerable<(Type type, object id)> {
        public Type type = null;
        public object id = null;
        private readonly List<(Type type, object id)> items = new();
        public void Add(Type t, object id) => items.Add((t, id));
        public void Add(Type t) => items.Add((t, null));
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<(Type type, object id)> GetEnumerator() {
            // Data in fields
            if (type != null) yield return (type, id);

            // Data in list
            using var itr = items.GetEnumerator();
            while (itr.MoveNext()) yield return itr.Current;
        }
    }
    /// <summary>
    /// Linked list version of CompCollector
    /// </summary>
    public class ComponentNode : IEnumerable<(Type type, object id)> {
        public Type type = null;
        public object id = null;
        public ComponentNode next;
        public ComponentNode(Type type = null, object id = null) {
            this.type = type;
            this.id = id;
            next = null;
        }
        private bool NoData => type == null;
        private bool IsLast => next is null || next.NoData;
        private (Type type, object id) Data => (this.type, this.id);
        public void Add(Type t, object o) {
            // if (NoData) {
            //     this.type = t;
            //     this.id = o;
            // }
            // else {
            //     ref var ptr = ref next;
            //     while (!ptr.IsLast) ptr = ptr.next;
            //     ptr = new(t, o);
            // }
            ComponentNode newNode = new(t, o) { next = this.next };
            this.next = newNode;
        }
        public void Add(Type t) => Add(t, null);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<(Type type, object id)> GetEnumerator() {
            if (!NoData) yield return Data;
            for (var e = next; !e?.NoData ?? false; e = e.next)
                yield return e.Data;
        }
    }
}