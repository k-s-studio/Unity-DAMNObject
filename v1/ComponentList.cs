using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.KsCode.DAMNObject.v1 {

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