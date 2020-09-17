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

        [Header("Impostor section")]
        public TextMeshProUGUI SabotagueTypeRequirement;
        public TextMeshProUGUI SabotagueResult;
        public TextMeshProUGUI SabotagueDescription;


        [NonSerialized] private CardPrototypeBase _activePrototype;

        public CardPrototypeBase ActivePrototype
        {
            get
            {
                return _activePrototype;
            }
            set
            {
                _activePrototype = value;
                Fill();
            }
        }

        protected void TrySet(TextMeshProUGUI element, string text)
        {
            if (element)
                element.text = text;
        }

        public void Fill()
        {
            if (_activePrototype != null)
            {
                TrySet(Name, _activePrototype.NameForPEGI);
                TrySet(Description, _activePrototype.description);
            }
            else
            {
                TrySet(Name, "No Prototype");
                TrySet(Description, "No Prototype");
            }
        }

        #region Inspector

        public bool Inspect()
        {
            var changed = false;

            if (_activePrototype != null)
            {
                _activePrototype.Nested_Inspect().nl(ref changed);
            }

            if (changed)
                Fill();

            return changed;
        }

        #endregion
    }
}
