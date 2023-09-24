using System.Collections;

public interface IEnemy
{ 
    public IEnumerator Hurt(float damage, float knockbackForce);
}