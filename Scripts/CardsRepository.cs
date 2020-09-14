using QuizCannersUtilities;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using UnityEngine;

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Deck", menuName = "DeckRenderer/Deck")]
    public class CardsRepository : ScriptableObject, IPEGI
    {
        public QcGoogleSheetParcer parcerForCards = new QcGoogleSheetParcer();

        public bool Inspect()
        {
            var changed = false;
            parcerForCards.Nested_Inspect();



            return changed;
        }
    }
}