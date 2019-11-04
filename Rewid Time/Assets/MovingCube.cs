using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;
    private int _caseSwitch = 0;
    private TimeBody _timeBody;

    private bool _isSwitched = false;
    private bool _isSwitchedRewind = false;

    // Start is called before the first frame update
    void Start()
    {
        _timeBody = GameObject.Find("Player").GetComponent<TimeBody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_caseSwitch)
        {
            case 0:
                transform.Translate(Vector3.right * 10f * Time.fixedDeltaTime);
                break;
            case 1:
                transform.Translate(Vector3.left * 10f * Time.fixedDeltaTime);
                break;
        }
       
        if (transform.position.x >= _pointA.position.x)
        {
            if (!_isSwitched)
            {
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                _isSwitched = true;
            }
        }
        else if (transform.position.x <= _pointB.position.x)
        {
            if (_isSwitched)
            {
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                _isSwitched = false;
            }
        }

        if (_timeBody.isRewinding && transform.position.x >= (_pointA.position.x - 0.1f))
        {
            if (!_isSwitchedRewind)
            {
                _isSwitchedRewind = true;
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                StartCoroutine(Routine());
            }
        }
        else if (_timeBody.isRewinding && transform.position.x <= (_pointB.position.x + 0.1f))
        {
            if (!_isSwitchedRewind)
            {
                _isSwitchedRewind = true;
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                StartCoroutine(Routine());
            }
        }
    }

    IEnumerator Routine()
    {
        yield return new WaitUntil(() => !_timeBody.isRewinding);
        _isSwitched = false;
        _isSwitchedRewind = false;
    }
}
