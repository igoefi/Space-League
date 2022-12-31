using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _ammoText;
    [SerializeField]private Image _grenade;
    [SerializeField]private TextMeshProUGUI _resurcesText;

    [SerializeField]private Image _hpImage;
    [SerializeField]private Image _armorImage;


    [SerializeField]private Image _hpBoss;
    [SerializeField]private GameObject _bossBlock;
    [SerializeField]private TextMeshProUGUI _bossName;

    [SerializeField]private GameObject _inventoryBlock;
    public GameObject ButtonPrefab;
    [SerializeField]private List<Button> _buttons = new List<Button>();
    [SerializeField]private Sprite _defoultSprite;

    private void Start() {
        Inventory.Instance.UpdateUIDataEvent += UpdateUIResources;
        Inventory.Instance.DropItemEvent += UpdateItemsBlock;
        FindObjectOfType<Player>().UpdateUIDataEvent += UpdateUIPlayer;
        Enemy.BossUpdateUIEvent += UpdateUIBoss;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B)){
            _inventoryBlock.SetActive(!_inventoryBlock.activeSelf);
            UpdateItemsBlock();
        }
    }
    private void UpdateUIResources(object sender, EventDataInventory dataResources){
        _ammoText.text = $"{dataResources.CurrentAmmoInWeapon} / {dataResources.Resources[ResourcesType.Ammo]}";
        Debug.Log(dataResources.CurrentAmmoInWeapon);
        _resurcesText.text = $"{dataResources.Resources[ResourcesType.Wood]} / {dataResources.Resources[ResourcesType.Iron]} / {dataResources.Resources[ResourcesType.Coin]}";

        _grenade.fillAmount = (float)dataResources.Grenade / dataResources.MaxGrenade;
        Debug.Log(dataResources.MaxGrenade + " " + dataResources.Grenade / dataResources.MaxGrenade);
    }

    private void UpdateUIPlayer(object sender, EventDataPlayer dataPlayer){
        _hpImage.fillAmount = dataPlayer.HP;
        _armorImage.fillAmount = dataPlayer.Armor;
    }

    private void UpdateUIBoss(object sender, EventDataBoss dataBoss){
        _bossBlock.SetActive(dataBoss.IsAlive);
        _hpBoss.fillAmount = dataBoss.HP;
        _bossName.text = dataBoss.Name;
    }

    private void UpdateItemsBlock(){
        for(int i = 0; i < _buttons.Count; i++){
            if(i >= Inventory.Instance.Items.Count){
                _buttons[i].image.sprite = _defoultSprite;
            }
            else{
                _buttons[i].image.sprite = Inventory.Instance.Items[i].ItemSprite;
            }
        }
    }
}
