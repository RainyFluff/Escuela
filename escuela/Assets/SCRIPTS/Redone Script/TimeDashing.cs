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

        //Denna del håller koll på vårt input (A) och sätter en timer så att om A trycks 2 gånger inom en halv sekund så ska det utföra en funktion nedan.

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

        //if statement är funktionen som utförs när funktionen ovan har gått igenom, Else statement är för att resetta vår timer.

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


    //En duplikation av funktionerna ovan för input (D) som gör exakt samma sak; varför? må du undra, för att inte göra det jobbigt med variablerna är svaret.


    void TimeSlow()
    {
        Time.timeScale = 0.10f;
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }
    //Saktar ner tiden och ökar fysik uppdateringar
    void DashLeft()
    {
        rb.AddForce(-Orientation.transform.right * rbPower, ForceMode.Impulse);
    }
    //Dashar vänster (galet)
    void DashRight()    
    {
        rb.AddForce(Orientation.transform.right * rbPower, ForceMode.Impulse);
    }
    //Dashar höger (galet)
    IEnumerator TimeNormal()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;


    }
    //Genom en courontine och en timer så ändrar det tillbaka tiden till sitt ursprungliga värde.
    

    //Jakob är extremt snäll och gullig och basically gav mig skriptets skellet (förklarade det på ett väldigt lättförståeligt och karismatiskt sätt)//Jakob.
    //Vi jobbade på det tillsammans om du frågar mig.
}
