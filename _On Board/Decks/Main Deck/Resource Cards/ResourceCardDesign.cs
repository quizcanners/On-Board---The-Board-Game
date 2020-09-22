using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    public class ResourceCardDesign : CardDesignGeneric<ResourceCardPrototype>
    {
        [Header("Impostor section")]
        public TextMeshProUGUI SabotagueTypeRequirement;
        public TextMeshProUGUI SabotagueResult;
        public TextMeshProUGUI SabotagueDescription;

        public override void Fill(ResourceCardPrototype prot)
        {
            TrySet(SabotagueTypeRequirement, prot.sabotagueType);
            TrySet(SabotagueDescription, prot.sabotague);

        }
    }
}
