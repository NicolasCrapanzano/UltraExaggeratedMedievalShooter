using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _hitMarker;
    [SerializeField]
    private Sprite[] _ClickAnim;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private bool _animON;
    private float _timer;
    public static int selectedCursor;
    private MainMenu _mainMenu;
    void Start()
    {
        _mainMenu = GameObject.FindObjectOfType<MainMenu>();
        Cursor.visible = false;
        _sprite = GetComponent<SpriteRenderer>();

        if(this.gameObject.tag == "Cursor")
        {
            selectedCursor = _mainMenu.data.actualCursor;
            _sprite.sprite = _ClickAnim[selectedCursor];
        }
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _cursorPos;
        if (this.gameObject.tag == "MenuCursor")
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetBool("Click",true);
                _timer = Time.time + 0.02f;
            }
            if (_timer <= Time.time)
            {
                _animator.SetBool("Click", false);
            }
        }
    }
    public void RutineInterceptor()
    {
        StartCoroutine("HitMarker");
    }
    public IEnumerator HitMarker()
    {
        //Debug.Log("test");
        _hitMarker.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _hitMarker.SetActive(false);
    }
}
