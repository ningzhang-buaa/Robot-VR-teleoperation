using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace RosSharp.RosBridgeClient
{
    public class JointTrajectoryPublisherUr5 : UnityPublisher<MessageTypes.Trajectory.JointTrajectory>
    {
        private string FrameId = "Unity";
        private BioIK.BioIK hebi;
 
        private MessageTypes.Trajectory.JointTrajectory message;

        double eular;
 
        protected override void Start()
        {
            base.Start();
            hebi = this.gameObject.GetComponent<BioIK.BioIK>();
            InitializeMessage();
        }
 
        private void InitializeMessage()
        {
            eular = 0;
            message = new MessageTypes.Trajectory.JointTrajectory
            {
                header = new MessageTypes.Std.Header { frame_id = FrameId },
                points = new MessageTypes.Trajectory.JointTrajectoryPoint[1],
            };
            message.points[0] = new MessageTypes.Trajectory.JointTrajectoryPoint();
            message.points[0].positions = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            message.points[0].velocities = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            message.points[0].accelerations = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            message.points[0].effort = new double[] { 0, 0, 0, 0, 0, 0, 0 };
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

            if(eular>0&&eular<45)
            {
                if(Input.GetKey(KeyCode.JoystickButton2))
                {
                     eular++;
                }
                if(Input.GetKey(KeyCode.JoystickButton3))
                {
                    eular--; 
                }
            }
            else if(eular<=0&&eular>-1)
            {
                if(Input.GetKey(KeyCode.JoystickButton2))
                {
                    eular++; 
                }
            }
            else if(eular>=45&&eular<46)
            {
                if(Input.GetKey(KeyCode.JoystickButton3))
                {
                    eular--; 
                }
            }
            message.points[0].positions[6]=eular/180*Mathf.PI;
            Publish(message);
        }
    }
}
