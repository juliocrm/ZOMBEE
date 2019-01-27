using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Image staminaBarFill;
    [SerializeField]
    private Text _weaponDurability;
    [SerializeField]
    private Image _weaponIcon;
    [SerializeField]
    private Text _weaponStamina;

    [SerializeField]
    private Image _bombTrapIcon;
    [SerializeField]
    private Image _stickyTrapIcon;
    [SerializeField]
    private Image _weakTrapIcon;
    [SerializeField]
    private Image _turnTrapIcon;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Text _weaponDurabilityMessageLabel;
    [SerializeField]
    private Text _weaponAvailableMessageLabel;

    private Stamina _playerStamina;
    private AttackComp _playerAttackComp;

    #region Set Icons

    public void SetStamina(float stamina) {
        staminaBarFill.fillAmount = stamina;
    }

    public void SetWeapon(WeaponDef weapon) {
        if (weapon.durability <= 0)
        {
            var color = _weaponIcon.color;
            color.a = 0.5f;
            _weaponIcon.color = color;
        }
        _weaponDurability.text = weapon.durability.ToString();
    }

    public void SetBombTrap(bool enable) {
        var color = _bombTrapIcon.color;

        color.a = enable ? 1 : 0.5f;
        _bombTrapIcon.color = color;
    }

    public void SetStickTrap(bool enable) {
        var color = _stickyTrapIcon.color;

        color.a = enable ? 1 : 0.5f;
        _stickyTrapIcon.color = color;
    }

    public void SetTurnTrap(bool enable) {
        var color = _turnTrapIcon.color;

        color.a = enable ? 1 : 0.5f;
        _turnTrapIcon.color = color;
    }
    public void SetWeak(bool enable) {
        var color = _weakTrapIcon.color;

        color.a = enable ? 1 : 0.5f;
        _weakTrapIcon.color = color;
    }
    #endregion

    private void Start()
    {
        var playerCtrl = transform.root.GetComponentInChildren<PlayerController>();
        _playerStamina = playerCtrl.gameObject.GetComponent<Stamina>();
        _playerAttackComp = playerCtrl.gameObject.GetComponent<AttackComp>();
        SetBombTrap(true);
        SetStickTrap(true);
        SetTurnTrap(true);
        SetWeak(true);
    }


    private void Update()
    {
        SetStamina(_playerStamina.StaminaAmount / Stamina.maxStamina);
        SetWeapon(_playerAttackComp.GetWeaponDef());
    }

    public void WeaponAvailableMessage(bool available, WeaponDef weaponDef)
    {
        animator.SetTrigger("WeaponMessage");
        if (available)
            _weaponAvailableMessageLabel.text = "Weapon picked up";
        else
            _weaponAvailableMessageLabel.text = "Weapon broken";

        _weaponDurabilityMessageLabel.text = weaponDef.durability.ToString();

    }

}
