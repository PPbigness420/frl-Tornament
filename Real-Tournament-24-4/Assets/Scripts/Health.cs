using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    public int maxHp = 100;

    void Start()
    {
        if(hp == 0)hp = maxHp;
    }

    public void Damage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}