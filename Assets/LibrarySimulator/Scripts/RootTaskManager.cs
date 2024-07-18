using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class RootTaskManager : MonoBehaviour
{
	public Transform taskTransform;
	public GameObject[] taskPrefabs;
	
	public float menuFadeInSeconds;
	public RectTransform taskSelectPanel;
	
	public float taskCompleteFadeOutSeconds;
	public TextMeshProUGUI taskCompleteText;
	
	public UnityEngine.Rendering.Volume bgVolume;
	
	public AudioClip completeSound;
	

	private GameObject currentTask;

	private bool open = true;
	private float fadeInFrac = 0;
	private float taskSelectHeight;
	
	private float fadeOutFrac = 0.1f;
	
	private float openDelay = -1;
	
    // Start is called before the first frame update
    void Start()
    {
        taskSelectHeight = taskSelectPanel.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Backspace))
			openDelay = 0.1f;
		
		if (openDelay > 0) {
			openDelay -= Time.deltaTime;
			if (openDelay <= 0) {
				open = true;
				openDelay = -1;
                EndTask();
            }
		}
		
        var delta = Time.deltaTime / menuFadeInSeconds;
		if (open)
			fadeInFrac = Mathf.Min(fadeInFrac + delta, 1f);
		else
			fadeInFrac = Mathf.Max(fadeInFrac - delta, 0f);
		
		var height = taskSelectHeight * fadeInFrac;
		if (Mathf.Abs(taskSelectPanel.rect.height - height) > 0.01f)
			taskSelectPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

		bgVolume.weight = fadeInFrac;
		
		if (fadeOutFrac <= 0)
			return;
		
		delta = Time.deltaTime / taskCompleteFadeOutSeconds;
		fadeOutFrac = Mathf.Max(fadeOutFrac - delta, 0f);
		
		var col = taskCompleteText.color;
		col.a = fadeOutFrac;
		taskCompleteText.color = col;
    }
	
	public void StartTask(int index) {
		EndTask();
        open = false;

        var prefab = taskPrefabs[index];
		if (prefab == null)
			return;

        currentTask = Instantiate(prefab, taskTransform);
        currentTask.GetComponentInChildren<TaskBase>()
            .onTaskCompleted += OnTaskCompleted;
	}
	
	public void EndTask()
	{
		if (currentTask == null)
			return;

        Destroy(currentTask);
        currentTask = null;
    }

	private void OnTaskCompleted() {
		fadeOutFrac = 2.0f;
		AudioSource.PlayClipAtPoint(
			completeSound,
			taskCompleteText.transform.position,
			0.5f
		);
		
		openDelay = 1.0f;
	}
}
