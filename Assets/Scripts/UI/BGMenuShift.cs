using UnityEngine;

public class BGMenuShift : MonoBehaviour {

    [SerializeField]
    float amplitude = 200;

    float angle;
    RectTransform trans;
    Vector3 origin;

    void Start () {
        trans = GetComponent<RectTransform> ();
        origin = trans.position;
    }

    void Update () {
        trans.position = new Vector3 (origin.x, origin.y + Mathf.Sin (angle) * amplitude, 0);

        angle += Time.deltaTime * 0.2f;
    }
}
