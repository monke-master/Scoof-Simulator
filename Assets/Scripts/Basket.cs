using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public int dealth = 3;
    private Text _scoreText;

    private GameObject _canvas;

    public GameObject dealthPrefab;
    public GameObject defeatTextPrefab;
    private float _dealthMargin = 40f;

    private List<GameObject> _dealthObjects;
    private BasketAudio audioSource;

    private HighScore _highScore;
   

    // Start is called before the first frame update
    void Start()
    {
        var scoreObject = GameObject.Find("Score");
        _scoreText = scoreObject.GetComponent<Text>();
        _scoreText.text = "Счет: 0";
        _canvas = GameObject.Find("Canvas");

        _dealthObjects = new List<GameObject>();

        float x = -(_canvas.GetComponent<RectTransform>().rect.width / 2) + _canvas.transform.position.x;
        float y = _canvas.GetComponent<RectTransform>().rect.height / 2 + _canvas.transform.position.y;
        for (int i = 0; i < dealth; i++)
        {
            var dealthImage = GameObject.Instantiate(dealthPrefab);
            dealthImage.transform.SetParent(_canvas.transform);
            
            x += dealthImage.GetComponent<RectTransform>().rect.width / 2;
            float dealthHeight = dealthImage.GetComponent<RectTransform>().rect.height;
            
            var pos = dealthImage.transform.position;
            pos.x = x;
            pos.y = y - dealthHeight / 2;
            dealthImage.transform.position = pos;

            x += _dealthMargin;

            _dealthObjects.Add(dealthImage);
        }
        
        audioSource = GameObject.Find("Basket Audio").GetComponent<BasketAudio>();
        _highScore = GameObject.Find("High Score").GetComponent<HighScore>();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;

        mousePos2D.z = -Camera.main.transform.position.z;

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Scoofpsule")
        {
            Destroy(collidedWith);
            var scoreStr = _scoreText.text.Substring(_scoreText.text.IndexOf(" ") + 1);
            var score = int.Parse(scoreStr);
            score += 100;
            _scoreText.text = _scoreText.text.Substring(
                0, 
                _scoreText.text.IndexOf(" ") + 1) + score;

            if (score > HighScore.highScore)
            {
                _highScore.SetHighScore(score);
            }
            
            if (!audioSource.isPlaying())
                audioSource.PlaySuccess();
        }
    }

    public void OnScoofpsuleDestroyed()
    {
        UpdateDealth();
        if (_dealthObjects.Count > 0)
        {
            var dealthImage = _dealthObjects[_dealthObjects.Count - 1];
            _dealthObjects.Remove(dealthImage);
            GameObject.Destroy(dealthImage);
        }
    }

    private void UpdateDealth()
    {
        dealth--;
        if (dealth < 0)
            Lose();
    }

    private void Lose()
    {
        Destroy(this);
        audioSource.PlayDefeat();
        ShowDefeatText();
    }

    private void ShowDefeatText()
    {
        var defeatText = GameObject.Instantiate(defeatTextPrefab);
        defeatText.transform.SetParent(_canvas.transform);
        var pos = defeatText.transform.position;
        pos.x = _canvas.GetComponent<RectTransform>().rect.width / 2;
        pos.y = _canvas.GetComponent<RectTransform>().rect.height / 2;
        defeatText.transform.position = pos;
    }
}
