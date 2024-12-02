using System.Collections.Generic;
using UnityEngine;
using B83.LogicExpressionParser;

public class VariableManager : MonoBehaviour
{
    public Dictionary<string, Variable> variableList = new Dictionary<string, Variable>();
    public Parser parser = new Parser();

    public Variable GetVariableValue(string variableName)
    {
        return variableList[variableName];
    }

    public bool CompareVariable(string variableName, string expression)
    {
        //Check if the variable exists or not
        bool success = variableList.TryGetValue(variableName, out var val);

        if (success)
        {
            LogicExpression exp = parser.Parse(expression);
            return exp.GetResult();

        }
        return false;

    }

    public void NewVariable(string name, int value)
    {
        variableList.Add(name, new Variable(value));
        parser.ExpressionContext[name].Set(variableList[name].variable);
    }

    private void Start()
    {
        NewVariable("merchantTalk", 0);
    }
}
