using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components.Abstract;
using System;

namespace Danish.Components
{

    public class dComponentHolder : MonoBehaviour
    {
        public List<dBaseComponent> components = new List<dBaseComponent>();

        private Dictionary<string, dBaseComponent> AvailableComponents = new Dictionary<string, dBaseComponent>();


        void populateDictionary(List<dBaseComponent> componentList)
        {
            foreach(var comp in componentList)
            {
                AvailableComponents.Add(comp.valueKey, comp);
            }
        }

        private void Start()
        {
            foreach (var comp in components)
            {
                comp.Init();
            }

            populateDictionary(components);

            Debug.Log(AvailableComponents.Count);
        }

        public dBaseComponent FindInDictionary(string key)
        {
            dBaseComponent result = null;

            if (!AvailableComponents.ContainsKey(key))
            {
                return null;
            }

            if(AvailableComponents.TryGetValue(key, out dBaseComponent _obj))
            {
                result = _obj;
            }

            return result;
        }
    }
}