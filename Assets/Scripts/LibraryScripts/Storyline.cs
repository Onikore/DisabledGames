using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Storyline : MonoBehaviour
{
    public bool startImmediately;

    public TextMeshProUGUI pcText;

    public TabletAttachment tablet;
    public HintManager hintManager;

    public ScannableBook book;
    public BookScanner scanner;

    public Light scannerLight;

    //public XRPushButton keyboardEnter;

    public Transform xrOrigin, initialPosition;

    private bool enterPressed = false;

    private Coroutine current;

    void Start()
    {
        if (hintManager == null)
            hintManager = FindObjectOfType<HintManager>();

        /*
        keyboardEnter.onPress.AddListener(
            new UnityEngine.Events.UnityAction(
                () => enterPressed = true
            )
        );
        */

        if (startImmediately)
            Begin();
    }

    public void Begin(Action callback=null)
    {
        xrOrigin.SetPositionAndRotation(initialPosition.position, initialPosition.rotation);
        current = StartCoroutine(RunStory(callback));
    }

    public void ResetStory()
    {
        if (current != null)
            StopCoroutine(current);

        pcText.text = "";
        hintManager.SetHint("");
    }

    private IEnumerator RunStory(Action callback)
    {
        hintManager.SetHint("");
        pcText.text = "Сканер:\n    Ожидание...";
        scannerLight.enabled = false;

        yield return new WaitForSeconds(2f);

        Debug.Log("Step 1: Enabled annoying sound");
        
        yield return new WaitUntil(() => tablet.currentAttachment != null);

        Debug.Log("Step 2: Disable annoying sound and show hint 1");

        hintManager.SetHint(
            "Тебе нужно отсканировать одну книгу. Она находится во втором справа ряду, по левую сторону, на 3 сверху полке. У этой книги красный переплёт"
        );

        yield return new WaitUntil(() => book.IsGrabbed);

        Debug.Log("Step 3: Show hint 2");

        hintManager.SetHint(
            "Отлично, теперь принеси эту книгу на стол и положи её на чёрный сканер"
        );

        yield return new WaitUntil(() => scanner.HaveBook);

        Debug.Log("Step 4: Show hint 3");

        enterPressed = false;

        hintManager.SetHint(
            "Теперь нажми Enter на клавиатуре компьютера чтобы запустить сканер"
        );

        yield return new WaitUntil(() => enterPressed);

        Debug.Log("Step 5: Wait...");

        pcText.text = "Сканер:\n    Сканирование...";
        scannerLight.enabled = true;

        hintManager.SetHint("Осталось немного подождать");

        yield return new WaitForSeconds(4f);

        Debug.Log("Step 5: Weeee");
        scannerLight.enabled = false;
        pcText.text = "Сканер:\n    Сканирование завершено\n    Изображение сохранено!";

        hintManager.SetHint("Задача выполнена!");

        yield return new WaitForSeconds(3f);

        if (callback != null)
            callback();
    }


}
