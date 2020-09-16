using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer
{
    public class CardDesign : MonoBehaviour, IPEGI
    {
        public TextMeshProUGUI Type;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;

        [Header("Impostor section")]
        public TextMeshProUGUI SabotagueTypeRequirement;
        public TextMeshProUGUI SabotagueResult;
        public TextMeshProUGUI SabotagueDescription;


        [NonSerialized] private CardPrototype _activePrototype;

        public CardPrototype ActivePrototype
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

        public void Fill()
        {
            if (_activePrototype != null)
            {
                Name.text = _activePrototype.NameForPEGI;
                Description.text = _activePrototype.description;
            }
            else
            {
                Name.text = "No Prototype";
                Description.text = "No Prototype";
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
