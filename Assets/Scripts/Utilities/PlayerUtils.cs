using System.Collections.Generic;

public class PlayerUtils
{
    private float health = 100.0f;
    private float maxHP = 100.0f;

    public float getHealth()
    {
        return health;
    }
    public void returnHPToMax() { health = maxHP; }
    public void HPGrant(float amount)
    {
        if (health < maxHP)
        {
            health += amount;
            if (health > maxHP)
            {
                health = maxHP;
            }
        }
    }
    public void HPRemove(float amount)
    {
        health -= amount;
    }
}