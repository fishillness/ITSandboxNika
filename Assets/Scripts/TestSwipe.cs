using UnityEngine;
using UnityEngine.EventSystems;

public class TestSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject _square;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _distanceMove;
    private Vector2 _direction;
    private Vector2 _pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _pos = _square.transform.position;
        SwipeDetected(eventData.delta.x, eventData.delta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void SwipeDetected(float deltaX, float deltaY) 
    {
        if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            _direction = deltaX > 0 ? Vector2.right : Vector2.left;
        else
            _direction = deltaY > 0 ? Vector2.up : Vector2.down;
    }

    private void Move()
    {
        var targetPosition = _pos + _direction * _distanceMove;
        _square.transform.position = Vector2.MoveTowards(_square.transform.position, targetPosition, _speedMovement * Time.deltaTime);
    }
}
