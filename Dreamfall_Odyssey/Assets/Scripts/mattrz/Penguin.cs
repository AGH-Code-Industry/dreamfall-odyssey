using System;
using UnityEngine;

namespace mattrz
{
    public class Penguin : Enemy
    {
        void Update()
        {
            base.Update();
            
            if(target.x - transform.position.x > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (target.x - transform.position.x < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
