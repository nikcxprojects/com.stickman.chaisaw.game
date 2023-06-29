using System.Collections;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PieceType type;
    [SerializeField] private AudioClip hurtClip;

    public PieceType Type => type;
    
    private HingeJoint2D _joint;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = playerController.PieceColor;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Chainsaw") && playerController.Pieces.Contains(this))
        {
            _joint.enabled = false;
            collision.gameObject.GetComponent<Chainsaw>().Destroy();
            Instantiate(playerController.EffectPieceDeath, collision.transform.position, Quaternion.identity);
            playerController.CheckPieces(this);
            AudioManager.getInstance().PlayAudio(hurtClip);
            AudioManager.getInstance().Vibrate(100);
            StartCoroutine(ChangeColor(playerController.PieceColor, 
                playerController.DeathPieceColor, playerController.TimeAnimationDeath));
            StartCoroutine(ChangeColor(playerController.DeathPieceColor, playerController.PieceColor,
                playerController.TimeAnimationDeath, playerController.TimeAnimationDeath));
        }
    }
    
    private IEnumerator ChangeColor(Color startColor, Color finishColor, float lifeTime, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        float timeElapsed = 0;
        while (timeElapsed < lifeTime)
        {
            var color = Color.Lerp(startColor, finishColor, timeElapsed / lifeTime);
            _spriteRenderer.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _spriteRenderer.color = finishColor;
    } 
    
    public enum PieceType
    {
        Vital,
        Other
    }
}
