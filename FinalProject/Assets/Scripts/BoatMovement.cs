using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class BoatMovement : MonoBehaviour
{
    [Header ("Boat Spec")]
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float upSpeed = 5f;
    [SerializeField] private float rotateSpeed = 0.1f;
    [SerializeField] private int fever_timmer = 0;

    private Rigidbody rb;
    private double asymmetry = 0;
    private float speedup = 1f;
    private Color default_color = new Color(0f, 0f, 0f, 0.45f);

    [Header ("Control")]
    public CircularDrive wheel;
    public ShotBomb shotBomb;

    [Header ("UI")]
    public Slider slider;
    public GameObject compass;
    public Transform end_point;
    public Image[] brainImg = null;

    public bool is_driving { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(feverTimeTrigger());
        rb = GetComponent<Rigidbody>();
        Player.instance.transform.SetParent(transform);
        is_driving = true;
    }

    void FixedUpdate()
    {
        if (is_driving)
        {
            ProcessMovement();
            ProcessRotation();
        }
    }

    void Update() {
        updateCompass();
    }

    #region  boat movement

    void ProcessMovement()
    {
        rb.AddRelativeForce(Vector3.forward * forwardSpeed * speedup);
    }

    void ProcessRotation()
    {
        float angle = wheel.outAngle * rotateSpeed;
        transform.Rotate(Vector3.up * angle * Time.fixedDeltaTime);
    }

    #endregion

    #region fevertime

    IEnumerator feverTimeTrigger()
    {
        while (true)
        {
            updateAsymmetry();
            if (asymmetry >= 0){
                if(fever_timmer > 210) continue;
                fever_timmer++;
            }
            else{
                if(fever_timmer < 2) continue;
                fever_timmer -=2;
            }
            speedup = fever_timmer * 0.005f + 1f;
            slider.value = fever_timmer;

            if(fever_timmer >= 150){
                // fevertime START
                speedup = fever_timmer * 0.01f + 1f;
                StartCoroutine(VerticalBoatMovement(1f));
                for(int i = 0; i < 150; i++){
                    slider.value--;
                    yield return new WaitForSeconds(0.1f);
                }
                // fevertime END
                speedup = 1f;
                StartCoroutine(VerticalBoatMovement(-1f));
                fever_timmer=0;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator VerticalBoatMovement(float dir)
    {
        int one_second = (int)(1 / Time.fixedDeltaTime);
        for (int i = 0; i < one_second * 5; i++)
        {
            transform.Translate(Vector3.up * Time.fixedDeltaTime * upSpeed * dir);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }


    #endregion

    void updateAsymmetry()
    {
        if(LinkConnection.Instance != null)
            asymmetry = LinkConnection.Instance.asymmetry_value;
        if(brainImg.Length == 2){
            if(asymmetry > 0){
                brainImg[0].color = new Color(0f,1f,0f,0.5f);
                brainImg[1].color = new Color(0f,1f,0f,0.5f);
            }
            else if(asymmetry < 0){
                brainImg[0].color = new Color(1f,0f,0f,0.5f);
                brainImg[1].color = default_color;
            }
            else{

            }
        }
    }

    void updateCompass()
    {
        if(compass != null && end_point != null)
        {
            compass.transform.LookAt(end_point, Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Item"){
            shotBomb.getBomb();
            Destroy(other.gameObject);
        }
    }

}
