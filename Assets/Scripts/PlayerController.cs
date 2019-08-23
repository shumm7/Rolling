using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    //物理
    private Rigidbody PlayerRigidbody;
    [SerializeField] public float speed = 3f;
    [SerializeField] private bool StartJump = true;
    [SerializeField] public int NeedToGet = 12;
    [SerializeField] string NextSceneName = "Stage2";

    //カメラ
    private Vector3 moveForward;
    public GameObject Camera;
    private Vector3 cameraVector;
    private Vector3 move;

    //Particle
    public GameObject particle;

    //UI
    public int score = 0;
    public Text scoreText;
    public Text resultText;
    public Text infoText;
    private bool holdingDown = false;
    private int pressedCount = 0;

    //Audio
    public AudioSource SoundEffect;
    public AudioClip Sound1;
    public AudioClip Sound2;

    void Start()
    {
        //Rigidbody
        PlayerRigidbody = GetComponent<Rigidbody>();

        //UI初期化
        SetScoreText();
        resultText.text = "";
        infoText.text = "";
    }

    void Update()
    {
        // スタート時の挙動
        if (StartJump)
        {
            StartJump = false;
            PlayerRigidbody.AddForce(0f, 10.0f, 0f, ForceMode.Impulse);
        }

        //ボールの移動
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");

        if (moveForward != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveForward);

        cameraVector = Vector3.Scale(Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveForward = cameraVector * moveZ + Camera.transform.right * moveX;
        move = moveForward * speed;

        PlayerRigidbody.AddForce(move - PlayerRigidbody.velocity, ForceMode.Acceleration);

        //落下時
        Transform playerTransform = this.transform;
        Vector3 pos = playerTransform.position;
        if (pos.y <= 5)
        {
            pos.x = 0;
            pos.z = 0;
            pos.y = 22;

            playerTransform.position = pos;
            PlayerRigidbody.velocity = Vector3.zero;
        }

        //勝利時画面遷移
        if (score >= NeedToGet) {
            if (Input.anyKey)
                holdingDown = true;

            // キーが離されたとき
            if (!Input.anyKey && holdingDown)
            {
                holdingDown = false;
                pressedCount++;

                if(pressedCount >= 2)
                    GetComponent<SceneController>().LoadScene(NextSceneName);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        //Pointと衝突時にPointを非表示
        if (other.gameObject.CompareTag("Point"))
        {
            other.gameObject.SetActive(false);
            Instantiate(particle, transform.position, transform.rotation);

            //float threshold = 0.15f;
            //SoundEffect.pitch = 1.0f + threshold * (score % 4);
            if(score % 4 == 3)
            {
                SoundEffect.PlayOneShot(Sound2);
            }
            else
            {
                SoundEffect.PlayOneShot(Sound1);
            }


            score++;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "SCORE " + score.ToString() + " / " + NeedToGet.ToString();

        if (score >= NeedToGet)
        {
            // リザルトの表示を更新
            resultText.text = "ROLLED!";
            infoText.text = "Press Any Key";

        }
    }
}