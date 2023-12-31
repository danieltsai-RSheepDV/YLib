
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Library.Serializable_Dictionary.Editor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
    public class SerializableDictionaryUIE : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();
            
            var kvpairs = new PropertyField(property.FindPropertyRelative("pairs"));
            kvpairs.label = property.displayName;
            container.Add(kvpairs);

            return container;
        }

        private static string RootPath
        {
            get
            {
                var g = AssetDatabase.FindAssets ( $"t:Script {nameof(SerializableDictionaryUIE)}" );
                var path = AssetDatabase.GUIDToAssetPath(g[0]);
                return path.Substring(0, path.Length - ("SerializableDictionaryUIE.cs").Length);;
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>.KVPair), true)]
    public class SerializableDictionaryKVPairUIE : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();
            
            var key = new PropertyField(property.FindPropertyRelative("key"));
            var value = new PropertyField(property.FindPropertyRelative("value"));
            container.Add(key);
            container.Add(value);

            return container;
        }
    }
}
