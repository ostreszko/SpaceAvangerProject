using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Klasa odpowiedzialna za zachowanie statku gracza
public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;
    float moveDirection;
    float speed = 0.25f;
    int lives = 1;
    int minusLive = 1;

    public float fireRate = 1f;
    private float nextFireTime;

    float health = 100;
     int myScore = 0;

    ObjectPooler objectPooler;
    Text hpGuiText;
    Text scoreGuiText;
    Text livesGuiText;
    PauseClass endGame;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        objectPooler = ObjectPooler.Instance;
        hpGuiText = GameObject.Find("HP").GetComponent<Text>();
        scoreGuiText = GameObject.Find("Score").GetComponent<Text>();
        livesGuiText = GameObject.Find("Lives").GetComponent<Text>();
        //Przypisanie metod do wydarzeń zmian statystyk gracza wyświtlanych na GUI
        Damaged += (sender, args) => SetGUIDamage(args.Amount);
        Scored += (sender, args) => SetGUIScore();
        Healed += (sender, args) => SetGUIHeal(args.Amount);
        Killed += (sender, args) => SetGUILives(args.Amount);
        endGame = PauseClass.Instance;
    }

    private void Awake()
    {
        StaticStates.ActualState = (int)StaticStates.States.Game;
    }

    //Eventy EventDrivenGUI
    public event EventHandler<DamageEventArgs> Damaged;
    public event EventHandler<HealEventArgs> Healed;
    public event EventHandler<ScoreEventArgs> Scored;
    public event EventHandler<KillEventArgs> Killed;

    void Update()
    {
        //Blokada akcji gracza podczas pauzy, Poruszanie zrobione na ruch myszki a strzał na lewy klik
        if (StaticStates.ActualState != (int)StaticStates.States.Pause)
        {
            moveDirection = Input.GetAxis("Mouse X") * speed;
            playerTransform.position += Vector3.right * moveDirection;
            if (playerTransform.position.x >= 9.5f)
            {
                playerTransform.position = new Vector3(9.5f, playerTransform.position.y);
            }
            else if (playerTransform.position.x <= -9.5f)
            {
                playerTransform.position = new Vector3(-9.5f, playerTransform.position.y);
            }

            if (Input.GetButton("Fire1") && nextFireTime < Time.time)
            {
                objectPooler.SpawnFromPool("Projectile", playerTransform.position + Vector3.up * 2, playerTransform.rotation);
                nextFireTime = Time.time + fireRate;
            }
            //Jeżeli zdrowie spada do 0 a mamy dodatkowe życie, zostajemy wskrzeszeni, w przeciwnym wypadku umieramy
            if (health <= 0)
            {
                if(lives > 0)
                {
                    health += 100;
                    Healed += (sender, args) => {
                       health  = args.Amount;
                    };
                    HealPlayer(health);
                    
                    Killed += (sender, args) => {
                        minusLive = args.Amount;
                    };
                    KillPlayer(minusLive);
                }
                else
                {
                    endGame.pauseMenuUI.GetComponentInChildren<Text>().color = new Color(233f / 255f, 79f / 255f, 55f / 255f);
                    endGame.pauseMenuUI.GetComponentInChildren<Text>().text = "YOU DIED";
                    gameObject.SetActive(false);
                    endGame.Pause();
                    StaticStates.ActualState = (int)StaticStates.States.GameOver;
                }
            }
        }
    }

    //EventDrivenGUI, wydarzenia i metody
    public class DamageEventArgs : EventArgs
    {
        public DamageEventArgs(float amount)
        {
            Amount = amount;
        }
        public float Amount { get; private set; }
    }

    public class HealEventArgs : EventArgs
    {
        public HealEventArgs(float amount)
        {
            Amount = amount;
        }
        public float Amount { get; private set; }
    }

    public class ScoreEventArgs : EventArgs
    {
        public ScoreEventArgs(int amount)
        {
            Amount = amount;
        }
        public int Amount { get; private set; }
    }

    public class KillEventArgs : EventArgs
    {
        public KillEventArgs(int minusLive)
        {
            Amount = minusLive;
        }
        public int Amount { get; private set; }
    }

    private void SetGUIDamage(float damage)
    {
        health = Math.Max(health - damage, 0);
        hpGuiText.text = health.ToString();
    }

    private void SetGUILives(int minusLive)
    {
        lives -= minusLive;
        livesGuiText.text = lives.ToString();
    }

    private void SetGUIHeal(float heal)
    {
        health = Math.Min(health + heal, 100);
        hpGuiText.text = health.ToString();
    }
    private void SetGUIScore()
    {
        scoreGuiText.text = myScore.ToString();
    }

    public void DamagePlayer(float damage)
    {
        if (Damaged != null)
        {
            Damaged(this, new DamageEventArgs(damage));
        }
    }

    public void HealPlayer(float heal)
    {
        if (Healed != null)
        {
            Healed(this, new HealEventArgs(heal));
        }
    }

    public void KillPlayer(int minusLive)
    {
        if (Killed != null)
        {
            Killed(this, new KillEventArgs(minusLive));
        }
    }
    public void AddScore(int score)
    {
        if (Scored != null)
        {
            myScore = myScore + score;
            Scored(this, new ScoreEventArgs(score));
            scoreGuiText.text = myScore.ToString();
        }
    }




}
