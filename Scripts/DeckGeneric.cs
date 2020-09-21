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
    public abstract class DeckBase : ScriptableObject, IPEGI, IPEGI_ListInspect
    {
        protected CardsRenderer DeckRederer => CardsRenderer.instance;

        public CardDesignBase cardDesignPrefab;

        public QcGoogleSheetToCfg parcerForCards = new QcGoogleSheetToCfg();

        public Vector2 cardResolution = new Vector2(2500, 3500);


        protected abstract IEnumerator Download();

        public abstract IEnumerator<CardPrototypeBase> GetEnumerator();

        #region Inspector
        public static DeckBase inspected;

        public abstract bool Inspect();
        
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

    public abstract class DeckGeneric<T> : DeckBase where T: CardPrototypeBase, new()
    {
        [SerializeField] public List<T> cards = new List<T>();

        public override IEnumerator<CardPrototypeBase> GetEnumerator() => cards.GetEnumerator();

        protected override IEnumerator Download()
        {
            parcerForCards.StartDownload();

            yield return parcerForCards.DownloadingCoro();

            parcerForCards.DecodeList_Indexed(cards);
        }

        private int _inspectedStuff = -1;
        private int _inspectedCard = -1;


        public override bool Inspect()
        {
            inspected = this;

            var changed = pegi.toggleDefaultInspector(this);

            if (_inspectedStuff == -1 && cardDesignPrefab)
            {
                if (parcerForCards.IsDownloading() == false)
                {

                    if (DeckRederer.IsRendering == false &&
                        "Render".ClickConfirm(confirmationTag: "rndDeck", toolTip: "Render all cards?"))
                        DeckRederer.RenderAllTheCards(this);

                    if (parcerForCards.SelectedPage != null)
                    {

                        if ("Download & Update {0} List".F(parcerForCards.SelectedPage.NameForDisplayPEGI()).Click())
                            CardsRenderer.instance.StartCoroutine(Download());
                    } else "NO GOOGLE SHEET PAGES".write();
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

            if (_inspectedStuff == -1)
            {
                "Card resolution".edit(ref cardResolution).nl(ref changed);
            }

            inspected = null;

            return changed;
        }


    }
}