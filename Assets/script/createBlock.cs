using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class createBlock : MonoBehaviour
{
    public GameObject m_object;
    public GameObject BuildObject;
    Vector3 touchWorldPosition;
    // Start is called before the first frame update
    void Start()
    {
        //touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);
        m_object = Instantiate(m_object, touchWorldPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    gameObject.GetComponent<Gamesystem>().create(BuildObject,m_object.transform.position);
        //}
        Vector3 touchScreenPosition = Input.mousePosition;

        touchScreenPosition.x = Mathf.Clamp(touchScreenPosition.x, 0.0f, Screen.width);
        touchScreenPosition.y = Mathf.Clamp(touchScreenPosition.y, 0.0f, Screen.height);

        // 10.0fに深い意味は無い。画面に表示したいので適当な値を入れてカメラから離そうとしているだけ.
        touchScreenPosition.z = 10.0f;

        Camera gameCamera = Camera.main;
        touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);

        m_object.transform.position = touchWorldPosition;
    }
}
