using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    public class BlackoutCardDesign : CardDesignGeneric<BlackoutPrototype>
    {
        [Header("Impostor section")]
        public TextMeshProUGUI SabotagueTypeRequirement;
        public TextMeshProUGUI SabotagueResult;
        public TextMeshProUGUI SabotagueDescription;

        public override void Fill(BlackoutPrototype prot)
        {
            TrySet(SabotagueTypeRequirement, prot.power.ToString());
            TrySet(SabotagueDescription, prot.sabotague);


        }
    }
}
