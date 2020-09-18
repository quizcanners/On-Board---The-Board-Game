using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer
{
    public class CardDesignBase : MonoBehaviour, IPEGI
    {
        public TextMeshProUGUI Type;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Lore;




        [NonSerialized] protected CardPrototypeBase activePrototype;

        public CardPrototypeBase ActivePrototype
        {
            get
            {
                return activePrototype;
            }
            set
            {
                activePrototype = value;
                Fill();
            }
        }

        protected void TrySet(TextMeshProUGUI element, string text)
        {
            if (element)
                element.text = text;
        }

        protected void TrySetEnable(Component cmp, bool value)
        {
            if (cmp)
                cmp.gameObject.SetActive(value);
        }

        public virtual void Fill()
        {
            if (activePrototype != null)
            {
                TrySet(Name, activePrototype.NameForPEGI);
                TrySet(Description, activePrototype.description);
                TrySet(Lore, activePrototype.lore);
            }
            else
            {
                TrySet(Name, "No Prototype");
                TrySet(Description, "No Prototype");
                TrySet(Lore, "No Prototype");
            }
        }

        #region Inspector

        public bool Inspect()
        {
            var changed = false;

            if (activePrototype != null)
            {
                activePrototype.Nested_Inspect().nl(ref changed);
            }

            if (changed)
                Fill();

            return changed;
        }

        #endregion
    }
}
