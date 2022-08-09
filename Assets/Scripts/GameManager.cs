using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud, menu;
    //Logic
    public int money;
    public int experience;

    //Show floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);

    }
    //Upgrade weapon

    public bool TryUpgradeWeapon()
    {
        //Max weapon level
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }
        if (money >= weaponPrices[weapon.weaponLevel])
        {
            money -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        else
        {
            return false;
        }
    }


    //Hitpoint bar
    public void OnHitpointsChange()
    {
        float ratio = (float)player.hitpoints / player.maxHitpoints;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //Experience
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;
            if (r == xpTable.Count)
                return r;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("Level up");
        player.OnLevelUp();
        OnHitpointsChange();
    }


    //Load Scene
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Death menu Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
    //Start Game
    public void StartGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
    //Save state
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += money.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin
        money = int.Parse(data[1]);

        //Set experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));



    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
