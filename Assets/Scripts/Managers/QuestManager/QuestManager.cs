using System.Collections.Generic;
using System.Linq;
using Managers.QuestManager;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private IEnumerable<WaterableEntityBehaviour> waterBehaviours;
    [SerializeField] private List<QuestData> Quests;
    [SerializeField] private Quest questPrefab;
    [SerializeField] private Canvas tabletCanvas;


    private void Start()
    {
        Quest previousQuest = null;

        for (var i = 0; i < Quests.Count; i++)
        {
            var quest = Quests[i];
            //var questObjects = FindObjectsOfType<CustomTag>().Where(x => x.HasTag(quest.TagEntityToCount)).Select(x => x.GetComponent<QuestableEntity>());
            var instance = Instantiate(questPrefab, tabletCanvas.transform);
            instance.Initialize(quest, previousQuest, i == Quests.Count - 1);

            if (previousQuest != null)
            {
                instance.gameObject.SetActive(false);
            }

            previousQuest = instance;

            // var ugui = questPrew.GetComponentInChildren<TextMeshProUGUI>();
            // var act = new Action(() => ugui.text = quest.Instruction + "\n" + questObjects.Count(x => x.IsCompleted == false));
            //  StartCoroutine(UpdateText(act, 1));
            /*if (previousQuest != null)
            {
                var prevButton = currentQuest.GetComponentsInChildren<Button>().First(x => x.CompareTag("PreviousPageButton"));
                var nextButton = previousQuest.GetComponentsInChildren<Button>().First(x => x.CompareTag("NextPageButton"));

                prevButton.onClick.AddListener(TurnPage(currentQuest, previousQuest));
                nextButton.onClick.AddListener(TurnPage(previousQuest, currentQuest));
                currentQuest.SetActive(false);
            }*/
        }
    }

    /*private static UnityAction TurnPage(GameObject turnOff, GameObject turnOn)
    {
        return () =>
        {
            turnOff.SetActive(false);
            turnOn.SetActive(true);
        };
    }*/

    /*
    private IEnumerator UpdateText(Action action, float waitTime)
    {
        while (true)
        {
            action.Invoke();
            yield return new WaitForSeconds(waitTime);
        }
    }*/
}