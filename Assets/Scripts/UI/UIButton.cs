using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool Interactable => interactable;

    public UnityEvent OnClickDown;
    public UnityEvent OnClick;

    public UnityAction PointerClick;
    public UnityAction<UIButton> PointerEnter;

    [SerializeField] private Animator m_Animator;
    [SerializeField] private string m_EnterBool;
    [SerializeField] private string m_NormalBool;


    [SerializeField] protected Color m_SelectColor; //F1F1F1
    [SerializeField] protected Color m_DefaultColor; //939393
    [SerializeField] private Color m_DisabledColor;
    [SerializeField] private Image m_ButtonImage;

    private bool interactable;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Interactable == false) return;

        PointerEnter?.Invoke(this);

        if (m_ButtonImage != null)
        {
            m_ButtonImage.color = m_SelectColor;
        }

        if (m_Animator == null) return;
        m_Animator.SetBool(m_EnterBool, true);
        m_Animator.SetBool(m_NormalBool, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable == false) return;
        if (m_ButtonImage != null)
        {
            m_ButtonImage.color = m_DefaultColor;
        }
        if (m_Animator == null) return;
        m_Animator.SetBool(m_NormalBool, true);
        m_Animator.SetBool(m_EnterBool, false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Interactable == false) return;
        OnClickDown?.Invoke();
        PointerClick?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Interactable == false) return;
        OnClick?.Invoke();
    }

    public void Disabled()
    {
        interactable = false;
        m_ButtonImage.color = m_DisabledColor;
    }
    public void Enabled()
    {
        interactable = true;
        m_ButtonImage.color = m_DefaultColor;
    }
}
