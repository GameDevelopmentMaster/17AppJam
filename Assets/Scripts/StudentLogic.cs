using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentLogic : MonoBehaviour
{

    public float time;
    public float seed;
    public List<Sprite> sprites;

    private Image spr;
    private RectTransform rect;
    private Vector3 origin;
    private Vector2 boundY = new Vector2(0, 282), originScale;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        spr = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        origin = rect.localPosition;
        originScale = rect.sizeDelta;


        seed = Random.Range(0f, 1f) * 42f + 21f;
        //spr.sprite = sprites[(int)Random.value * sprites.Count];

        // play anim
        spr.sprite = sprites[(int)Random.value * sprites.Count];
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 0.5f;

        //rect.localPosition = origin + new Vector3(
        //    Mathf.Sin(time * 1.2f + seed) * 42.2f + seed + Mathf.Cos(time + 666f) * 20.2f,
        //    Mathf.Cos(time * 1.2f - seed + Mathf.Sin(time * 0.1f) * 2f + seed + 0.4232682423786423642387469f) * 160.2f + Mathf.Sin(time + 666f) * 84.1f,
        //    0f
        //    );

        rect.localPosition = origin + new Vector3(0f, Mathf.Clamp(rect.localPosition.y, boundY.x, boundY.y), 0f)
             + new Vector3(
            0f,
            Mathf.Abs(Mathf.Cos(time * 16f + seed) * 20f),
            0f
            );

        float delta = (boundY.y - boundY.x);
        float scale = Mathf.Lerp(0.6f, 1.2f, Mathf.Abs((rect.localPosition.y - boundY.y)) / delta);
        rect.sizeDelta = originScale * new Vector2(
            scale,
            scale
            );
    }
}
