using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zinnor.Tactics.Scriptables
{
    public class ScriptableSet<T> : ScriptableObject, IEnumerable<T>
    {
        public List<T> Items;

        public int Count => Items.Count;

        public T this[int index] => Items[index];

        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public void Add(T newItem)
        {
            if (newItem == null)
            {
                return;
            }

            if (Items.Contains(newItem))
            {
                return;
            }

            Items.Add(newItem);
        }

        public void Remove(T newItem)
        {
            Items.Remove(newItem);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}