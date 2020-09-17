using QuizCannersUtilities;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Deck", menuName = "DeckRenderer/Deck")]
    public class CardsRepository : ScriptableObject, IPEGI, IPEGI_ListInspect
    {
        public QcGoogleSheetParcer parcerForCards = new QcGoogleSheetParcer();

        protected CardsRenderer Rederer => CardsRenderer.instance;

        public CardDesignBase cardDesignPrefab;

        [SerializeField] public List<CardPrototypeBase> cards = new List<CardPrototypeBase>();
        
        protected IEnumerator Download()
        {
            parcerForCards.StartDownload();

            yield return parcerForCards.DownloadingCoro();

            parcerForCards.DecodeList_Indexed(cards);
        }


        #region Inspector

        private int _inspectedStuff = -1;
        private int _inspectedCard = -1;

        public static CardsRepository inspected;

        public bool Inspect()
        {
            inspected = this;

            var changed = pegi.toggleDefaultInspector(this);
            
            if (_inspectedStuff == -1 && cardDesignPrefab)
            {
                if (parcerForCards.IsDownloading() == false)
                {

                    if (Rederer.IsRendering == false &&
                        "Render".ClickConfirm(confirmationTag: "rndDeck", toolTip: "Render all cards?"))
                        Rederer.RenderAllTheCards(this);

                    if ("Download & Update {0} List".F(parcerForCards.SelectedPage.NameForDisplayPEGI()).Click())
                        CardsRenderer.instance.StartCoroutine(Download());
                }
                else
                {
                    "Updating...".write(toolTip: "Downloading...");
                }
            }

            pegi.nl();

            parcerForCards.enter_Inspect(ref _inspectedStuff, 0).nl();//Nested_Inspect();
            
            "Cards".enter_List(ref cards, ref _inspectedCard, ref _inspectedStuff, 1).nl();

            if (_inspectedStuff == -1 || !cardDesignPrefab)
                "Card Design".edit(90, ref cardDesignPrefab).nl();

            inspected = null;

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

#if UNITY_EDITOR
    [CustomEditor(typeof(CardsRepository))]
    public class CardsRepositoryDrawer : PEGI_Inspector_SO<CardsRepository> { }
#endif

}