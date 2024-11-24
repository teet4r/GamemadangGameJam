using System;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        if (Ingame.Instance.Hero.IsNull() || Ingame.Instance.Hero.IsDead)
            return;

        Vector2 heroPos = Ingame.Instance.Hero.transform.position;
        Vector2 pos = transform.position;

        float diffX = Math.Abs(heroPos.x - pos.x);
        float diffY = Math.Abs(heroPos.y - pos.y);

        Vector2 nextHeroPos = Ingame.Instance.Hero.nextPos;

        float xPos = nextHeroPos.x < 0 ? -1 : 1;
        float yPos = nextHeroPos.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                    transform.Translate(xPos * 43.2f * Vector2.right);
                else if (diffX < diffY)
                    transform.Translate(yPos * 43.2f * Vector2.up);
                break;
        }
    }
}
