using Robot.Common;
using System.Collections.Generic;

namespace VitaliiKindrat.ChallengeRobot
{
    public interface IMyAlgorithm
    { 
        RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map);
    }
}
