using UnityEngine;

namespace UI.Buttons
{
    public class OverBattleButton : AbstractionButton
    {
        [SerializeField] private OverBattle _overBattle;
        [SerializeField]private StageUI _stageUI;

        protected override void OnClick()
        {
            _overBattle.EscapeFromBattle();
            _stageUI.DefaultStage();
            gameObject.SetActive(false);
        }
    }
}
