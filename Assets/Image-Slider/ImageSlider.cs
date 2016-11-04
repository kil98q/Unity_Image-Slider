using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour {

    [SerializeField] private Texture[] _images;
    [SerializeField] private int _currentImage = 0;
    [SerializeField] private float _timer = 0;
    [SerializeField] private float _time = 0;
    [SerializeField] private bool _autoSliding = false;
    [SerializeField] private bool _pauze;
    [SerializeField] private bool _startSliderOnStartUp = false;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private RawImage _image;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Text _currentImageText;

    void Start()
    {
       
        _currentImageText.text = _currentImage + 1 + "/" + _images.Length;
        if (_startSliderOnStartUp)
        {
            _autoSliding = false;
            ShowSlider();
        }
        else
        {
            _prevButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);
        }
    }

    void ShowSlider()
    {
        _prevButton.gameObject.SetActive(true);
        _nextButton.gameObject.SetActive(true);
        _image.gameObject.SetActive(true);

        _image.texture = _images[_currentImage];

        if (_images[_currentImage] is MovieTexture)
        {
            ((MovieTexture)_image.texture).Play();
            _image.gameObject.GetComponent<AudioSource>().clip = ((MovieTexture)_image.texture).audioClip;
            _image.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        StartAutoSlider();
    }

    void StartAutoSlider()
    {
        if (!_pauze)
        {
            if (_images[_currentImage] is MovieTexture)
            {
                Debug.Log(((MovieTexture)_image.texture).duration);
                _timer = ((MovieTexture)_image.texture).duration;
            }
            else
            {
                _timer = 2;
            }

            if (_timer <= _time)
            {
                Next();
                _time = 0;
            }
            else
            {
                _time += Time.deltaTime;
            }
        } else
        {
            if (_images[_currentImage] is MovieTexture)
            {
                _timer = ((MovieTexture)_image.texture).duration;
            } else
            {
                _timer = 4;
            }
             if (_timer <= _time)
             {
                 _pauze = false;
             } else
             {
                 _time += Time.deltaTime;
             }
        }
    }

    public void Next()
    {
   
        if (_currentImage != _images.Length - 1)
        {

            _currentImage += 1;
            _image.texture = _images[_currentImage];
            if(_images[_currentImage] is MovieTexture)
            {
                ((MovieTexture)_image.texture).Stop();
                Debug.Log(_currentImage);
                ((MovieTexture)_image.texture).Play();
                _image.gameObject.GetComponent<AudioSource>().clip = ((MovieTexture)_image.texture).audioClip;
                _image.gameObject.GetComponent<AudioSource>().Play();
            }
            _currentImageText.text = _currentImage + 1 + "/" + _images.Length;
        } else
        {
            _currentImage = 0;
            _image.texture = _images[_currentImage];
            if (_images[_currentImage] is MovieTexture)
            {
                ((MovieTexture)_image.texture).Stop();
                Debug.Log(_currentImage);
                ((MovieTexture)_image.texture).Play();
                _image.gameObject.GetComponent<AudioSource>().clip = ((MovieTexture)_image.texture).audioClip;
                _image.gameObject.GetComponent<AudioSource>().Play();
            }
            _currentImageText.text = _currentImage + 1 + "/" + _images.Length;
        }
    }

    public void StopAutoSliding()
    {
        _time = 0;
        _pauze = true;
    }

    public void Prev()
    {
        if (_currentImage != 0)
        {
            _currentImage -= 1;
            _image.texture = _images[_currentImage];
            if (_images[_currentImage] is MovieTexture)
            {
                ((MovieTexture)_image.texture).Stop();
                Debug.Log(_currentImage);
                ((MovieTexture)_image.texture).Play();
                _image.gameObject.GetComponent<AudioSource>().clip = ((MovieTexture)_image.texture).audioClip;
                _image.gameObject.GetComponent<AudioSource>().Play();
            }
            _currentImageText.text = _currentImage + 1 + "/" + _images.Length;

        } else
        {
            _currentImage = _images.Length-1;
            _image.texture = _images[_currentImage];
            if (_images[_currentImage] is MovieTexture)
            {
                ((MovieTexture)_image.texture).Stop();
                Debug.Log(_currentImage);
                ((MovieTexture)_image.texture).Play();
            _image.gameObject.GetComponent<AudioSource>().clip = ((MovieTexture)_image.texture).audioClip;
            _image.gameObject.GetComponent<AudioSource>().Play();
            }
            _currentImageText.text = _currentImage + 1 + "/" + _images.Length;
        }
    }
}
