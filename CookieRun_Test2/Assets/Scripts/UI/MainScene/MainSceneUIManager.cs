using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    [SerializeField]
    private List<PopBase> popUps = new List<PopBase>();

    private PopBase curPop;

    [SerializeField]
    private Text txtGold;

    [SerializeField]
    private Transform curUnitPos;

    [SerializeField]
    private Text txtSelectUnitName;


    private void Awake()
    {
        if (popUps!=null)
        {
            for (int i = 0; i < popUps.Count; i++)
            {
                if (popUps[i].gameObject.activeSelf == true)
                    popUps[i].gameObject.SetActive(false);
            }
        }

        if(txtGold != null)
        {
            txtGold.text = GameData.Instance.playerScore.ToString();
        }

        if (curUnitPos != null)
        {
            GameObject go = GameData.Instance.GetLastSelectUnit();
            Image unit = (new GameObject()).AddComponent<Image>();
            unit.sprite = go.GetComponent<SpriteRenderer>().sprite;
            Transform StartTransform = curUnitPos;
            Instantiate(unit, StartTransform);
        }

        if (txtSelectUnitName != null)
        {
            txtSelectUnitName.text = GameData.Instance.GetLastSelectUnitName();
        }
    }


    public void OnClickSelectChar()
    {
        if (curPop != null)
        {
            PopSelectChar pop = curPop.GetComponent<PopSelectChar>();

            if(pop != null)
            {
                if (pop.gameObject.activeSelf)
                    return;

                pop.gameObject.SetActive(true);
                pop.OpenUI(this.gameObject);
            }
            else
            {
                curPop.gameObject.SetActive(false);

                for (int i = 0; i < popUps.Count; i++)
                {
                    pop = popUps[i].gameObject.GetComponent<PopSelectChar>();

                    if (pop != null)
                    {
                        pop.gameObject.SetActive(true);
                        pop.OpenUI(this.gameObject);

                        curPop = pop;
                        return;
                    }
                }
            }
        }
        else
        {
            PopSelectChar pop = null;
            for (int i = 0; i < popUps.Count; i++)
            {
                pop = popUps[i].gameObject.GetComponent<PopSelectChar>();

                if (pop != null)
                {
                    pop.gameObject.SetActive(true);
                    pop.OpenUI(this.gameObject);

                    curPop = pop;
                    return;
                }
            }
        }
    }
}
