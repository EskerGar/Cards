using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image shine;
    public Transform DefaultParent { get; set; }
    public Transform DefaultTempCardParent { get; set; }
    
    private Camera _camera;
    private Vector3 _offset;
    private CanvasGroup _canvasGroup;
    private Transform _tempCardTransform;
    private Canvas _canvas;
    private CardBehaviour _cardBehaviour;

    private void Awake()
    {
        _camera = Camera.main;
        _canvasGroup = GetComponent<CanvasGroup>();
        _cardBehaviour = GetComponent<CardBehaviour>();
        shine.enabled = false;
    }

    public void MoveToField(Vector3 fieldPosition)
    {
        transform.SetParent(_canvas.transform);
        transform.DOMove(fieldPosition, .5f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
        DefaultParent = DefaultTempCardParent = transform.parent;
        _tempCardTransform.SetParent(DefaultParent);
        _tempCardTransform.SetSiblingIndex(transform.GetSiblingIndex());
        transform.SetParent(DefaultParent.parent);
        _canvasGroup.blocksRaycasts = false;
        shine.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var newPosition = _camera.ScreenToWorldPoint(eventData.position);
        newPosition.z = 0;
        transform.position = newPosition + _offset;
        if(_tempCardTransform.parent != DefaultTempCardParent)
            _tempCardTransform.SetParent(DefaultTempCardParent);
        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DefaultParent);
        _canvasGroup.blocksRaycasts = true;
        var index = _tempCardTransform.GetSiblingIndex();
        transform.SetSiblingIndex(index);
        CardPool.CheckCardFromHand(_cardBehaviour, index);
        _tempCardTransform.SetParent(_canvas.transform);
        _tempCardTransform.localPosition = new Vector3(459f, 0f);
        shine.enabled = false;
    }

    public void Initialize(Transform tempCard, Canvas canvas)
    {
        _tempCardTransform = tempCard;
        _canvas = canvas;
    }

    private void CheckPosition()
    {
        var newIndex = DefaultTempCardParent.childCount;
        for (int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (!(transform.position.x < DefaultTempCardParent.GetChild(i).position.x)) continue;
            newIndex = i;
            
            if (_tempCardTransform.GetSiblingIndex() < newIndex)
                newIndex--;
            break;
        }
        _tempCardTransform.SetSiblingIndex(newIndex);
    }
}
