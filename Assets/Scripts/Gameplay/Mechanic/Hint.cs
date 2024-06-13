using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintType
{
    public enum hintTypes { Arrow, Nav, Click, Around }
}

public class Hint : MonoBehaviour
{
    [SerializeField] HintType.hintTypes type;
    [SerializeField] public CanvasGroup hintCanvasGroup;
    GameObject ParentObject;
    GameObject ParentObjectTarget;

    [SerializeField] GameObject HintTarget;

    public float speed = 4.0f;
    public bool movingToB = true;

    float targetScale = 1.2f;

    void Start()
    {
        ParentObject = gameObject.transform.parent.gameObject;
        if (type == HintType.hintTypes.Nav)
        {
            DragAndDrop dnd = ParentObject.GetComponent<DragAndDrop>();
            ParentObjectTarget = dnd.itemTarget;

            StartCoroutine(MoveBetweenPoints());
        }
        else if (type == HintType.hintTypes.Arrow || type == HintType.hintTypes.Click)
        {
            StartCoroutine(ScaleLoop());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            // Tentukan target berdasarkan arah saat ini
            Transform target = movingToB ? HintTarget != null ? HintTarget.transform : ParentObjectTarget.transform : ParentObject.transform;

            // Mulai bergerak menuju target
            while (Vector3.Distance(transform.position, target.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                yield return null;
            }

            // Tunggu di target selama satu detik sebelum bergerak kembali
            yield return new WaitForSeconds(0.5f);

            // Ganti arah
            movingToB = !movingToB;
        }
    }

    IEnumerator ScaleLoop()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(targetScale, targetScale, targetScale);

        while (true)
        {
            // Scale up
            yield return StartCoroutine(ScaleOverTime(destinationScale, speed));
            // Scale down
            yield return StartCoroutine(ScaleOverTime(originalScale, speed));
        }
    }

    IEnumerator ScaleOverTime(Vector3 target, float duration)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target;
    }
}
