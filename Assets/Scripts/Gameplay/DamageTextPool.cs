using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageTextPool : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private int poolSize = 10;
    private Queue<GameObject> pool;

    void Awake()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(damageTextPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void ShowDamage(Vector3 worldPosition, int amount)
    {
        if (pool.Count == 0) return;

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);

        // Posicionar en el mundo
        obj.transform.position = worldPosition;

        TMP_Text text = obj.GetComponent<TMP_Text>();
        CanvasGroup group = obj.GetComponent<CanvasGroup>();

        text.text = "-" + amount.ToString();
        group.alpha = 1f;

        // Animación simple con coroutine
        StartCoroutine(AnimateDamageText(obj, group));
    }

    private IEnumerator AnimateDamageText(GameObject obj, CanvasGroup group)
    {
        float duration = 0.75f;
        float elapsed = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + Vector3.up * 0.75f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            group.alpha = Mathf.Lerp(1f, 0f, t);

            yield return null;
        }

        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
