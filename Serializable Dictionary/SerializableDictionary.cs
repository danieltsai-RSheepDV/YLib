using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Library.Serializable_Dictionary
{
    [Serializable]
    public class SerializableDictionary<TU, TV> : IEnumerable
    {
        //Scythe Dictionary
        [Serializable]
        public class KVPair
        {
            public TU key;
            public TV value;

            public KVPair(TU k, TV v)
            {
                key = k;
                value = v;
            }
        }
        public List<KVPair> pairs;
        private Dictionary<TU, int> cache = new();

        public TV this[TU key]
        {
            get
            {
                if (cache.ContainsKey(key))
                {
                    return pairs[cache[key]].value;
                }
                else
                {
                    KVPair searchResult = pairs.Find((kvpair) => kvpair.key.Equals(key));
                    int index = pairs.IndexOf(searchResult);
                    if (index >= 0)
                    {
                        cache.Add(key, index);
                        return searchResult.value;
                    }
                    else
                    {
                        throw new KeyNotFoundException();
                    }

                }
            }
            set
            {
                if (cache.ContainsKey(key))
                {
                    pairs[cache[key]].value = value;
                }
                else
                {
                    KVPair searchResult = pairs.Find((kvpair) => kvpair.key.Equals(key));
                    int index = pairs.IndexOf(searchResult);
                    
                    if (index >= 0)
                    {
                        cache.Add(key, index);
                        pairs[index].value = value;
                    }
                    else
                    {
                        throw new KeyNotFoundException();
                    }
                }
            }
        }

        public bool HasKey(TU key)
        {
            if (cache.ContainsKey(key))
            {
                return true;
            }
            else
            {
                KVPair searchResult = pairs.Find((kvpair) => kvpair.key.Equals(key));
                int index = pairs.IndexOf(searchResult);
                if (index >= 0)
                {
                    cache.Add(key, index);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Add(TU key, TV value)
        {
            if (key == null) throw new ArgumentNullException();
            else if(HasKey(key)) throw new Exception("key already added");
            else
            {
                cache.Add(key, pairs.Count);
                pairs.Add(new KVPair(key, value));
            }
        }

        public bool Remove(TU key)
        {
            if (key == null) throw new ArgumentNullException();
            else if (!HasKey(key)) return false;
            else
            {
                if (cache.ContainsKey(key))
                {
                    pairs.RemoveAt(cache[key]);
                    cache.Remove(key);
                }
                else
                {
                    pairs.Remove(pairs.Find((kvpair) => kvpair.key.Equals(key)));
                }

                return true;
            }
        }

        public void Dump()
        {
            Debug.Log(pairs);
            foreach (var kvp in cache) {
                Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return pairs.GetEnumerator();
        }
    }
}