using System.Collections;
using UnityEngine;

public class Variable
{
    public int variable;
    public int min = 0;
    public int max = 100;

    public Variable(int var, int _min, int _max)
    {
        variable = Mathf.Clamp(var, _min, _max);
        min = _min;
        max = _max;
    }

    public Variable(int var)
    {
        variable = var;
        min = 0;
        max = 100;
    }
}
