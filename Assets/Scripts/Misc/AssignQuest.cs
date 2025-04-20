using Naninovel.Commands;
using Naninovel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CommandAlias("quest")]
public class AssignQuest : Command
{
    [ParameterAlias("reset")]
    public BooleanParameter ResetState = true;

    [RequiredParameter]
    public StringParameter Name;

    public override UniTask Execute(AsyncToken token = default)
    {
        if (Assigned(Name)) GameManager.Instance.questManager.StartQuest(Name);
        else Debug.Log("Name is invalid");

        return UniTask.CompletedTask;
    }
}
