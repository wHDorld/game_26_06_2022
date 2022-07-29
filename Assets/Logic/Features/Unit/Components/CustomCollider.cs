using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Unit.Components
{
    [RequireComponent(typeof(Collider))]
    public class CustomCollider : MonoBehaviour
    {
        ContactPoint[] contacts;
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("123");
            contacts = new ContactPoint[collision.contactCount];
            collision.GetContacts(contacts);
        }
        private void OnDrawGizmos()
        {
            if (contacts == null) return;
            Gizmos.color = Color.green;
            foreach (var a in contacts)
                Gizmos.DrawSphere(a.point, 0.1f);
        }
    }
}
