using System;
using QuizCannersUtilities;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using UnityEngine;

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Deck", menuName = "DeckRenderer/Deck")]
    public class CardsRepository : ScriptableObject, IPEGI, IPEGI_ListInspect
    {
        public QcGoogleSheetParcer parcerForCards = new QcGoogleSheetParcer();

        protected CardsRenderer Rederer => CardsRenderer.instance;

        protected List<CardPrototype> cards = new List<CardPrototype>();



        protected IEnumerator Download()
        {
            parcerForCards.StartDownload();

            yield return parcerForCards.DownloadingCoro();

            parcerForCards.DecodeList_Indexed(cards);
        }


        #region Inspector

        private int _inspectedStuff = -1;
        private int _inspectedCard = -1;

        public bool Inspect()
        {
            var changed = false;

            if (parcerForCards.IsDownloading() == false)
            {
                if ("Download & Update {0} List".F(parcerForCards.SelectedPage.NameForDisplayPEGI()).Click())
                    CardsRenderer.instance.StartCoroutine(Download());
            }
            else
            {
                "Updating...".write(toolTip: "Downloading...");
            }

            pegi.nl();

            parcerForCards.enter_Inspect(ref _inspectedStuff, 0).nl();//Nested_Inspect();
            
            "Cards".enter_List(ref cards, ref _inspectedCard, ref _inspectedStuff, 1).nl();

            return changed;
        }

        public bool InspectInList(IList list, int ind, ref int edited)
        {
            name.write();

            if (parcerForCards.IsDownloading() == false)
            {
                if (icon.Download.Click())
                    CardsRenderer.instance.StartCoroutine(Download());
            }
            else
            {
                icon.Wait.write(toolTip: "Downloading...");
            }
        
            if (icon.Enter.Click())
                edited = ind;

            this.ClickHighlight();

            return false;
        }

        #endregion

    }
}