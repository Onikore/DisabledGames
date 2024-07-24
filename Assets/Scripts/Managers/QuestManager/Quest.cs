using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers.QuestManager
{
    public class Quest : MonoBehaviour
    {
        public event Action<Quest> OnQuestCompleted;

        private IEnumerable<QuestableEntity> questableEntities;
        private TextMeshProUGUI textMeshProUGUI;
        private QuestData questData;

        private Quest previousQuest;

        private bool isLastPage;

        public void Initialize(QuestData questData, Quest previousQuest, bool isLastPage)
        {
            this.questData = questData;
            this.previousQuest = previousQuest;
            this.isLastPage = isLastPage;

            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
            questableEntities = FindObjectsOfType<CustomTag>()
                .Where(x => x.HasTag(questData.TagEntityToCount))
                .Select(x => (QuestableEntity)x.GetComponent(questData.QuestType));

            foreach (var questableEntity in questableEntities)
                questableEntity.OnQuestCompleted += OnQuestableEntityComplete;
           

            textMeshProUGUI.text = questData.Instruction + "\n" + questableEntities.Count(x => x.IsCompleted == false);

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            GetComponentsInChildren<Button>().First(x => x.CompareTag("NextPageButton")).interactable = false;

            if (previousQuest != null)
            {
                var prevButton = GetComponentsInChildren<Button>().First(x => x.CompareTag("PreviousPageButton"));
                var nextButton = previousQuest.GetComponentsInChildren<Button>().First(x => x.CompareTag("NextPageButton"));

                prevButton.onClick.AddListener(TurnPage(gameObject, previousQuest.gameObject));
                nextButton.onClick.AddListener(TurnPage(previousQuest.gameObject, gameObject));
            }

            GetComponentsInChildren<Button>().First(x => x.CompareTag("PreviousPageButton")).interactable = previousQuest != null;
        }

        private static UnityAction TurnPage(GameObject turnOff, GameObject turnOn)
        {
            return () =>
            {
                turnOff.SetActive(false);
                turnOn.SetActive(true);
            };
        }

        private void OnQuestableEntityComplete(QuestableEntity entity)
        {
            textMeshProUGUI.text = questData.Instruction + "\n" + questableEntities.Count(x => x.IsCompleted == false);
            entity.OnQuestCompleted -= OnQuestableEntityComplete;
        


            Debug.Log("Questable entity complete");
            if (questableEntities.All(x => x.IsCompleted))
            {
                AudioManager.instance.Play("QuestFinished");
                Debug.Log("Quest complete");
                OnQuestCompleted?.Invoke(this);

                if (!isLastPage)
                    GetComponentsInChildren<Button>().First(x => x.CompareTag("NextPageButton")).interactable = true;
            }
        }
    }
}
