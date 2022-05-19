using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDashing : MonoBehaviour

{
    [Header("Public")]
    public Rigidbody rb;
    public GameObject Orientation;
    public float rbPower = 20;
    
    
    float FirstInputTime = 0;
    float TimeSinceFirstInput = 1;
    float DoubleInputCD = -2;
    float DoubleInputWindow = -10;
    float triggerOnce = 0;
    
    
    float FirstInputTime2 = 0;
    float TimeSinceFirstInput2 = 1;
    float DoubleInputCD2 = -2;
    float DoubleInputWindow2 = -10;
    float triggerOnce2 = 0;

    //Alla variabler

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceFirstInput = Time.time - FirstInputTime;
        TimeSinceFirstInput2 = Time.time - FirstInputTime2;

        if (Time.time - DoubleInputCD >= 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (TimeSinceFirstInput <= 0.5)
                {

                    DoubleInputCD = Time.time;
                    FirstInputTime = 0;
                    DoubleInputWindow = Time.time;
                }

                else
                {
                    FirstInputTime = Time.time;
                }
            }
        }

        //Denna del h�ller koll p� v�rt input (A) och s�tter en timer s� att om A trycks 2 g�nger inom en halv sekund s� ska det utf�ra en funktion nedan.

        if (Time.time - DoubleInputWindow <= 0.2)
        {
            if (triggerOnce == 0)
            {
                TimeSlow();
                DashLeft();
                triggerOnce = 1;
                StartCoroutine("TimeNormal");
            }
            
        }
        else
        {
            triggerOnce = 0;
            
        }

        //if statement �r funktionen som utf�rs n�r funktionen ovan har g�tt igenom, Else statement �r f�r att resetta v�r timer.

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (TimeSinceFirstInput2 <= 0.5)
            {
                DoubleInputCD2 = Time.time;
                FirstInputTime2 = 0;
                DoubleInputWindow2 = Time.time;
            }

            else
            {
                FirstInputTime2 = Time.time;
            }
        }
        if (Time.time - DoubleInputWindow2 <= 0.2)
        {
            if (triggerOnce2 == 0)
            {
                TimeSlow();
                DashRight();
                triggerOnce2 = 1;
                StartCoroutine("TimeNormal");
            }

        }
        else
        {
            triggerOnce2 = 0;

        }
    }


    //En duplikation av funktionerna ovan f�r input (D) som g�r exakt samma sak; varf�r? m� du undra, f�r att inte g�ra det jobbigt med variablerna �r svaret.


    void TimeSlow()
    {
        Time.timeScale = 0.10f;
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }
    //Saktar ner tiden och �kar fysik uppdateringar
    void DashLeft()
    {
        rb.AddForce(-Orientation.transform.right * rbPower, ForceMode.Impulse);
    }
    //Dashar v�nster (galet)
    void DashRight()    
    {
        rb.AddForce(Orientation.transform.right * rbPower, ForceMode.Impulse);
    }
    //Dashar h�ger (galet)
    IEnumerator TimeNormal()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;


    }
    //Genom en courontine och en timer s� �ndrar det tillbaka tiden till sitt ursprungliga v�rde.
    

    //Jakob �r extremt sn�ll och gullig och basically gav mig skriptets skellet (f�rklarade det p� ett v�ldigt l�ttf�rst�eligt och karismatiskt s�tt)//Jakob.
    //Vi jobbade p� det tillsammans om du fr�gar mig.
}
