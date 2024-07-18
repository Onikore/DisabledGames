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
        pcText.text = "������:\n    ��������...";
        scannerLight.enabled = false;

        yield return new WaitForSeconds(2f);

        Debug.Log("Step 1: Enabled annoying sound");
        
        yield return new WaitUntil(() => tablet.currentAttachment != null);

        Debug.Log("Step 2: Disable annoying sound and show hint 1");

        hintManager.SetHint(
            "���� ����� ������������� ���� �����. ��� ��������� �� ������ ������ ����, �� ����� �������, �� 3 ������ �����. � ���� ����� ������� �������"
        );

        yield return new WaitUntil(() => book.IsGrabbed);

        Debug.Log("Step 3: Show hint 2");

        hintManager.SetHint(
            "�������, ������ ������� ��� ����� �� ���� � ������ � �� ������ ������"
        );

        yield return new WaitUntil(() => scanner.HaveBook);

        Debug.Log("Step 4: Show hint 3");

        enterPressed = false;

        hintManager.SetHint(
            "������ ����� Enter �� ���������� ���������� ����� ��������� ������"
        );

        yield return new WaitUntil(() => enterPressed);

        Debug.Log("Step 5: Wait...");

        pcText.text = "������:\n    ������������...";
        scannerLight.enabled = true;

        hintManager.SetHint("�������� ������� ���������");

        yield return new WaitForSeconds(4f);

        Debug.Log("Step 5: Weeee");
        scannerLight.enabled = false;
        pcText.text = "������:\n    ������������ ���������\n    ����������� ���������!";

        hintManager.SetHint("������ ���������!");

        yield return new WaitForSeconds(3f);

        if (callback != null)
            callback();
    }


}
