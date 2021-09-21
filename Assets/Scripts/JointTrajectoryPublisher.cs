using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace RosSharp.RosBridgeClient
{
    public class JointTrajectoryPublisher : UnityPublisher<MessageTypes.Trajectory.JointTrajectory>
    {
        private string FrameId = "Unity";
        private BioIK.BioIK hebi;
 
        private MessageTypes.Trajectory.JointTrajectory message;
 
        protected override void Start()
        {
            base.Start();
            hebi = this.gameObject.GetComponent<BioIK.BioIK>();
            InitializeMessage();
        }
 
        private void InitializeMessage()
        {
            message = new MessageTypes.Trajectory.JointTrajectory
            {
                header = new MessageTypes.Std.Header { frame_id = FrameId },
                points = new MessageTypes.Trajectory.JointTrajectoryPoint[1],
            };
            message.points[0] = new MessageTypes.Trajectory.JointTrajectoryPoint();
            message.points[0].positions = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].velocities = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].accelerations = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].effort = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].time_from_start = new MessageTypes.Std.Duration { nsecs= 20000000 };
        }
 
        private void FixedUpdate()
        {
            int i = 0;
            foreach (BioIK.BioSegment segment in hebi.Segments)
            {
                if (segment.Joint != null)
                {
                    double angle = 0.0;
                    if (segment.Joint.X.IsEnabled())
                    {
                        angle = segment.Joint.X.GetCurrentValue();
                    }
                    else if (segment.Joint.Y.IsEnabled())
                    {
                        angle = segment.Joint.Y.GetCurrentValue();
                    }
                    else if (segment.Joint.Z.IsEnabled())
                    {
                        angle = segment.Joint.Z.GetCurrentValue();
                    }
                    message.points[0].positions[i] = angle * Mathf.PI / 180;
                    i++;
                }
            }
            Publish(message);
        }
    }
}
