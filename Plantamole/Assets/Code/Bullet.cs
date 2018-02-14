using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;

	void Update () {
        transform.position = GameController.GetTruePos(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D[] contactPoints = new ContactPoint2D[10];
        collision.GetContacts(contactPoints);
        if (collision.gameObject.GetComponent<Worm>()) {
            collision.gameObject.GetComponent<Worm>().GetDamage(10);
            Instantiate(Resources.Load("WormDamagedSound"), GameController.GetTruePos(contactPoints[0].point), Quaternion.identity, GameController.game);
        } else {
            Instantiate(Resources.Load("CarrotHitSound"), GameController.GetTruePos(contactPoints[0].point), Quaternion.identity, GameController.game);
        }
        Instantiate(Resources.Load("CarrotHitParticle"), GameController.GetTruePos(contactPoints[0].point), Quaternion.identity, GameController.game);
        Destroy(gameObject);
    }
}