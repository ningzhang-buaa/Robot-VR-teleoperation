using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RosSharp.RosBridgeClient
{
    public class GetPositionValue : UnityPublisher<MessageTypes.Geometry.Pose>
    {
        private MessageTypes.Geometry.Pose posmessage;

        protected override void Start()
        {
            base.Start();
        }
        private void FixedUpdate()
        {
            Vector3 p = this.transform.position;
            Vector3 e = this.transform.eulerAngles;
            Quaternion q = Quaternion.Euler(e);
            posmessage = new MessageTypes.Geometry.Pose
            {
                position = new MessageTypes.Geometry.Point{x=p.x, y=p.y, z=p.z},
                orientation = new MessageTypes.Geometry.Quaternion{x=q.x, y=q.y, z=q.z, w=q.w},
            };
            Publish(posmessage);
        }
    }
}
