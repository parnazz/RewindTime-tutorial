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

    private bool _isSwitchedA = false;
    private bool _isSwitchedB = false;
    private bool _isSwitchedRewind = false;

    // Start is called before the first frame update
    void Start()
    {
        _timeBody = GameObject.Find("Player").GetComponent<TimeBody>();
    }

    // Update is called once per frame
    void Update()
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

        MoveCube();

        RewindCheck();
    }

    // Метод неодходим для того, чтобы не происходило регистрации 
    // прохождения точки А/Б при перемотке времени.
    IEnumerator SwitchRewindRoutine()
    {
        yield return new WaitUntil(() => !_timeBody.isRewinding);
        _isSwitchedA = false;
        _isSwitchedB = false;
        _isSwitchedRewind = false;
    }

    // При перемотке времени фиксируем факт прохождения через точку А/Б
    // и меняем значение переменной _caseSwitch на противоположное.
    // Так получаем логичную петлю во времени.
    private void RewindCheck()
    {
        // Небольшой оффсет при проверках нужен, так как почему-то при 
        // оригинальном значении x не регистрируется факт прохождения
        // через точку А/Б при перемотке времени.
        if (_timeBody.isRewinding && transform.position.x >= (_pointA.position.x - 0.05f))
        {
            if (!_isSwitchedRewind)
            {
                _isSwitchedRewind = true;
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                StartCoroutine(SwitchRewindRoutine());
            }
        }
        else if (_timeBody.isRewinding && transform.position.x <= (_pointB.position.x + 0.05f))
        {
            if (!_isSwitchedRewind)
            {
                _isSwitchedRewind = true;
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                StartCoroutine(SwitchRewindRoutine());
            }
        }
    }

    private void MoveCube()
    {
        if (transform.position.x >= _pointA.position.x)
        {
            if (!_isSwitchedA)
            {
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                _isSwitchedA = true;
                _isSwitchedB = false;
            }
        }
        else if (transform.position.x <= _pointB.position.x)
        {
            if (!_isSwitchedB)
            {
                _caseSwitch -= 1;
                _caseSwitch = Mathf.Abs(_caseSwitch);
                _isSwitchedB = true;
                _isSwitchedA = false;
            }
        }
    }
}
