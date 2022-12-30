using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _ammoText;

    [SerializeField]private TextMeshProUGUI _resurcesText;

    [SerializeField]private Image _hpImage;
    [SerializeField]private Image _armorImage;


    [SerializeField]private Image _hpBoss;
    [SerializeField]private GameObject _bossBlock;
    [SerializeField]private TextMeshProUGUI _bossName;

    [SerializeField]private GameObject _itemBlock;
    public GameObject ButtonPrefab;
    private List<Button> _buttons = new List<Button>();

    private void Start() {
        Inventory.Instance.UpdateUIDataEvent += UpdateUIResources;
        FindObjectOfType<Player>().UpdateUIDataEvent += UpdateUIPlayer;
        Enemy.BossUpdateUIEvent += UpdateUIBoss;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B)){
            _itemBlock.SetActive(!_itemBlock.activeSelf);
            UpdateItemsBlock();
        }
    }
    private void UpdateUIResources(object sender, EventDataInventory dataResources){
        _ammoText.text = $"{dataResources.Resources[ResourcesType.Ammo]}/ 100";

        _resurcesText.text = $"{dataResources.Resources[ResourcesType.Wood]} / {dataResources.Resources[ResourcesType.Iron]} / {dataResources.Resources[ResourcesType.Coin]}";
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
        if(Inventory.Instance.Items.Count != _buttons.Count){
            for(int i = _buttons.Count; i < Mathf.Abs(_buttons.Count - Inventory.Instance.Items.Count); i++){
                Button newButton = Instantiate(ButtonPrefab, transform.position, Quaternion.identity).GetComponent<Button>();
                newButton.transform.parent = _itemBlock.transform;
                newButton.image.sprite = Inventory.Instance.Items[i].ItemSprite;
                
                _buttons.Add(newButton);
            }
        }
    }
}
