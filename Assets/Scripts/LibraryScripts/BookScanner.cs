using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScanner : MonoBehaviour
{
    public Transform AttachPoint;

    [Range(0.01f, 2f)]
    public float TotalAnimationTime = 1f;

    public bool HaveBook { get; private set; }

    private Pose startPose;
    private ScannableBook currentBook;

    private float animTime = -1f;

    public void OnBookReleased(ScannableBook book)
    {
        if (currentBook != null)
            return;

        currentBook = book;

        animTime = TotalAnimationTime;
        startPose = new Pose(book.transform.position, book.transform.rotation);
        currentBook.SetGrabbable(false);
    }

    void Update()
    {
        if (animTime <= 0f)
            return;

        animTime -= Time.deltaTime;

        var startPos = startPose.position;
        var endPos = AttachPoint.position;

        var startRot = startPose.rotation;
        var endRot = AttachPoint.rotation;

        var fac = Mathf.Clamp01(animTime / TotalAnimationTime);
        currentBook.transform.position = Vector3.Lerp(endPos, startPos, fac);
        currentBook.transform.rotation = Quaternion.Slerp(endRot, startRot, fac);
        currentBook.OpenAnimator.SetFloat("factor", 1 - fac);

        if (animTime > 0)
            return;

        HaveBook = true;
    }

    void OnTriggerEnter(Collider other)
    {
        var book = GetBook(other);
        if (book == null)
            return;

        Debug.Log("Book!", other);
        book.currentScanner = this;
    }

    void OnTriggerExit(Collider other)
    {
        var book = GetBook(other);
        if (book == null)
            return;

        Debug.Log("no book ....", other);
        book.currentScanner = null;
    }

    private ScannableBook GetBook(Collider c)
    {
        var obj = c.attachedRigidbody?.gameObject;
        if (obj == null)
            return null;
        if (!obj.TryGetComponent(out ScannableBook book))
            return null;

        return book;
    }
}
