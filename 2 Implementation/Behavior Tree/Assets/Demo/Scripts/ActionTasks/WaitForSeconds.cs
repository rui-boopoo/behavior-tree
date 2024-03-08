using Boopoo.BehaviorTrees;
using UnityEngine;

public class WaitForSeconds : Task
{
    private float _timeInSecond;
    private float _timer;

    private bool _pass;

    public WaitForSeconds(float timeInSecond)
    {
        _timeInSecond = timeInSecond;
    }

    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        if (_pass)
        {
            EndAction();
            return;
        }

        _timer += Time.deltaTime;
        if (!(_timer >= _timeInSecond)) return;

        _pass = true;
        EndAction();
    }

    private void Reset()
    {
        _pass = false;
        _timer = 0;
    }
}