using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] ParticleSystem[] hammerBuyPart;
    [SerializeField] ParticleSystem[] knifeBuyPart;
    [SerializeField] ParticleSystem[] grillBuyPart;
    [SerializeField] ParticleSystem[] heartBuyPart;
    [SerializeField] ParticleSystem[] timerBuyPart;


    public GameObject[] hammerInfo;
    public GameObject[] knifeInfo;
    public GameObject[] grillInfo;

    public GameObject pos_Heart;
    public GameObject pos_Timer;
    public GameObject pos_Hammer;
    public GameObject pos_Knife;
    public GameObject pos_Grill;
    public GameObject exit;
    GameObject[] heartUI = new GameObject[5];
    GameObject[] timerUI = new GameObject[5];
    GameObject[] hammerUI = new GameObject[5];
    GameObject[] knifeUI = new GameObject[5];
    GameObject[] grillUI = new GameObject[5];

    [SerializeField] GameObject preText;
    [SerializeField] GameObject nextText;

    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject scores;

    [SerializeField] GameObject heartOff;
    [SerializeField] GameObject timerOFf;
    [SerializeField] GameObject heartHit;
    [SerializeField] GameObject timerHit;

    TextMeshProUGUI[] hammerInfoUI = new TextMeshProUGUI[3];
    TextMeshProUGUI[] knifeInfoUI = new TextMeshProUGUI[3];
    TextMeshProUGUI[] grillInfoUI = new TextMeshProUGUI[3];
    TextMeshProUGUI heartBtnText;
    TextMeshProUGUI timerBtnText;
    TextMeshProUGUI hammerBtnText;
    TextMeshProUGUI knifeBtnText;
    TextMeshProUGUI grillBtnText;
    TextMeshProUGUI startBtnText;

    GameObject maxLevel;

    ParticleSystem heartBtnPart;
    ParticleSystem timerBtnPart;
    ParticleSystem hammerBtnPart;
    ParticleSystem knifeBtnPart;
    ParticleSystem grillBtnPart;
    ParticleSystem startBtnPart;

    GameObject buyUI;

    //버튼
    Button btnHeart;
    Button btntimer; 
    [SerializeField] Button btnHammer;
    [SerializeField] Button btnKnife;
    [SerializeField] Button btnGrill;
    GameObject btnStart;

    [SerializeField] GameObject preBtn;
    [SerializeField] GameObject nextBtn;

    // Start is called before the first frame update
    public void SetUI()
    {
        for (int i = 0; i < heartUI.Length; i++)//개수만큼 넣어주고 비활성화 시킨다
        {
            heartUI[i] = pos_Heart.transform.GetChild(i).gameObject;//info - buy - nomoney - itemstock - money
            timerUI[i] = pos_Timer.transform.GetChild(i).gameObject;//info - buy - nomoney - itemstock - money
            heartUI[i].SetActive(false);
            timerUI[i].SetActive(false);
        }
        for (int i = 0; i < hammerUI.Length; i++)//개수만큼 넣어주고 비활성화 시킨다
        {
            hammerUI[i] = pos_Hammer.transform.GetChild(i).gameObject;//info - upgrade - nomoney - maxlevel - money
            knifeUI[i] = pos_Knife.transform.GetChild(i).gameObject;//info - upgrade - nomoney - maxlevel - money
            grillUI[i] = pos_Grill.transform.GetChild(i).gameObject;//info - upgrade - nomoney - maxlevel - money
            if (i == 0)
            {
                hammerInfoUI = hammerUI[0].GetComponentsInChildren<TextMeshProUGUI>();//1-2-3
                knifeInfoUI = knifeUI[0].GetComponentsInChildren<TextMeshProUGUI>();//1-2-3
                grillInfoUI = grillUI[0].GetComponentsInChildren<TextMeshProUGUI>();//1-2-3
            }
            hammerUI[i].SetActive(false);
            knifeUI[i].SetActive(false);
            grillUI[i].SetActive(false);
        }
    }
    public IEnumerator BuyItemParticle(string name)
    {
        switch (name)
        {
            case "heart":
                heartBuyPart[0].Play();
                SoundManager.soundMN.Charging();
                yield return new WaitForSeconds(1.0f);
                for (int i = 1; i < heartBuyPart.Length; i++)
                {
                    heartBuyPart[i].Play();
                }
                ItemManager.instance.AddItemName(name);
                SoundManager.soundMN.EnhanceSound();
                BuyItemUI(name);
                break;
            case "timer":
                timerBuyPart[0].Play();
                SoundManager.soundMN.Charging();
                yield return new WaitForSeconds(1.0f);
                for (int i = 1; i < timerBuyPart.Length; i++)
                {
                    timerBuyPart[i].Play();
                }
                ItemManager.instance.AddItemName(name);
                SoundManager.soundMN.EnhanceSound();
                BuyItemUI(name);
                break;
        }
    }
    public IEnumerator BuyEquipParticle(string name)
    {
        switch (name)
        {
            case "Hammer":
                hammerBuyPart[0].Play();
                SoundManager.soundMN.Charging();
                yield return new WaitForSeconds(1.0f);
                for (int i = 1; i < hammerBuyPart.Length; i++)
                {
                    hammerBuyPart[i].Play();
                }
                SoundManager.soundMN.EnhanceSound();
                EquipManager.instance.SetEquip();
                UpgradeEquipUI(name);
                break;
            case "Knife":
                knifeBuyPart[0].Play();
                SoundManager.soundMN.Charging();
                yield return new WaitForSeconds(1.0f);
                for (int i = 1; i < knifeBuyPart.Length; i++)
                {
                    knifeBuyPart[i].Play();
                }
                SoundManager.soundMN.EnhanceSound();
                EquipManager.instance.SetEquip();
                UpgradeEquipUI(name);
                break;
            case "Grill":
                grillBuyPart[0].Play(); 
                SoundManager.soundMN.Charging();
                yield return new WaitForSeconds(1.0f);
                for (int i = 1; i < grillBuyPart.Length; i++)
                {
                    grillBuyPart[i].Play();
                }
                SoundManager.soundMN.EnhanceSound();
                EquipManager.instance.SetEquip();
                UpgradeEquipUI(name);
                break;
        }
    }
    void Start()
    {
        for (int i = 0; i < hammerBuyPart.Length; i++)
        {
            hammerBuyPart[i].Stop();
            knifeBuyPart[i].Stop();
            grillBuyPart[i].Stop();
            heartBuyPart[i].Stop();
            timerBuyPart[i].Stop();
        }

        SetUI();
        /*preBtn = GameObject.Find("Left_Btn");
        nextBtn = GameObject.Find("Right_Btn");*/

        btnHeart = GameObject.Find("Buy_Heart_Btn").GetComponent<Button>();
        btntimer = GameObject.Find("Buy_Timer_Btn").GetComponent<Button>();
        //StartBtn = GameObject.Find("Start_Btn").GetComponent<Button>();
        btnStart = GameObject.Find("Start_Btn");

        heartBtnPart = btnHeart.GetComponentInChildren<ParticleSystem>();
        timerBtnPart = btntimer.GetComponentInChildren<ParticleSystem>();
        hammerBtnPart = btnHammer.GetComponentInChildren<ParticleSystem>();
        knifeBtnPart = btnKnife.GetComponentInChildren<ParticleSystem>();
        grillBtnPart = btnGrill.GetComponentInChildren<ParticleSystem>();
        startBtnPart = btnStart.GetComponentInChildren<ParticleSystem>();

        hammerBtnText = btnHammer.GetComponentInChildren<TextMeshProUGUI>();
        knifeBtnText = btnKnife.GetComponentInChildren<TextMeshProUGUI>();
        grillBtnText = btnGrill.GetComponentInChildren<TextMeshProUGUI>();

        heartBtnText = btnHeart.GetComponentInChildren<TextMeshProUGUI>();
        timerBtnText = btntimer.GetComponentInChildren<TextMeshProUGUI>();
        startBtnText = btnStart.GetComponentInChildren<TextMeshProUGUI>();
        preText.SetActive(false);
        nextText.SetActive(false);
    }
    public void StageBtnTextOff()
    {
        preText.SetActive(false);
        nextText.SetActive(false);
    }
    public void StageBtnText(string name)
    {
        switch (name)
        {
            case "Left_Btn":
                print("left진입");
                switch (StageManager.instance.MyStage)
                {
                    case 2:
                        preText.SetActive(true);
                        preText.GetComponent<TextMeshProUGUI>().text = "Tutorial";
                        break;
                    case 3:
                        preText.SetActive(true);
                        preText.GetComponent<TextMeshProUGUI>().text = "Easy";
                        break;
                    default:
                        break;
                }
                break;
            case "Right_Btn":
                print("right진입");
                switch (StageManager.instance.MyStage)
                {
                    case 1:
                        nextText.SetActive(true);
                        nextText.GetComponent<TextMeshProUGUI>().text = "Easy";
                        break;
                    case 2:
                        nextText.SetActive(true);
                        nextText.GetComponent<TextMeshProUGUI>().text = "Hard";
                        break;
                    default:
                        break;
                }
                break;
            
        }
    }
    public void SetStageBtn(int stage)
    {
        switch (stage)
        {
            case 1:
                preBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
                scoreText.GetComponent<TextMeshProUGUI>().text = "화면가이드";
                scores.SetActive(false);
                break;
            case 3:
                nextBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
                break;
            default:
                preBtn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                nextBtn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                scoreText.GetComponent<TextMeshProUGUI>().text = "SCORE";
                scores.SetActive(true);
                break;
        }
    }
    public void MaxLevelInfo(string name)//장비관련
    {
        switch (name)
        {
            case "Hammer":
                hammerUI[3].SetActive(true);
                Invoke("MaxLevelMsgOff", 2.0f);
                break;
            case "Knife":
                knifeUI[3].SetActive(true);
                Invoke("MaxLevelMsgOff", 2.0f);
                break;
            case "Grill":
                grillUI[3].SetActive(true);
                Invoke("MaxLevelMsgOff", 2.0f);
                break;
            default:
                break;
        }
    }
   public void NoneHitItem(string name)
    {
        switch (name)
        {
            case "Heart":
                heartOff.GetComponent<MeshRenderer>().material.color = new Color(1, 0.5f, 0.5f, 0.3f);
                break;
            case "Timer":
                timerOFf.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 1, 1, 0.3f);
                break;
            default:
                break;
        }
    }
   /*public void HitItem(string name)
    {
        switch (name)
        {
            case "Heart":
                //heartOff.GetComponent<Material>().color = new Color(1, 0.5f, 0.5f, 0.3f);
                heartOff.GetComponent<MeshRenderer>().material.color = new Color(1, 0.5f, 0.5f, 0.8f);
                break;
            case "Timer":
                //timerOFf.GetComponent<Material>().color = new Color(0.5f, 1, 1, 0.3f);
                timerOFf.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 1, 1, 0.8f);
                break;
            default:
                break;
        }
    }*/
    public void InfoMsgOn(string name)//상세정보 알림켜짐
    {
        switch (name)
        {
            case "Heart":
                heartUI[0].SetActive(true);
                break;
            case "Timer":
                timerUI[0].SetActive(true);
                break;
            case "HammerPos":
                hammerUI[0].SetActive(true);
                
                switch (EquipManager.instance.equipList[0].level)
                {
                    case 1:
                        hammerInfoUI[0].enabled = true;
                        hammerInfoUI[1].enabled = false;
                        hammerInfoUI[2].enabled = false;
                        break;
                    case 2:
                        hammerInfoUI[0].enabled = false;
                        hammerInfoUI[1].enabled = true;
                        hammerInfoUI[2].enabled = false;
                        break;
                    case 3:
                        hammerInfoUI[0].enabled = false;
                        hammerInfoUI[1].enabled = false;
                        hammerInfoUI[2].enabled = true;
                        break;
                    default:
                        break;
                }
                break;
            case "KnifePos":
                knifeUI[0].SetActive(true);
                switch (EquipManager.instance.equipList[1].level)
                {
                    case 1:
                        knifeInfoUI[0].enabled = true;
                        knifeInfoUI[1].enabled = false;
                        knifeInfoUI[2].enabled = false;
                        break;
                    case 2:
                        knifeInfoUI[0].enabled = false;
                        knifeInfoUI[1].enabled = true;
                        knifeInfoUI[2].enabled = false;
                        break;
                    case 3:
                        knifeInfoUI[0].enabled = false;
                        knifeInfoUI[1].enabled = false;
                        knifeInfoUI[2].enabled = true;
                        break;
                    default:
                        break;
                }
                break;
            case "GrillPos":
                grillUI[0].SetActive(true);
                switch (EquipManager.instance.equipList[2].level)
                {
                    case 1:
                        grillInfoUI[0].enabled = true;
                        grillInfoUI[1].enabled = false;
                        grillInfoUI[2].enabled = false;
                        break;
                    case 2:
                        grillInfoUI[0].enabled = false;
                        grillInfoUI[1].enabled = true;
                        grillInfoUI[2].enabled = false;
                        break;
                    case 3:
                        grillInfoUI[0].enabled = false;
                        grillInfoUI[1].enabled = false;
                        grillInfoUI[2].enabled = true;
                        break;
                    default:
                        break;
                }
                break;
            case "Exit_Btn":
                exit.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void InfoMsgOff()
    {
        heartUI[0].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        timerUI[0].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        hammerUI[0].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        knifeUI[0].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        grillUI[0].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        exit.GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
    }
    public void BuyMsgOff()
    {
        heartUI[1].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        timerUI[1].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        hammerUI[1].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        knifeUI[1].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        grillUI[1].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
    }
    public void NoMoneyMsgOff()
    {
        heartUI[2].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        timerUI[2].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        hammerUI[2].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        knifeUI[2].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        grillUI[2].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        //noMoney.SetActive(false);
    }
    public void MaxLevelMsgOff()
    {
        hammerUI[3].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        knifeUI[3].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        grillUI[3].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
    }
    public void ItemStockMsgOff()
    {
        heartUI[3].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        timerUI[3].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
    }
    public void MoneyInfoMsgOff()
    {
        heartUI[4].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        timerUI[4].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        hammerUI[4].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        knifeUI[4].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
        grillUI[4].GetComponent<Animator>().SetTrigger("Close");//애니메이션 close(eventplay메소드 실행, SetActive(false)실행)
    }
    public void MoneyInfoMsg(string name)
    {
        switch (name)
        {
            case "Buy_Heart_Btn"://하트면
                if(ItemManager.instance.HeartStock == 0)
                {
                    if (heartUI[4].gameObject.activeSelf == true)
                    {
                        return;
                    }
                    heartUI[4].SetActive(true);
                    heartUI[4].GetComponentInChildren<TextMeshProUGUI>().text = "구매 가격\n" + ItemManager.instance.HeartPrice.ToString();
                }
                break;
            case "Buy_Timer_Btn"://타이머면 
                if (ItemManager.instance.TimerStock == 0)
                {
                    if (timerUI[4].gameObject.activeSelf == true)
                    {
                        return;
                    }
                    timerUI[4].SetActive(true);
                    timerUI[4].GetComponentInChildren<TextMeshProUGUI>().text = "구매 가격\n" + ItemManager.instance.TimerPrice.ToString();
                }
                break;
            case "Upgrade_Hammer_Btn":
                if(EquipManager.instance.equipList[0].level < 3) 
                {
                    if (hammerUI[4].gameObject.activeSelf == true)
                    {
                        return;
                    }
                    hammerUI[4].SetActive(true);
                    hammerUI[4].GetComponentInChildren<TextMeshProUGUI>().text = "업그레이드 가격\n" + EquipManager.instance.equipList[0].price.ToString();
                }
                break;
            case "Upgrade_Knife_Btn":
                if (EquipManager.instance.equipList[1].level < 3)
                {
                    if (knifeUI[4].gameObject.activeSelf == true)
                    {
                        return;
                    }
                    knifeUI[4].SetActive(true);
                    knifeUI[4].GetComponentInChildren<TextMeshProUGUI>().text = "업그레이드 가격\n" + EquipManager.instance.equipList[1].price.ToString();
                }
                break;
            case "Upgrade_Grill_Btn":
                if(EquipManager.instance.equipList[2].level < 3)
                {
                    if (grillUI[4].gameObject.activeSelf == true)
                    {
                        return;
                    }
                    grillUI[4].SetActive(true);
                    grillUI[4].GetComponentInChildren<TextMeshProUGUI>().text = "업그레이드 가격\n" + EquipManager.instance.equipList[2].price.ToString();
                }
                break;
        }
    }
    public void NoMoney(string name)//돈이 없다는 것을 표시 
    {
        switch (name)
        {
            case "heart" ://하트면
                heartUI[2].SetActive(true);
                Invoke("NoMoneyMsgOff", 2.0f);
                break;
            case "timer"://타이머면 
                timerUI[2].SetActive(true);
                Invoke("NoMoneyMsgOff", 2.0f);
                break;
            case "Hammer1":
            case "Hammer2":
            case "Hammer3":
                hammerUI[0].SetActive(false);
                hammerUI[2].SetActive(true);
                Invoke("NoMoneyMsgOff", 2.0f);
                break;
            case "Knife1":
            case "Knife2":
            case "Knife3":
                knifeUI[2].SetActive(true);
                Invoke("NoMoneyMsgOff", 2.0f);
                break;
            case "Grill1":
            case "Grill2":
            case "Grill3":
                grillUI[2].SetActive(true);
                Invoke("NoMoneyMsgOff", 2.0f);
                break;
        }
    }
    
    public void BtnCheck()//아이템 재고, 장비레벨에 따른 버튼 조절
    {
        if (ItemManager.instance.HeartStock == 1)
        {
            btnHeart.interactable = false;
            heartBtnText.color = new Color(0, 0, 0, 0.6f);
            heartBtnPart.Stop();
        }
        if(ItemManager.instance.TimerStock == 1)
        {
            btntimer.interactable = false;
            timerBtnText.color = new Color(0, 0, 0, 0.6f);
            timerBtnPart.Stop();
        }
        if (ItemManager.instance.HeartStock == 0)
        {
            btnHeart.interactable = true;
            heartBtnText.color = new Color(255,255,255);
            heartBtnPart.Play();
        }
        if (ItemManager.instance.TimerStock == 0)
        {
            btntimer.interactable = true;
            timerBtnText.color = new Color(255, 255, 255);
            timerBtnPart.Play();
        }
        if(EquipManager.instance.equipList[0].level == 3)
        {
            btnHammer.interactable = false;
            hammerBtnText.color = new Color(0, 0, 0, 0.6f);
            hammerBtnPart.Stop();
        }
        if (EquipManager.instance.equipList[1].level == 3)
        {
            btnKnife.interactable = false;
            knifeBtnText.color = new Color(0, 0, 0, 0.6f);
            knifeBtnPart.Stop();
        }
        if (EquipManager.instance.equipList[2].level == 3)
        {
            btnGrill.interactable = false;
            grillBtnText.color = new Color(0, 0, 0, 0.6f);
            grillBtnPart.Stop();
        }
    }

    public void BtnStart(int stage)
    {
        //처음에 로드한 스테이지의 lock정보를 가지고 있는 리스트의 값을 현재 눌린 스테이지의 값과 비교
        //열렸을 경우 스타트 버튼 활성화, 잠겼을 경우 스타트 버튼 비활성화
        //0 = 열림, 1 = 잠김
        btnStart.GetComponent<Button>().interactable = true;
        btnStart.transform.GetChild(1).gameObject.SetActive(true);
        startBtnText.color = new Color(255, 255, 255);
        startBtnPart.Play();
        /*if (StageManager.instance.stageLock
         * [stage-1] == 0)
        {

        else
        {
            btnStart.GetComponent<Button>().interactable = false;
            btnStart.transform.GetChild(1).gameObject.SetActive(false); 
            startBtnText.color = new Color(0, 0, 0, 0.6f);
            startBtnPart.Stop();
        }*/
    }
    public void UpgradeEquipUI(string name)
    {
        if (name.Contains("Hammer"))
        {
            //BuyEquipParticle("Hammer");
            hammerUI[1].SetActive(true);
            Invoke("BuyMsgOff", 2.0f);
        }
        if (name.Contains("Knife"))
        {
            //BuyEquipParticle("Knife");
            knifeUI[1].SetActive(true);
            Invoke("BuyMsgOff", 2.0f);
        }
        if (name.Contains("Grill"))
        {
            //BuyEquipParticle("Grill");
            grillUI[1].SetActive(true);
            Invoke("BuyMsgOff", 2.0f);
        }
    }
    public void BuyItemUI(string name)
    {
        switch (name)
        {
            case "heart":
                heartUI[1].SetActive(true);
                Invoke("BuyMsgOff", 2.0f);
                break;
            case "timer":
                timerUI[1].SetActive(true);
                Invoke("BuyMsgOff", 2.0f);
                break;
        }
    }
    public void ItemStockMsg(string name)
    {
        switch (name)
        {
            case "heart":
                heartUI[3].SetActive(true);
                Invoke("ItemStockMsgOff", 2.0f);
                break;
            case "timer":
                timerUI[3].SetActive(true);
                Invoke("ItemStockMsgOff", 2.0f);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        BtnCheck();
    }
}
