using UnityEngine;

public class PageTransition : MonoBehaviour
{
    public enum PageDirection
    {
        NONE, LEFT, RIGHT
    }

    [Header("REF")]
    [SerializeField] protected GameObject tabsPages;

    [Header("STATE")]
    [SerializeField] protected PageDirection pageDirection;
    //[SerializeField] protected int fadeSelect;
    [SerializeField] protected int selectedPage;
    [SerializeField] protected int previousPage;

    [Header("FIELDS")]
    [SerializeField] protected float length;
    [SerializeField] protected float lerpValue;

    private Vector3   _newPos;
    private Vector3   _oldPos;
    private Transform _tabsTransform;

    private void Start()
    {
        _newPos = Vector3.zero;
        _oldPos = Vector3.zero;
        _tabsTransform = tabsPages.transform;
    }

    private void Update()
    {
        //if ( fadeSelect == 0 )
        //{

        //}
        //else if ( fadeSelect == 1 )
        //{
        //    if ( lerpValue >= 1 )
        //    {
        //        fadeSelect = 0;
        //        lerpValue = 0.0f;
        //    }
        //    else
        //    {
        //        //MoveLeft
        //        lerpValue += 5f * Time.deltaTime;
        //        tabsPages.transform.position = Vector3.Lerp( tabsPages.transform.position, targetLeft.position, lerpValue );
        //    }
        //}
        //else if ( fadeSelect == 2 )
        //{
        //    if ( lerpValue >= 1 )
        //    {
        //        fadeSelect = 0;
        //        lerpValue = 0.0f;
        //    }
        //    else
        //    {
        //        lerpValue += 5f * Time.deltaTime;
        //        //MoveRight
        //        tabsPages.transform.position = Vector3.Lerp( tabsPages.transform.position, targetRight.position, lerpValue );

        //    }
        //}

        UpdatePage();
    }
    
    private void UpdatePage()
    {
        //pageDirection = ( PageDirection )fadeSelect;

        switch ( pageDirection )
        {
            case PageDirection.NONE:
                break;
            case PageDirection.LEFT:
                MoveToPage( -selectedPage );
                break;
            case PageDirection.RIGHT:
                MoveToPage( selectedPage );
                break;
            default:
                break;
        }
    }

    private void MoveToPage( int page )
    {
        if ( lerpValue >= 1f )
        {
            lerpValue     = 0f;
            //fadeSelect    = 0;
            pageDirection = PageDirection.NONE;
        }
        else
        {
            lerpValue += Time.deltaTime / length;
            _newPos.Set( _oldPos.x * page, _oldPos.y, _oldPos.z );
            _tabsTransform.position = Vector3.Lerp( _oldPos, _newPos, lerpValue );
        }
    }

    public void TransitionPage()
    {
        //if ( selectedPage > previousPage )
        //{
        //    fadeSelect = 1;
        //}
        //else if ( selectedPage > previousPage )
        //{
        //    fadeSelect = 2;
        //}

        _oldPos = _tabsTransform.position;
        //fadeSelect = selectedPage > previousPage ? 1 : 2;
        pageDirection = selectedPage > previousPage ? PageDirection.LEFT : PageDirection.RIGHT;
    }
}
