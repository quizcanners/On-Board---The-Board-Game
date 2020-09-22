using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeckRenderer.OnBoard
{
    public class DestinationCardDesign : CardDesignGeneric<DestinationCardPrototype>
    {
        public Color safePlanetColor;
        public Color unsafePlanetColor;
        public List<Image> imagesToColor; 

        public override void Fill(DestinationCardPrototype prot)
        {
            imagesToColor.TrySetColor_RGB(prot.isSafeToLand ? safePlanetColor : unsafePlanetColor);

        }
    }
}
